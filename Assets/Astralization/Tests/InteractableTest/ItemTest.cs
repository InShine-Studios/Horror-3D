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
            if (gameObject.name == "Player")
            {
                player = gameObject.transform.Find("Character").gameObject;
                itemHud = gameObject.GetComponentInChildren<IItemHudDisplay>();
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
    public IEnumerator PlayerItemDetector_ShowClosestItemIcon()
    {
        yield return new WaitWhile(() => sceneLoaded == false);
        yield return SimulateInput(KeyboardMouseTestFixture.RegisteredInput.MoveLeft, false, 1);
        GameObject overworldAnkh = GameObject.Find("OverworldItems/Ankh");
        GameObject overworldAnkh2 = GameObject.Find("OverworldItems/Ankh (3)");
        IInteractableItemMarker markItem = overworldAnkh.transform.GetComponentInChildren<IInteractableItemMarker>();
        IInteractableItemMarker markItem2 = overworldAnkh2.transform.GetComponentInChildren<IInteractableItemMarker>();
        Assert.True(markItem.IsEnabled());
        Assert.False(markItem2.IsEnabled());
        yield return SimulateInput(KeyboardMouseTestFixture.RegisteredInput.MoveForward, false, 0.25f);
        Assert.True(markItem2.IsEnabled());
        Assert.False(markItem.IsEnabled());
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
    public IEnumerator ItemHud_ExpandAndShrink()
    {
        yield return new WaitWhile(() => sceneLoaded == false);
        IInventory inventory = player.transform.Find("Rotate/InteractZone").GetComponent<IInventory>();
        // Expand Hud by pressing button
        yield return SimulateInput(KeyboardMouseTestFixture.RegisteredInput.ToggleItemHudDisplay);
        yield return new WaitWhile(() => itemHud.OnTransition);
        Assert.IsTrue(itemHud.IsExpanded);

        // Shrink Hud by pressing button
        yield return SimulateInput(KeyboardMouseTestFixture.RegisteredInput.ToggleItemHudDisplay);
        yield return new WaitUntil(() => !itemHud.OnTransition);
        Assert.IsFalse(itemHud.IsExpanded);

        // Keep Hud expanded when changing active item
        yield return SimulateInput(KeyboardMouseTestFixture.RegisteredInput.ToggleItemHudDisplay);
        yield return new WaitUntil(() => !itemHud.OnTransition);
        for (int i = 0; i < 5; i++)
        {
            inputTestFixture.Set("Scroll/Y", inventory.GetScrollStep());
            yield return null;
        }
        Assert.IsTrue(itemHud.IsExpanded);

        // Shrink Hud when idle for MaxIdleDuration seconds
        yield return new WaitForSeconds(itemHud.MaxIdleDuration);
        yield return new WaitWhile(() => itemHud.OnTransition);
        Assert.IsFalse(itemHud.IsExpanded);
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
            IItem currItem = inventory.GetItemByIndex(i);
            RuntimeAnimatorController logoAnimController = GameObject.Find("Player/HudCanvas/ItemHud/Slot " + (i+1)).GetComponent<Animator>()?.runtimeAnimatorController;
            if (i == 2)
            {
                Assert.IsNotNull(currItem);
                Assert.IsNotNull(logoAnimController);
            }
            else
            {
                Assert.IsNull(currItem);
                Assert.IsNull(logoAnimController);
            }
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
