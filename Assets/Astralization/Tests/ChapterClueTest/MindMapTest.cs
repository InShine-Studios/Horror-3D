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
            if (gameObject.name == "Player")
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

    #region MindMapNavigation
    [UnityTest]
    public IEnumerator MindMap_InitialNode()
    {
        // Initial select: Chapter node
        Assert.AreEqual(-1, _mindMapTree.GetCoreNodeIdx());
        Assert.AreEqual(-1, _mindMapTree.GetClueNodeIdx());
        Assert.AreEqual("CHAPTER 1 node", _mindMapTree.GetSelectedNode().name);
        yield return null;
    }

    [UnityTest]
    public IEnumerator MindMap_ChangeCore()
    {
        yield return new WaitForSeconds(1f); // wait for state change
        for (int i = 0; i < 4; i++)
        {
            yield return SimulateInput(KeyboardMouseTestFixture.RegisteredInput.ChangeCoreForward);
            yield return new WaitForSeconds(1f);
            Assert.AreEqual(i%3, _mindMapTree.GetCoreNodeIdx());
            Assert.AreEqual(-1, _mindMapTree.GetClueNodeIdx());
            Assert.AreEqual("Core " + ((i%3)+1) + " node", _mindMapTree.GetSelectedNode().name);
        }

        for (int i = 2; i >= 0; i--)
        {
            yield return SimulateInput(KeyboardMouseTestFixture.RegisteredInput.ChangeCoreBack);
            yield return new WaitForSeconds(1f);
            Assert.AreEqual(i, _mindMapTree.GetCoreNodeIdx());
            Assert.AreEqual(-1, _mindMapTree.GetClueNodeIdx());
            Assert.AreEqual("Core " + (i + 1) + " node", _mindMapTree.GetSelectedNode().name);
        }

        yield return null;
    }

    [UnityTest]
    public IEnumerator MindMap_ChangeCoreFirstForward()
    {
        yield return new WaitForSeconds(1f); // wait for state change
        yield return SimulateInput(KeyboardMouseTestFixture.RegisteredInput.ChangeCoreForward);
        yield return new WaitForSeconds(1f);
        Assert.AreEqual(0, _mindMapTree.GetCoreNodeIdx());
        Assert.AreEqual(-1, _mindMapTree.GetClueNodeIdx());
        yield return null;
    }

    [UnityTest]
    public IEnumerator MindMap_ChangeCoreFirstBackward()
    {
        yield return new WaitForSeconds(1f); // wait for state change
        yield return SimulateInput(KeyboardMouseTestFixture.RegisteredInput.ChangeCoreBack);
        yield return new WaitForSeconds(1f);
        Assert.AreEqual(0, _mindMapTree.GetCoreNodeIdx());
        Assert.AreEqual(-1, _mindMapTree.GetClueNodeIdx());
        Assert.AreEqual("Core 1 node", _mindMapTree.GetSelectedNode().name);
        yield return null;
    }

    [UnityTest]
    public IEnumerator MindMap_ChangeClueWhenCoreIsNotSelected()
    {
        yield return new WaitForSeconds(1f); // wait for state change
        yield return SimulateInput(KeyboardMouseTestFixture.RegisteredInput.ChangeClueForward);
        Assert.AreEqual(-1, _mindMapTree.GetCoreNodeIdx());
        Assert.AreEqual(-1, _mindMapTree.GetClueNodeIdx());
        Assert.AreEqual("CHAPTER 1 node", _mindMapTree.GetSelectedNode().name);

        yield return SimulateInput(KeyboardMouseTestFixture.RegisteredInput.ChangeClueBack);
        Assert.AreEqual(-1, _mindMapTree.GetCoreNodeIdx());
        Assert.AreEqual(-1, _mindMapTree.GetClueNodeIdx());
        Assert.AreEqual("CHAPTER 1 node", _mindMapTree.GetSelectedNode().name);

        yield return null;
    }

    [UnityTest]
    public IEnumerator MindMap_ChangeClueWhenCoreIsSelected()
    {
        yield return new WaitForSeconds(1f); // wait for state change
        yield return SimulateInput(KeyboardMouseTestFixture.RegisteredInput.ChangeCoreForward);
        yield return new WaitForSeconds(1f);
        Assert.AreEqual(0, _mindMapTree.GetCoreNodeIdx());

        int coreIdx = _mindMapTree.GetCoreNodeIdx() + 1;
        for (int i = 0; i < 4; i++)
        {
            yield return SimulateInput(KeyboardMouseTestFixture.RegisteredInput.ChangeClueForward);
            yield return new WaitForSeconds(1f);
            Assert.AreEqual(i % 3, _mindMapTree.GetClueNodeIdx());
            Assert.AreEqual("Clue " + coreIdx + "." + ((i % 3) + 1) + " node", _mindMapTree.GetSelectedNode().name);
        }

        for (int i = 2; i >= 0; i--)
        {
            yield return SimulateInput(KeyboardMouseTestFixture.RegisteredInput.ChangeClueBack);
            yield return new WaitForSeconds(1f);
            Assert.AreEqual(i, _mindMapTree.GetClueNodeIdx());
            Assert.AreEqual("Clue " + coreIdx + "." + (i + 1) + " node", _mindMapTree.GetSelectedNode().name);
        }

        yield return SimulateInput(KeyboardMouseTestFixture.RegisteredInput.ChangeCoreForward);
        yield return new WaitForSeconds(1f);
        Assert.AreEqual(1, _mindMapTree.GetCoreNodeIdx());
        coreIdx = _mindMapTree.GetCoreNodeIdx() + 1;

        for (int i = 0; i < 4; i++)
        {
            yield return SimulateInput(KeyboardMouseTestFixture.RegisteredInput.ChangeClueForward);
            yield return new WaitForSeconds(1f);
            Assert.AreEqual(i % 3, _mindMapTree.GetClueNodeIdx());
            Assert.AreEqual("Clue " + coreIdx + "." + ((i % 3) + 1) + " node", _mindMapTree.GetSelectedNode().name);
        }

        for (int i = 2; i >= 0; i--)
        {
            yield return SimulateInput(KeyboardMouseTestFixture.RegisteredInput.ChangeClueBack);
            yield return new WaitForSeconds(1f);
            Assert.AreEqual(i, _mindMapTree.GetClueNodeIdx());
            Assert.AreEqual("Clue " + coreIdx + "." + (i + 1) + " node", _mindMapTree.GetSelectedNode().name);
        }

        yield return null;
    }
    #endregion
}
