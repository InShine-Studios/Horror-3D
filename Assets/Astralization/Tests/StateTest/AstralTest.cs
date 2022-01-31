using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using UnityEngine.UI;

public class AstralTest : TestBase
{
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
    public IEnumerator AstralMeter_NPCWrongAnswer()
    {
        yield return new WaitWhile(() => sceneLoaded == false);
        IAstralMeterLogic astralMeterLogic = GameObject.Find("AstralMeter").GetComponent<IAstralMeterLogic>();
        astralMeterLogic.NPCWrongAnswer();

        Assert.IsTrue(astralMeterLogic.GetAstralMeter() >= 10.0f);
        Assert.IsTrue(astralMeterLogic.GetAstralMeter() <= 15.0f);
    }

    [UnityTest]
    public IEnumerator AstralMeter_PlayerKilled()
    {
        yield return new WaitWhile(() => sceneLoaded == false);
        IAstralMeterLogic astralMeterLogic = GameObject.Find("AstralMeter").GetComponent<IAstralMeterLogic>();
        astralMeterLogic.PlayerKilled();

        Assert.IsTrue(astralMeterLogic.GetAstralMeter() >= 15.0f);
        Assert.IsTrue(astralMeterLogic.GetAstralMeter() <= 20.0f);
    }
    #endregion
}
