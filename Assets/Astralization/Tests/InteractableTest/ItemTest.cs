using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using UnityEngine.UI;

public class ItemTest : TestBase
{
    protected IItemHudDisplay itemHud;
    protected GameObject overworldItem;

    protected override void FindGameObjects(Scene scene)
    {
        GameObject[] gameObjects = scene.GetRootGameObjects();
        foreach (GameObject gameObject in gameObjects)
        {
            if (gameObject.name == "Iris")
            {
                player = gameObject;
            }
            else if (gameObject.name == "UI")
            {
                itemHud = gameObject.transform.Find("ItemHud").GetComponent<IItemHudDisplay>();
            }
            else if (gameObject.name == "OverworldItems")
            {
                overworldItem = gameObject;
            }
        }
    }

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
        yield return SimulateInput(KeyboardMouseTestFixture.RegisteredInput.MoveForward, false, 0.1f);
        yield return SimulateInput(KeyboardMouseTestFixture.RegisteredInput.PickItem);
        GameObject flashlightOnPlayer = player.transform.Find("Rotate/InteractZone/DummyFlashlight").gameObject;
        Assert.NotNull(flashlightOnPlayer);
        Assert.Null(overworldItem.transform.Find("DummyFlashlight"));

        IInventory inventory = player.transform.Find("Rotate/InteractZone").GetComponent<IInventory>();
        Assert.AreEqual(1, inventory.GetNumOfItem());
        Assert.NotNull(inventory.GetActiveItem());
        Assert.AreEqual(0, inventory.GetActiveIdx());

        yield return SimulateInput(KeyboardMouseTestFixture.RegisteredInput.UseItem);
        Assert.IsTrue(flashlightOnPlayer.GetComponentInChildren<Light>().enabled);
        Image img = itemHud.GetSelectedItemLogo();
        Assert.IsTrue(img.enabled);
        Assert.AreEqual(flashlightOnPlayer.name, img.sprite.name);
        Assert.AreEqual(img, itemHud.GetItemLogo(0));

        yield return SimulateInput(KeyboardMouseTestFixture.RegisteredInput.DiscardItem);
        GameObject overworldFlashlight = GameObject.Find("OverworldItems/DummyFlashlight");
        Assert.NotNull(overworldFlashlight);
        Assert.Null(player.transform.Find("Rotate/InteractZone/DummyFlashlight"));
        Assert.IsFalse(itemHud.GetSelectedItemLogo().enabled);
        Assert.AreEqual(0, inventory.GetNumOfItem());
        Assert.IsNull(inventory.GetActiveItem());
    }

    [UnityTest]
    public IEnumerator PlayerItemDetector_ShowClosestItemIcon()
    {
        yield return new WaitWhile(() => sceneLoaded == false);
        yield return SimulateInput(KeyboardMouseTestFixture.RegisteredInput.MoveLeft, false, 1);
        GameObject overworldAnkh = GameObject.Find("OverworldItems/Ankh");
        GameObject overworldAnkh2 = GameObject.Find("OverworldItems/Ankh(2)");
        Transform markItem = overworldAnkh.transform.Find("ExclamationMarkItem");
        Transform markItem2 = overworldAnkh2.transform.Find("ExclamationMarkItem");
        Assert.True(markItem.gameObject.activeInHierarchy);
        Assert.False(markItem2.gameObject.activeInHierarchy);
        yield return SimulateInput(KeyboardMouseTestFixture.RegisteredInput.MoveForward, false, 0.25f);
        Assert.True(markItem2.gameObject.activeInHierarchy);
        Assert.False(markItem.gameObject.activeInHierarchy);
    }

    [UnityTest]
    public IEnumerator PlayerInventory_ScrollActiveItem()
    {
        yield return new WaitWhile(() => sceneLoaded == false);
        IInventory inventory = player.transform.Find("Rotate/InteractZone").GetComponent<IInventory>();
        int idxBefore = inventory.GetActiveIdx();
        inputTestFixture.Set("Scroll/Y", inventory.GetScrollStep());
        yield return null;

        int newIdx = Utils.MathCalcu.mod(idxBefore - 1, inventory.Size);
        Assert.AreEqual(newIdx, inventory.GetActiveIdx());
        Assert.AreEqual(inventory.GetItemByIndex(inventory.GetActiveIdx()), inventory.GetActiveItem());
        Assert.AreEqual(itemHud.GetSelectedItemLogo(), itemHud.GetItemLogo(newIdx));
    }

    [UnityTest]
    public IEnumerator PlayerInventory_InventoryQuickslot()
    {
        yield return new WaitWhile(() => sceneLoaded == false);
        IInventory inventory = player.transform.Find("Rotate/InteractZone").GetComponent<IInventory>();
        inventory.SetLength(5);
        KeyboardMouseTestFixture.RegisteredInput[] quickslots =
        {
            KeyboardMouseTestFixture.RegisteredInput.InventorySlot1,
            KeyboardMouseTestFixture.RegisteredInput.InventorySlot2,
            KeyboardMouseTestFixture.RegisteredInput.InventorySlot3,
            KeyboardMouseTestFixture.RegisteredInput.InventorySlot4,
            KeyboardMouseTestFixture.RegisteredInput.InventorySlot5
        };

        for (int i = 0; i < quickslots.Length; i++)
        {
            KeyboardMouseTestFixture.RegisteredInput quickslot = quickslots[i];
            yield return SimulateInput(quickslot);
            Assert.AreEqual(i, inventory.GetActiveIdx());
            Assert.AreEqual(inventory.GetItemByIndex(i), inventory.GetActiveItem());
        }
    }

    [UnityTest]
    public IEnumerator PlayerInventory_InventoryQuickslotOutOfRange()
    {
        yield return new WaitWhile(() => sceneLoaded == false);
        IInventory inventory = player.transform.Find("Rotate/InteractZone").GetComponent<IInventory>();
        inventory.SetLength(2);
        int prevActiveIdx = inventory.GetActiveIdx();
        yield return SimulateInput(KeyboardMouseTestFixture.RegisteredInput.InventorySlot3);
        Assert.AreEqual(prevActiveIdx, inventory.GetActiveIdx());
        Assert.AreEqual(inventory.GetItemByIndex(prevActiveIdx), inventory.GetActiveItem());
    }

    [UnityTest]
    public IEnumerator PlayerInventory_InventoryPlacementSequence()
    {
        yield return new WaitWhile(() => sceneLoaded == false);
        yield return SimulateInput(KeyboardMouseTestFixture.RegisteredInput.InventorySlot3);
        yield return SimulateInput(KeyboardMouseTestFixture.RegisteredInput.MoveForward, false, 0.1f);
        yield return SimulateInput(KeyboardMouseTestFixture.RegisteredInput.PickItem);
        IInventory inventory = player.transform.Find("Rotate/InteractZone").GetComponent<IInventory>();

        for (int i = 0; i < inventory.Size; i++)
        {
            bool isNull = inventory.GetItemByIndex(i) is null;
            if (i == 2) Assert.IsFalse(isNull);
            else Assert.IsTrue(isNull);
        }

        yield return SimulateInput(KeyboardMouseTestFixture.RegisteredInput.MoveLeft, false, 1f);
        yield return SimulateInput(KeyboardMouseTestFixture.RegisteredInput.PickItem);
        for (int i = 0; i < inventory.Size; i++)
        {
            bool isNull = inventory.GetItemByIndex(i) is null;
            if (i == 0 || i == 2) Assert.IsFalse(isNull);
            else Assert.IsTrue(isNull);
        }
    }
    #endregion
}
