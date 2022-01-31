using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using UnityEngine.UI;

public class AstralWorldEntryTest : TestBase
{
    #region Setup Teardown
    [SetUp]
    public override void SetUp()
    {
        sceneName = "AstralWorldTestScene";
        base.SetUp();
    }
    #endregion

    #region Astral World
    [UnityTest]
    public IEnumerator AstralWorld_MoveToAstralWorld()
    {
        yield return new WaitWhile(() => sceneLoaded == false);
        IAstralMeterLogic astralMeterLogic = GameObject.Find("AstralMeter").GetComponent<IAstralMeterLogic>();
        Assert.IsTrue(astralMeterLogic.GetConstantRate() == 0.05f);

        GameObject ankhOW = GameObject.Find("OverworldItems/Ankh");
        inputTestFixture.Press(KeyboardMouseTestFixture.RegisteredInput.MoveLeft);
        float moveDuration = GetMovementDurationTowards(ankhOW.transform);
        yield return new WaitForSeconds(moveDuration);
        inputTestFixture.Release(KeyboardMouseTestFixture.RegisteredInput.MoveLeft);

        inputTestFixture.Press(KeyboardMouseTestFixture.RegisteredInput.PickItem);
        yield return null;
        GameObject ankh = player.transform.Find("Rotate/InteractZone/Ankh").gameObject;
        Assert.NotNull(ankh);

        IInventory inventory = player.transform.Find("Rotate/InteractZone").GetComponent<IInventory>();
        Assert.AreEqual(1, inventory.GetNumOfItem());
        Assert.NotNull(inventory.GetActiveItem());
        Assert.AreEqual(0, inventory.GetActiveIdx());

        inputTestFixture.Press(KeyboardMouseTestFixture.RegisteredInput.UseItem);
        yield return null;
        GameObject astral = GameObject.Find("VOL_Global_AstralWorld_1");
        Assert.IsTrue(astral.activeInHierarchy);

        GameObject realWorld = GameObject.Find("VOL_Global_RealWorld_1");
        for (int i = 0; i < realWorld.transform.childCount; i++)
        {
            GameObject go = realWorld.transform.GetChild(i).gameObject;
            Assert.IsFalse(go.activeInHierarchy);
        }

        IGameManager gameManager = GameObject.Find("GameManager").GetComponent<IGameManager>();

        Assert.IsTrue(gameManager.GetWorld());
        Assert.IsTrue(astralMeterLogic.GetConstantRate() == 0.083f);
    }
    #endregion
}
