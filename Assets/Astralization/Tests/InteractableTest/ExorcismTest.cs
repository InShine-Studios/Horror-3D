using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ExorcismTest : TestBase
{
    IExorcismBar exorcismBar;
    GameObject exorcismSliderObj;

    protected override void FindGameObjects(Scene scene)
    {
        GameObject[] gameObjects = scene.GetRootGameObjects();
        foreach (GameObject gameObject in gameObjects)
        {
            if (gameObject.name == "Player")
            {
                player = gameObject.transform.Find("Character").gameObject;
                playerMovement = player.GetComponent<IPlayerMovement>();
                exorcismBar = gameObject.transform.GetComponentInChildren<IExorcismBar>();
                exorcismSliderObj = gameObject.transform.Find(UiCanvas + "/ExorcismHud/Slider").gameObject;
            }
        }
    }

    #region Setup Teardown
    [SetUp]
    public override void SetUp()
    {
        sceneName = "ExorcismTestScene";
        base.SetUp();
    }
    #endregion

    #region Exorcism Item
    [UnityTest]
    public IEnumerator ExorcismItem_ExorcismRitualSuccess()
    {
        yield return new WaitWhile(() => sceneLoaded == false);
        GameObject exorcismItemOW = GameObject.Find("OverworldItems/ExorcismItem");
        float moveDuration = GetMovementDurationTowards(exorcismItemOW.transform);
        yield return SimulateInput(KeyboardMouseTestFixture.RegisteredInput.MoveRight, false, moveDuration);
        yield return SimulateInput(KeyboardMouseTestFixture.RegisteredInput.PickItem);

        yield return null;
        GameObject exorcismItemObj = player.transform.Find("Rotate/InteractZone/ExorcismItem").gameObject;
        Assert.NotNull(exorcismItemObj);

        IInventory inventory = player.transform.Find("Rotate/InteractZone").GetComponent<IInventory>();
        Assert.AreEqual(1, inventory.GetNumOfItem());
        Assert.AreEqual("ExorcismItem", inventory.GetActiveItemName());
        Assert.AreEqual(0, inventory.GetActiveIdx());

        inputTestFixture.Press(KeyboardMouseTestFixture.RegisteredInput.UseItem);
        yield return new WaitForSeconds(0.3f);
        Assert.IsTrue(exorcismBar.IsUsed());
        Assert.IsTrue(exorcismSliderObj.activeSelf);
        Assert.AreEqual(exorcismBar.GetCooldownHelper().GetAccumulatedTime(), exorcismBar.GetSliderValue());

        yield return new WaitForSeconds(5.0f);
        inputTestFixture.Release(KeyboardMouseTestFixture.RegisteredInput.UseItem);
        yield return new WaitForSeconds(0.3f);
        Assert.IsFalse(exorcismBar.IsUsed());
        Assert.IsFalse(exorcismSliderObj.activeSelf);
        Assert.IsTrue(exorcismBar.IsExorcised());
    }

    [UnityTest]
    public IEnumerator ExorcismItem_ExorcismRitualCancel()
    {
        yield return new WaitWhile(() => sceneLoaded == false);
        GameObject exorcismItemOW = GameObject.Find("OverworldItems/ExorcismItem");
        float moveDuration = GetMovementDurationTowards(exorcismItemOW.transform);
        yield return SimulateInput(KeyboardMouseTestFixture.RegisteredInput.MoveRight, false, moveDuration);
        yield return SimulateInput(KeyboardMouseTestFixture.RegisteredInput.PickItem);

        yield return null;
        GameObject exorcismItemObj = player.transform.Find("Rotate/InteractZone/ExorcismItem").gameObject;
        Assert.NotNull(exorcismItemObj);

        IInventory inventory = player.transform.Find("Rotate/InteractZone").GetComponent<IInventory>();
        Assert.AreEqual(1, inventory.GetNumOfItem());
        Assert.AreEqual("ExorcismItem", inventory.GetActiveItemName());
        Assert.AreEqual(0, inventory.GetActiveIdx());

        inputTestFixture.Press(KeyboardMouseTestFixture.RegisteredInput.UseItem);
        yield return new WaitForSeconds(0.3f);
        Assert.IsTrue(exorcismBar.IsUsed());
        Assert.IsTrue(exorcismSliderObj.activeSelf);
        Assert.AreEqual(exorcismBar.GetCooldownHelper().GetAccumulatedTime(), exorcismBar.GetSliderValue());

        yield return new WaitForSeconds(2.0f);
        inputTestFixture.Release(KeyboardMouseTestFixture.RegisteredInput.UseItem);
        yield return new WaitForSeconds(0.3f);
        Assert.IsFalse(exorcismBar.IsUsed());
        Assert.IsFalse(exorcismSliderObj.activeSelf);
        Assert.IsFalse(exorcismBar.IsExorcised());
    }

    [UnityTest]
    public IEnumerator ExorcismItem_CantUseInAstralWorld()
    {
        yield return new WaitWhile(() => sceneLoaded == false);
        GameObject exorcismItemOW = GameObject.Find("OverworldItems/ExorcismItem");
        float moveDuration = GetMovementDurationTowards(exorcismItemOW.transform);
        yield return SimulateInput(KeyboardMouseTestFixture.RegisteredInput.MoveRight, false, moveDuration);

        GameObject ankhItemOw = GameObject.Find("OverworldItems/Ankh");
        moveDuration = GetMovementDurationTowards(ankhItemOw.transform);
        yield return SimulateInput(KeyboardMouseTestFixture.RegisteredInput.MoveForward, false, moveDuration);
        yield return SimulateInput(KeyboardMouseTestFixture.RegisteredInput.PickItem);
        yield return SimulateInput(KeyboardMouseTestFixture.RegisteredInput.UseItem);
        yield return SimulateInput(KeyboardMouseTestFixture.RegisteredInput.DiscardItem);

        yield return SimulateInput(KeyboardMouseTestFixture.RegisteredInput.MoveBack, false, moveDuration);
        yield return SimulateInput(KeyboardMouseTestFixture.RegisteredInput.PickItem);
        yield return SimulateInput(KeyboardMouseTestFixture.RegisteredInput.UseItem);

        GameObject exorcismItemObj = player.transform.Find("Rotate/InteractZone/ExorcismItem").gameObject;
        GameObject hud = GameObject.Find("Player/UiCanvas").gameObject;
        GameObject exorcismBarObj = hud.transform.Find("ExorcismHud").gameObject;
        GameObject exorcismSliderObj = hud.transform.Find("ExorcismHud/Slider").gameObject;
        IExorcismBar exorcismBar = exorcismBarObj.GetComponent<IExorcismBar>();

        Assert.IsFalse(exorcismBar.IsUsed());
        Assert.IsFalse(exorcismSliderObj.activeSelf);
        Assert.IsFalse(exorcismBar.IsExorcised());
    }
    #endregion
}
