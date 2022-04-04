using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using UnityEngine.UI;

public class AstralTest : TestBase
{
    protected override void FindGameObjects(Scene scene)
    {
        GameObject[] gameObjects = scene.GetRootGameObjects();
        foreach (GameObject gameObject in gameObjects)
        {
            if (gameObject.name == "Iris")
            {
                player = gameObject;
            }
        }
    }

    #region Setup Teardown
    [SetUp]
    public override void SetUp()
    {
        sceneName = "AstralTestScene";
        base.SetUp();
    }
    #endregion

    #region Astral World
    [UnityTest]
    public IEnumerator AstralMeter_VictimWrongAnswer()
    {
        yield return new WaitWhile(() => sceneLoaded == false);
        IAstralMeterLogic astralMeterLogic = GameObject.Find("WorldState").GetComponent<IAstralMeterLogic>();
        astralMeterLogic.VictimWrongAnswer();

        Assert.IsTrue(astralMeterLogic.GetCurrentMeter() >= 10.0f);
        Assert.IsTrue(astralMeterLogic.GetCurrentMeter() <= 15.0f);
    }

    [UnityTest]
    public IEnumerator AstralMeter_PlayerKilled()
    {
        yield return new WaitWhile(() => sceneLoaded == false);
        IAstralMeterLogic astralMeterLogic = GameObject.Find("WorldState").GetComponent<IAstralMeterLogic>();
        astralMeterLogic.PlayerKilled();

        Assert.IsTrue(astralMeterLogic.GetCurrentMeter() >= 15.0f);
        Assert.IsTrue(astralMeterLogic.GetCurrentMeter() <= 20.0f);
    }
    #endregion
}
