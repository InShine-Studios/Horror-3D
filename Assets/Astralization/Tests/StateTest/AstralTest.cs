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
    //[UnityTest]
    //public IEnumerator AstralMeter_RealWorldAccumulation()
    //{
    //    yield return new WaitWhile(() => sceneLoaded == false);
    //    IAstralMeterLogic astralMeterLogic = party.GetComponent<IAstralMeterLogic>();

    //    yield return new WaitForSeconds(1.0f);
    //    Assert.IsTrue(astralMeterLogic.IsOnRealWorld());
    //    Assert.AreEqual(3.0f, astralMeterLogic.GetAstralMeter() * 60, 1.0f);
    //}

    //[UnityTest]
    //public IEnumerator AstralMeter_AstralWorldAccumulation()
    //{
    //    yield return new WaitWhile(() => sceneLoaded == false);
    //    IAstralMeterLogic astralMeterLogic = party.GetComponent<IAstralMeterLogic>();
    //    astralMeterLogic.ChangeWorld();

    //    yield return new WaitForSeconds(1.0f);
    //    Assert.IsFalse(astralMeterLogic.IsOnRealWorld());
    //    Assert.AreEqual(5.0f, astralMeterLogic.GetAstralMeter() * 60, 1.0f);
    //}

    //[UnityTest]
    //public IEnumerator AstralMeter_OnSightAccumulation()
    //{
    //    yield return new WaitWhile(() => sceneLoaded == false);
    //    IAstralMeterLogic astralMeterLogic = party.GetComponent<IAstralMeterLogic>();
    //    astralMeterLogic.ChangeWorld();
    //    astralMeterLogic.ChangeSightState();

    //    yield return new WaitForSeconds(1.0f);
    //    Assert.IsFalse(astralMeterLogic.IsOnRealWorld());
    //    Assert.AreEqual(1.0f, astralMeterLogic.GetAstralMeter(), 1.0f);
    //}

    [UnityTest]
    public IEnumerator AstralMeter_NPCWrongAnswer()
    {
        yield return new WaitWhile(() => sceneLoaded == false);
        IAstralMeterLogic astralMeterLogic = GameObject.Find("AstralMeter").GetComponent<IAstralMeterLogic>();
        //astralMeterLogic.ChangeWorld();
        astralMeterLogic.NPCWrongAnswer();

        //Assert.IsFalse(astralMeterLogic.IsOnRealWorld());
        Assert.IsTrue(astralMeterLogic.GetAstralMeter() >= 10.0f);
        Assert.IsTrue(astralMeterLogic.GetAstralMeter() <= 15.0f);
    }

    [UnityTest]
    public IEnumerator AstralMeter_PlayerKilled()
    {
        yield return new WaitWhile(() => sceneLoaded == false);
        IAstralMeterLogic astralMeterLogic = GameObject.Find("AstralMeter").GetComponent<IAstralMeterLogic>();
        //astralMeterLogic.ChangeWorld();
        astralMeterLogic.PlayerKilled();

        //Assert.IsFalse(astralMeterLogic.IsOnRealWorld());
        Assert.IsTrue(astralMeterLogic.GetAstralMeter() >= 15.0f);
        Assert.IsTrue(astralMeterLogic.GetAstralMeter() <= 20.0f);
    }
    #endregion
}
