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

    void AddNode();
    void BuildNodeRelation();
    void ChangeClue(int indexStep);
    void ChangeCore(int indexStep);
    void ClearAllNodes();
    int GetClueNodeIdx();
    int GetCoreNodeIdx();
    MindMapNode GetSelectedNode();
    bool IsNodeRelated();
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
public class MindMapTree : MonoBehaviour, IMindMapTree
{
    #region Event
    public static event Action<MindMapNode> SetNodeInfo;
    public static event Action<bool> ActivatedModal;
    #endregion

    #region Const
    public static string LayerName = "MindMap";
    #endregion

    #region Variables
    [Header("Prefabs")]
    [SerializeField]
    private MindMapNode _mindMapNodeBase;
    [SerializeField]
    private NodeModelDictionary _nodeModelDictionary;
    private MindMapPooler _pooler;

    [Header("Tree Data")]
    [SerializeField]
    private MindMapNode _root;
    [SerializeField]
    [Tooltip("Mind Map Node data SO. The first data is the default menu")]
    private MindMapTreeData[] _mindMapTreeData;
    private MindMapTreeData _currentMindMapTreeData;

    [Header("Camera and Navigation")]
    [SerializeField]
    private MindMapCameraManager _mindMapCameraManager; // TODO: make this non-serializable and integrate with UiState

    private int chapterIdx;
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

    public MindMapNode GetSelectedNode()
    {
        return selectedNode;
    }

    public int GetCoreNodeIdx()
    {
        return coreNodeIdx;
    }

    public int GetClueNodeIdx()
    {
        return clueNodeIdx;
    }

    public void FocusOnRoot()
    {
        coreNodeIdx = -1;
        clueNodeIdx = -1;
        SetCameraFocus(_root);
    }

    private int GetMaxNodeCount()
    {
        int maxNodeCount = 0;
        foreach(MindMapTreeData treeData in _mindMapTreeData)
        {
            maxNodeCount = Math.Max(maxNodeCount, treeData.NodeNames.Count);
        }

        return maxNodeCount;
    }
    #endregion

    #region MonoBehaviour
    private void Start()
    {
        ClearAllNodes();

        // Initialize pooler
        _pooler = gameObject.AddComponent<MindMapPooler>();
        _pooler.Initialize(_mindMapNodeBase, GetMaxNodeCount());

        chapterIdx = 0;
        _currentMindMapTreeData = _mindMapTreeData[chapterIdx];
        LoadTree();
        FocusOnRoot();
        SendNodeActiveInfo(true);
        SendNodeInfo(_root);
    }

    private void OnEnable()
    {
        NodeNavigation.ChangeNodeEvent += ChangeNodeFromButton;
        ChapterNavigation.ChangeChapterEvent += ChangeChapterFromButton;
    }

    private void OnDisable()
    {
        NodeNavigation.ChangeNodeEvent -= ChangeNodeFromButton;
        ChapterNavigation.ChangeChapterEvent -= ChangeChapterFromButton;

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
        MindMapNode mindMapNode;
        _pooler.GetInstance(out mindMapNode);
        mindMapNode.transform.parent = transform;
    }

    public void LoadTree()
    {
        if (_mindMapTreeData == null)
        {
            Debug.LogError("[MIND MAP BUILDER] Mind map tree data has not been assigned");
            return;
        }

        int nodeCount = _currentMindMapTreeData.ParentReferenceIdx.Count;
        MindMapNode[] nodeInstances = new MindMapNode[nodeCount];

        for (int i = 0; i < nodeCount; i++)
        {
            MindMapNode newNode;
            _pooler.GetInstance(out newNode);
            // General assignments
            newNode.NodeName = _currentMindMapTreeData.NodeNames[i];
            newNode.NodeDescription = _currentMindMapTreeData.NodeDescriptions[i];
            newNode.AnimController = _currentMindMapTreeData.NodeAnimationControllers[i];
            
            // Type-related assignments
            newNode.NodeType = _currentMindMapTreeData.NodeTypes[i];
            GameObject nodeModel = Instantiate(_nodeModelDictionary[newNode.NodeType]);
            nodeModel.transform.parent = newNode.transform;
            nodeModel.gameObject.layer = LayerMask.NameToLayer(LayerName);

            // Parent reference assignments
            if (_currentMindMapTreeData.ParentReferenceIdx[i] != -1)
            {
                newNode.Parent = nodeInstances[_currentMindMapTreeData.ParentReferenceIdx[i]];
            }
            else
            {
                _root = newNode;
            }

            // Transform assignments
            newNode.transform.parent = transform;
            newNode.transform.position = _currentMindMapTreeData.NodePositions[i];
            newNode.transform.rotation = _currentMindMapTreeData.NodeRotations[i];
            newNode.transform.localScale = _currentMindMapTreeData.NodeScales[i];
            newNode.SetCameraFollowPosition(_currentMindMapTreeData.NodeCameraFollowPosition[i]);
            newNode.SetCameraLookAtPosition(_currentMindMapTreeData.NodeCameraLookAtPosition[i]);

            newNode.gameObject.layer = LayerMask.NameToLayer(LayerName);

            newNode.name = newNode.NodeName + " node";
            nodeInstances[i] = newNode;
        }

        BuildNodeRelation();
    }

    public void LoadTree(MindMapTreeData data)
    {
        _currentMindMapTreeData = data;
        LoadTree();
    }

    private void ChangeNodeFromButton(int jumpIdx)
    {
        switch (selectedNode.NodeType)
        {
            case MindMapNodeType.CORE:
                ChangeCore(jumpIdx);
                break;
            case MindMapNodeType.CLUE:
                ChangeClue(jumpIdx);
                break;
            default:
                break;
        }
    }

    private void ChangeChapterFromButton(int jumpIdx)
    {
        ActivateCamera(false);
        chapterIdx = Mathf.Clamp(chapterIdx + jumpIdx, 0, _mindMapTreeData.Length - 1);
        coreNodeIdx = 0;
        clueNodeIdx = 0;
        _currentMindMapTreeData = _mindMapTreeData[chapterIdx];

        foreach (MindMapNode mindMapNode in GetComponentsInChildren<MindMapNode>())
        {
            _pooler.ReturnObjectToPool(mindMapNode);
        }

        LoadTree();

        SetCameraFocus(Root);
        ActivateCamera(true);
        SendNodeInfo(Root);
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
        StartCoroutine(ModalTransitioning());
        SendNodeInfo(Root.Children[coreNodeIdx]);
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
        StartCoroutine(ModalTransitioning());
        SendNodeInfo(Root.Children[coreNodeIdx].Children[clueNodeIdx]);
    }
    #endregion

    #region Camera Manipulation
    private void SetCameraFocus(MindMapNode node)
    {
        _mindMapCameraManager.FocusOn(node.GetCameraFollow(), node.GetCameraLookAt());
        selectedNode = node;
    }

    private void ActivateCamera(bool isActive)
    {
        _mindMapCameraManager.Enable(isActive);
    }
    #endregion

    #region Send Event
    public void SendNodeInfo(MindMapNode node)
    {
        SetNodeInfo?.Invoke(node);
    }

    public void SendNodeActiveInfo(bool is_active)
    {
        ActivatedModal?.Invoke(is_active);
    }

    private IEnumerator ModalTransitioning()
    {
        float _cameraBlendTime = _mindMapCameraManager.GetCameraBlendTime();
        SendNodeActiveInfo(false);
        yield return new WaitForSeconds(_cameraBlendTime);
        SendNodeActiveInfo(true);
    }
    #endregion
}
