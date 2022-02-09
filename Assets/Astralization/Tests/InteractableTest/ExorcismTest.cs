using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UI;

public class ExorcismTest : TestBase
{
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
    public IEnumerator ExorcismItem_ExorcismRitual()
    {
        yield return new WaitWhile(() => sceneLoaded == false);
        GameObject exorcismItemOW = GameObject.Find("OverworldItems/ExorcismItem");
        float moveDuration = GetMovementDurationTowards(exorcismItemOW.transform);
        yield return SimulateInput(KeyboardMouseTestFixture.RegisteredInput.MoveRight, false, moveDuration);
        yield return SimulateInput(KeyboardMouseTestFixture.RegisteredInput.PickItem);

        yield return null;
        GameObject exorcismItem = player.transform.Find("Rotate/InteractZone/ExorcismItem").gameObject;
        Assert.NotNull(exorcismItem);

        IInventory inventory = player.transform.Find("Rotate/InteractZone").GetComponent<IInventory>();
        Assert.AreEqual(1, inventory.GetNumOfItem());
        Assert.NotNull(inventory.GetActiveItem());
        Assert.AreEqual(0, inventory.GetActiveIdx());

        yield return SimulateInput(KeyboardMouseTestFixture.Press(KeyboardMouseTestFixture.RegisteredInput.UseItem), false, 5.0f);
        Assert.IsTrue(exorcismItem.GetComponentInChildren<Light>().enabled);
        Image img = hud.transform.Find("ItemHud/Logo").GetComponent<Image>();
        Assert.IsTrue(img.enabled);
        Assert.AreEqual(exorcismItem.name, img.sprite.name);



        yield return SimulateInput(KeyboardMouseTestFixture.RegisteredInput.DiscardItem);
        GameObject overworldFlashlight = GameObject.Find("OverworldItems/DummyFlashlight");
        Assert.NotNull(overworldFlashlight);
        Assert.IsFalse(img.enabled);
        Assert.AreEqual(0, inventory.GetNumOfItem());
        Assert.IsNull(inventory.GetActiveItem());
    }
    #endregion
}
