using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class MindMapTest : TestBase
{
    #region Variables
    private IMindMapBuilder _mindMapBuilder;
    private IMindMapTree _mindMapTree; 
    #endregion

    protected override void FindGameObjects(Scene scene)
    {
        GameObject[] gameObjects = scene.GetRootGameObjects();
        foreach (GameObject gameObject in gameObjects)
        {
            if (gameObject.name == "MindMapManager")
            {
                _mindMapBuilder = gameObject.transform.GetComponentInChildren<IMindMapBuilder>();
                _mindMapTree = gameObject.transform.GetComponentInChildren<IMindMapTree>();
            }
        }
    }

    #region Setup Teardown
    [SetUp]
    public override void SetUp()
    {
        sceneName = "MindMapTestScene";
        base.SetUp();
    }
    #endregion

    #region MindMapNodeStructure
    [UnityTest]
    public IEnumerator MindMap_ClearThenLoadMindMap()
    {
        _mindMapTree.ClearAllNodes();
        Assert.AreEqual(0, _mindMapTree.NodeCount);

        _mindMapTree.LoadTree();
        yield return null;
        Assert.IsTrue(_mindMapTree.IsNodeRelated());

        Queue<MindMapNode> queue = new Queue<MindMapNode>();
        queue.Enqueue(_mindMapTree.Root);

        while (queue.Count > 0)
        {
            MindMapNode currentNode = queue.Dequeue();
            switch (currentNode.NodeType)
            {
                case MindMapNodeType.CHAPTER:
                    Assert.AreEqual(null,currentNode.Parent);
                    break;
                case MindMapNodeType.CLUE:
                    Assert.AreEqual(0,currentNode.Children.Count);
                    break;
                case MindMapNodeType.CORE:
                    Assert.AreNotEqual(null, currentNode.Parent);
                    Assert.AreNotEqual(0, currentNode.Children.Count);
                    break;
            }
        }
        yield return null;
    }
    #endregion
}
