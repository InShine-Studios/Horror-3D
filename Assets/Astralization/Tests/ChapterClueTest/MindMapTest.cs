using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;

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
        Assert.Equals(0, _mindMapTree.NodeCount);

        yield return null;
    }
    #endregion
}
