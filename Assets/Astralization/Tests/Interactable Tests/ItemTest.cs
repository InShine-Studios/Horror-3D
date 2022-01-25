using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UI;

public class ItemTest : TestBase
{
    #region Setup Teardown
    [SetUp]
    public override void SetUp()
    {
        sceneName = "InteractableTestScene";
        base.SetUp();
    }
    #endregion

    #region Item and Inventory
    [UnityTest]
    public IEnumerator PlayerItemDetector_PlayerInventory_PickUseDiscardDummyFlashlight()
    {
        yield return new WaitWhile(() => sceneLoaded == false);
        yield return SimulateInput(KeyboardMouseTestFixture.RegisteredInput.PickItem);
        GameObject flashlight = player.transform.Find("Rotate/InteractZone/DummyFlashlight").gameObject;
        Assert.NotNull(flashlight);

        IInventory inventory = player.transform.Find("Rotate/InteractZone").GetComponent<IInventory>();
        Assert.AreEqual(1, inventory.GetNumOfItem());
        Assert.NotNull(inventory.GetActiveItem());
        Assert.AreEqual(0, inventory.GetActiveIdx());

        yield return SimulateInput(KeyboardMouseTestFixture.RegisteredInput.UseItem);
        Assert.IsTrue(flashlight.GetComponentInChildren<Light>().enabled);
        Image img = hud.transform.Find("ItemHud/Logo").GetComponent<Image>();
        Assert.IsTrue(img.enabled);
        Assert.AreEqual(flashlight.name, img.sprite.name);

        yield return SimulateInput(KeyboardMouseTestFixture.RegisteredInput.DiscardItem);
        GameObject overworldFlashlight = GameObject.Find("OverworldItems/DummyFlashlight");
        Assert.NotNull(overworldFlashlight);
        Assert.IsFalse(img.enabled);
        Assert.AreEqual(0, inventory.GetNumOfItem());
        Assert.IsNull(inventory.GetActiveItem());
    }

    [UnityTest]
    public IEnumerator PlayerInventory_ScrollActiveItem()
    {
        yield return new WaitWhile(() => sceneLoaded == false);
        IInventory inventory = player.transform.Find("Rotate/InteractZone").GetComponent<IInventory>();
        int idxBefore = inventory.GetActiveIdx();
        inputTestFixture.Set("Scroll/Y", inventory.GetScrollStep());
        yield return null;

        Assert.AreEqual(idxBefore + 1, inventory.GetActiveIdx());
        Assert.AreEqual(inventory.GetItemByIndex(inventory.GetActiveIdx()), inventory.GetActiveItem());
        Image img = hud.transform.Find("ItemHud/Logo").GetComponent<Image>();
        Assert.IsTrue(!img.enabled);
    }
    #endregion
}
