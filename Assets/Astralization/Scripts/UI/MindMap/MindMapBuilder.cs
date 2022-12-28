using System.Collections.Generic;
using UnityEngine;

public interface IMindMapBuilder
{
    void AddNode();
    void ClearChild();
    string Load();
    void Save(string filename);
}


/**
 * Class to build mind map with unity scene viewer and inspector.
 */
[RequireComponent(typeof(MindMapTree))]
[ExecuteInEditMode]
public class MindMapBuilder : MonoBehaviour, IMindMapBuilder
{
    #region Const
    private readonly string[] DirectoryPathSequence = new string[] { "Resources", "Stages", "ClueTree" };
    #endregion

    #region Variables
    [Header("MindMapData")]
    [SerializeField]
    private MindMapTreeData _mindMapTreeData;

    private MindMapTree _mindMapTree;
    #endregion

    #region MonoBehaviour
    private void Awake()
    {
        _mindMapTree = GetComponent<MindMapTree>();
    }
    #endregion

    #region Helper
    private int CountRootNodes()
    {
        int rootNodeNum = 0;
        MindMapNode[] mindMapNodes = GetComponentsInChildren<MindMapNode>();
        foreach (MindMapNode mindMapNode in mindMapNodes)
        {
            if (mindMapNode.Parent == null) rootNodeNum++;
        }
        return rootNodeNum;
    }

    private void SaveTree(string filename)
    {
        MindMapTreeData mindMapTreeData = ScriptableObject.CreateInstance<MindMapTreeData>();
        Dictionary<string, int> nodeIdxDict = new Dictionary<string, int>();
        int nodeIdxAccumulator = 0;

        Queue<MindMapNode> queue = new Queue<MindMapNode>();
        queue.Enqueue(_mindMapTree.Root);

        while (queue.Count > 0)
        {
            MindMapNode currentNode = queue.Dequeue();
            nodeIdxDict.Add(currentNode.NodeName, nodeIdxAccumulator++);

            int parentIdx = currentNode.Parent != null ? nodeIdxDict[currentNode.Parent.NodeName] : -1;
            mindMapTreeData.ParentReferenceIdx.Add(parentIdx);
            mindMapTreeData.NodeNames.Add(currentNode.NodeName);
            mindMapTreeData.NodeDescriptions.Add(currentNode.NodeDescription);
            mindMapTreeData.NodeTypes.Add(currentNode.NodeType);
            mindMapTreeData.NodeAnimationControllers.Add(currentNode.AnimController);
            mindMapTreeData.NodePositions.Add(currentNode.transform.position);
            mindMapTreeData.NodeRotations.Add(currentNode.transform.rotation);
            mindMapTreeData.NodeScales.Add(currentNode.transform.localScale);
            mindMapTreeData.NodeCameraFollowPosition.Add(currentNode.GetCameraFollow().localPosition) ;
            mindMapTreeData.NodeCameraLookAtPosition.Add(currentNode.GetCameraLookAt().localPosition);

            foreach (MindMapNode child in currentNode.Children)
            {
                queue.Enqueue(child);
            }
        }

        if (filename == "") filename = "DummyTree";
        Utils.FileSystemHelper.CreateAsset(mindMapTreeData, DirectoryPathSequence, filename + ".asset");
        _mindMapTreeData = mindMapTreeData;
        Debug.Log("[MIND MAP BUILDER] Mind map tree is saved. " + 
            filename + ".asset is saved in " + Utils.FileSystemHelper.CombinePath(DirectoryPathSequence));
    }
    #endregion

    public void AddNode()
    {
        _mindMapTree.AddNode();
    }

    public void ClearChild()
    {
        _mindMapTree.ClearAllNodes();
    }

    public void Save(string filename)
    {
        if (CountRootNodes() > 1)
        {
            Debug.LogError("[MIND MAP BUILDER] Root node can't be more than one. " +
                "Check parent node assignment on every node to ensure only one node that has no parent.");
            return;
        }
        if (_mindMapTree.Root == null)
        {
            Debug.LogError("[MIND MAP BUILDER] Root node of MindMapTree has not been assigned.");
            return;
        }

        Utils.FileSystemHelper.CreateDirectories(DirectoryPathSequence);
        _mindMapTree.BuildNodeRelation();
        SaveTree(filename);
    }

    public string Load()
    {
        if (_mindMapTreeData == null)
        {
            Debug.LogError("[MIND MAP BUILDER] Mind map tree data has not been assigned");
            return null;
        }
        if (_mindMapTree == null) _mindMapTree = GetComponent<MindMapTree>();

        _mindMapTree.LoadTree(_mindMapTreeData);

        return _mindMapTreeData.name;
    }
}
