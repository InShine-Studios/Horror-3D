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

        IAstralMeterLogic astralMeterLogic = GameObject.Find("AstralMeter").GetComponent<IAstralMeterLogic>();
        IGameManager gameManager = GameObject.Find("GameManager").GetComponent<IGameManager>();

        yield return new WaitForSeconds(1.0f);
        Assert.IsTrue(gameManager.GetWorld());
        Assert.IsTrue(astralMeterLogic.GetAstralMeter() <= 10.0f);
    }
    #endregion
}
