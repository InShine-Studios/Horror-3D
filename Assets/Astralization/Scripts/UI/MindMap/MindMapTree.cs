using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public interface IMindMapTree
{
    int NodeCount { get; }
    MindMapNode Root { get; }

    bool IsNodeRelated();
    void AddNode();
    void BuildNodeRelation();
    void ClearAllNodes();
    void LoadTree();
    void LoadTree(MindMapTreeData data);
}

#region SerializableClass
[Serializable]
public class NodeModelDictionary : SerializableDictionary<MindMapNodeType, GameObject> { }
#endregion

/**
 * Class for managing and manipulating mind map tree.
 * Current intended usage: Chapter Clues implementation 
 */
[ExecuteInEditMode]
public class MindMapTree : MonoBehaviour, IMindMapTree
{
    #region Const
    public static string LayerName = "MindMap";
    #endregion

    #region Variables
    [Header("Prefabs")]
    [SerializeField]
    private MindMapNode _mindMapNodeBase;
    [SerializeField]
    private NodeModelDictionary _nodeModelDictionary;

    [Header("Tree Data")]
    [SerializeField]
    private MindMapNode _root;
    [SerializeField]
    private MindMapTreeData _mindMapTreeData;

    [Header("Camera and Navigation")]
    [SerializeField]
    private MindMapCameraManager _mindMapCameraManager; // TODO: make this non-serializable and integrate with UiState

    private MindMapNode selectedNode = null;
    private int coreNodeIdx;
    private int clueNodeIdx;

    public MindMapNode Root { get { return _root; } }
    public int NodeCount { get { return transform.childCount; } }
    #endregion

    #region SetGet
    private MindMapNode GetNode(string nodeName)
    {
        Queue<MindMapNode> queue = new Queue<MindMapNode>();
        queue.Enqueue(_root);

        while (queue.Count > 0)
        {
            MindMapNode currentNode = queue.Dequeue();
            if (currentNode.NodeName == nodeName) return currentNode;
            foreach (MindMapNode child in currentNode.Children)
            {
                queue.Enqueue(child);
            }
        }

        return null;
    }

    private MindMapNode GetChildrenOfNode(MindMapNode node, int childIdx)
    {
        return node.Children[childIdx];
    }

    private void SelectNode(string nodeName)
    {
        selectedNode = GetNode(nodeName);
    }

    private void SelectNode(MindMapNode node)
    {
        selectedNode = node;
    }
    #endregion

    #region MonoBehaviour
    private void Start()
    {
        coreNodeIdx = -1;
        clueNodeIdx = -1;
        LoadTree();
        SetCameraFocus(_root);
    }
    #endregion

    #region Loader
    public void BuildNodeRelation()
    {
        if (IsNodeRelated())
        {
            Debug.LogWarning("[MIND MAP] Nodes are already connected with each other. " +
                "Ignore this when you're trying to save tree data.");
            return;
        }

        MindMapNode[] mindMapNodes = GetComponentsInChildren<MindMapNode>();
        foreach (MindMapNode mindMapNode in mindMapNodes)
        {
            if (mindMapNode.Parent != null)
            {
                mindMapNode.Parent.Children.Add(mindMapNode);
            }
        }
    }
    #endregion

    #region NodeHandler
    public bool IsNodeRelated()
    {
        foreach (MindMapNode mindMapNode in GetComponentsInChildren<MindMapNode>())
        {
            if (mindMapNode.Children.Count == 0 && mindMapNode.NodeType == MindMapNodeType.CORE)
            {
                return false;
            }
        }
        return true;
    }
    public void ClearAllNodes()
    {
        while (transform.childCount != 0)
        {
            DestroyImmediate(transform.GetChild(0).gameObject);
        }
        Debug.Log("[MIND MAP] Node is cleared.");
    }

    public void AddNode()
    {
        MindMapNode mindMapNode = Instantiate(_mindMapNodeBase);
        mindMapNode.transform.parent = transform;
    }

    public void LoadTree()
    {
        if (_mindMapTreeData == null)
        {
            Debug.LogError("[MIND MAP BUILDER] Mind map tree data has not been assigned");
            return;
        }

        ClearAllNodes();

        int nodeCount = _mindMapTreeData.ParentReferenceIdx.Count;
        MindMapNode[] nodeInstances = new MindMapNode[nodeCount];

        for (int i = 0; i < nodeCount; i++)
        {
            MindMapNode newNode = Instantiate(_mindMapNodeBase);
            // General assignments
            newNode.NodeName = _mindMapTreeData.NodeNames[i];
            newNode.NodeDescription = _mindMapTreeData.NodeDescriptions[i];
            newNode.AnimController = _mindMapTreeData.NodeAnimationControllers[i];
            
            // Type-related assignments
            newNode.NodeType = _mindMapTreeData.NodeTypes[i];
            GameObject nodeModel = Instantiate(_nodeModelDictionary[newNode.NodeType]);
            nodeModel.transform.parent = newNode.transform;
            nodeModel.gameObject.layer = LayerMask.NameToLayer(LayerName);

            // Parent reference assignments
            if (_mindMapTreeData.ParentReferenceIdx[i] != -1)
            {
                newNode.Parent = nodeInstances[_mindMapTreeData.ParentReferenceIdx[i]];
            }
            else
            {
                _root = newNode;
            }

            // Transform assignments
            newNode.transform.parent = transform;
            newNode.transform.position = _mindMapTreeData.NodePositions[i];
            newNode.transform.rotation = _mindMapTreeData.NodeRotations[i];
            newNode.transform.localScale = _mindMapTreeData.NodeScales[i];
            newNode.SetCameraFollowPosition(_mindMapTreeData.NodeCameraFollowPosition[i]);
            newNode.SetCameraLookAtPosition(_mindMapTreeData.NodeCameraLookAtPosition[i]);

            newNode.gameObject.layer = LayerMask.NameToLayer(LayerName);

            newNode.name = newNode.NodeName + " node";
            nodeInstances[i] = newNode;
        }

        BuildNodeRelation();
    }

    public void LoadTree(MindMapTreeData data)
    {
        _mindMapTreeData = data;
        LoadTree();
    }

    public void ChangeCore(int indexStep)
    {
        if (_mindMapCameraManager.IsOnTransition) return;

        if (coreNodeIdx == -1)
        {
            coreNodeIdx = 0;
        } 
        else
        {
            List<MindMapNode> coreNodes = Root.Children;
            coreNodeIdx = Utils.MathCalcu.mod(coreNodeIdx + indexStep, coreNodes.Count);
        }
        SetCameraFocus(Root.Children[coreNodeIdx]);
    }

    public void ChangeClue(int indexStep)
    {
        if (_mindMapCameraManager.IsOnTransition) return;

        if (coreNodeIdx == -1) return;

        if (selectedNode.NodeType == MindMapNodeType.CORE)
        {
            clueNodeIdx = 0;
        }
        else
        {
            List<MindMapNode> clueNodes = Root.Children[coreNodeIdx].Children;
            clueNodeIdx = Utils.MathCalcu.mod(clueNodeIdx + indexStep, clueNodes.Count);
        }
        SetCameraFocus(Root.Children[coreNodeIdx].Children[clueNodeIdx]);

    }
    #endregion

    #region Camera Manipulation
    public void SetCameraFocus(MindMapNode node)
    {
        _mindMapCameraManager.FocusOn(node.GetCameraFollow(), node.GetCameraLookAt());
        selectedNode = node;
    }
    #endregion
}
