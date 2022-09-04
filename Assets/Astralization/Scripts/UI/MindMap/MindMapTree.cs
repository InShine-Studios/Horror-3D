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


/**
 * Class for managing and manipulating mind map tree.
 * Current intended usage: Chapter Clues implementation 
 */
public class MindMapTree : MonoBehaviour, IMindMapTree
{
    #region Variables
    [SerializeField]
    private MindMapNode _mindMapNodePrefab;
    [SerializeField]
    private MindMapNode _root;
    [SerializeField]
    private MindMapTreeData _mindMapTreeData;
    private MindMapNode selectedNode = null;

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

    private void SelectNode(string nodeName)
    {
        selectedNode = GetNode(nodeName);
    }
    #endregion

    #region MonoBehaviour
    private void Start()
    {
        LoadTree();
        BuildNodeRelation();
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
        MindMapNode mindMapNode = Instantiate(_mindMapNodePrefab);
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
            MindMapNode newNode = Instantiate(_mindMapNodePrefab);
            newNode.NodeName = _mindMapTreeData.NodeNames[i];
            newNode.NodeType = _mindMapTreeData.NodeTypes[i];
            newNode.NodeDescription = _mindMapTreeData.NodeDescriptions[i];
            newNode.AnimController = _mindMapTreeData.NodeAnimationControllers[i];

            if (_mindMapTreeData.ParentReferenceIdx[i] != -1)
            {
                newNode.Parent = nodeInstances[_mindMapTreeData.ParentReferenceIdx[i]];
            }
            else
            {
                _root = newNode;
            }

            newNode.name = newNode.NodeName + " node";
            newNode.transform.parent = transform;
            nodeInstances[i] = newNode;
        }

        BuildNodeRelation();
    }

    public void LoadTree(MindMapTreeData data)
    {
        _mindMapTreeData = data;
        LoadTree();
    }
    #endregion
}
