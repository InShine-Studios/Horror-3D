using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ExorcismTest : TestBase
{
    protected GameObject hud;

    protected override void FindGameObjects(Scene scene)
    {
        GameObject[] gameObjects = scene.GetRootGameObjects();
        foreach (GameObject gameObject in gameObjects)
        {
            if (gameObject.name == "Iris")
            {
                player = gameObject;
                playerMovement = player.GetComponent<IPlayerMovement>();
            }
            else if (gameObject.name == "Canvas")
            {
                hud = gameObject;
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
        //Debug.Log("[TEST EXORCISM] Exorcism Overworld Item Found");
        float moveDuration = GetMovementDurationTowards(exorcismItemOW.transform);
        yield return SimulateInput(KeyboardMouseTestFixture.RegisteredInput.MoveRight, false, moveDuration);
        yield return SimulateInput(KeyboardMouseTestFixture.RegisteredInput.PickItem);

        yield return null;
        GameObject exorcismItemObj = player.transform.Find("Rotate/InteractZone/ExorcismItem").gameObject;
        GameObject exorcismBarObj = hud.transform.Find("ExorcismHud").gameObject;
        GameObject exorcismSliderObj = hud.transform.Find("ExorcismHud/Slider").gameObject;
        Assert.NotNull(exorcismItemObj);

        IExorcismItem exorcismItem = exorcismItemObj.GetComponent<IExorcismItem>();
        IExorcismBar exorcismBar = exorcismBarObj.GetComponent<IExorcismBar>();

        IInventory inventory = player.transform.Find("Rotate/InteractZone").GetComponent<IInventory>();
        Assert.AreEqual(1, inventory.GetNumOfItem());
        Assert.NotNull(inventory.GetActiveItem());
        Assert.AreEqual(0, inventory.GetActiveIdx());

        inputTestFixture.Press(KeyboardMouseTestFixture.RegisteredInput.UseItem);
        yield return new WaitForSeconds(0.3f);
        Assert.IsTrue(exorcismItem.GetUsed());
        Assert.IsTrue(exorcismSliderObj.activeSelf);
        Assert.AreEqual(exorcismItem.GetAccTime(), exorcismBar.GetSliderValue());

        yield return new WaitForSeconds(5.0f);
        inputTestFixture.Release(KeyboardMouseTestFixture.RegisteredInput.UseItem);
        yield return new WaitForSeconds(0.3f);
        Assert.IsFalse(exorcismItem.GetUsed());
        Assert.IsFalse(exorcismSliderObj.activeSelf);
    }

    [UnityTest]
    public IEnumerator ExorcismItem_ExorcismRitualCancel()
    {
        yield return new WaitWhile(() => sceneLoaded == false);
        GameObject exorcismItemOW = GameObject.Find("OverworldItems/ExorcismItem");
        //Debug.Log("[TEST EXORCISM] Exorcism Overworld Item Found");
        float moveDuration = GetMovementDurationTowards(exorcismItemOW.transform);
        yield return SimulateInput(KeyboardMouseTestFixture.RegisteredInput.MoveRight, false, moveDuration);
        yield return SimulateInput(KeyboardMouseTestFixture.RegisteredInput.PickItem);

        yield return null;
        GameObject exorcismItemObj = player.transform.Find("Rotate/InteractZone/ExorcismItem").gameObject;
        GameObject exorcismBarObj = hud.transform.Find("ExorcismHud").gameObject;
        GameObject exorcismSliderObj = hud.transform.Find("ExorcismHud/Slider").gameObject;
        Assert.NotNull(exorcismItemObj);

        IExorcismItem exorcismItem = exorcismItemObj.GetComponent<IExorcismItem>();
        IExorcismBar exorcismBar = exorcismBarObj.GetComponent<IExorcismBar>();

        IInventory inventory = player.transform.Find("Rotate/InteractZone").GetComponent<IInventory>();
        Assert.AreEqual(1, inventory.GetNumOfItem());
        Assert.NotNull(inventory.GetActiveItem());
        Assert.AreEqual(0, inventory.GetActiveIdx());

        inputTestFixture.Press(KeyboardMouseTestFixture.RegisteredInput.UseItem);
        yield return new WaitForSeconds(0.3f);
        Assert.IsTrue(exorcismItem.GetUsed());
        Assert.IsTrue(exorcismSliderObj.activeSelf);
        Assert.AreEqual(exorcismItem.GetAccTime(), exorcismBar.GetSliderValue());

        yield return new WaitForSeconds(2.0f);
        inputTestFixture.Release(KeyboardMouseTestFixture.RegisteredInput.UseItem);
        yield return new WaitForSeconds(0.3f);
        Assert.IsFalse(exorcismItem.GetUsed());
        Assert.IsFalse(exorcismSliderObj.activeSelf);
    }
    #endregion
}
