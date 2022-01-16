using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.TestTools;

class KeyBoardMouseTestFixture: InputTestFixture
{
    public enum RegisteredInput
    {
        Sprint,
        Interact,
        PickItem,
        DiscardItem,
        UseItem,
        ChangeItem
    }
    private Keyboard keyboard;
    private Mouse mouse;
    public Dictionary<RegisteredInput, KeyControl> keyboardInputMap;
    public Dictionary<RegisteredInput, ButtonControl> buttonInputMap;
    public Dictionary<RegisteredInput, AxisControl> axisInputMap;

    public override void Setup()
    {
        base.Setup();
        keyboard = InputSystem.AddDevice<Keyboard>();
        mouse = InputSystem.AddDevice<Mouse>();
        keyboardInputMap = new Dictionary<RegisteredInput, KeyControl>() {
            {RegisteredInput.Sprint, keyboard.leftShiftKey },
            {RegisteredInput.Interact, keyboard.eKey },
            {RegisteredInput.PickItem, keyboard.fKey },
            {RegisteredInput.DiscardItem, keyboard.gKey }
        };
        buttonInputMap = new Dictionary<RegisteredInput, ButtonControl>(){
            {RegisteredInput.UseItem, mouse.rightButton},
        };
        axisInputMap = new Dictionary<RegisteredInput, AxisControl>() {
            { RegisteredInput.ChangeItem, mouse.scroll.y }
        };
    }

    public override void TearDown()
    {
        InputSystem.RemoveDevice(keyboard);
        InputSystem.RemoveDevice(mouse);
        base.TearDown();
    }

    public void Press(RegisteredInput action)
    {
        if(keyboardInputMap.ContainsKey(action)) 
            Press(keyboardInputMap[action]);
        else
            Press(buttonInputMap[action]);
    }

    public void Release(RegisteredInput action)
    {
        if (keyboardInputMap.ContainsKey(action))
            Release(keyboardInputMap[action]);
        else
            Release(buttonInputMap[action]);
    }

    public void Set(string motionType, float value)
    {
        Set<float>(mouse, motionType, value);
    }
}

public class PlayerTest
{
    private const string sceneName = "TestScene";
    private bool sceneLoaded = false;
    private GameObject player;
    private KeyBoardMouseTestFixture inputTestFixture = new KeyBoardMouseTestFixture();

    #region Setup Teardown
    [SetUp]
    public void PlayerSetUp()
    {
        inputTestFixture.Setup();
        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.LoadScene(sceneName);
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        sceneLoaded = true;
        GameObject[] gameObjects = scene.GetRootGameObjects();
        foreach (GameObject gameObject in gameObjects)
        {
            if (gameObject.name == "Player") player = gameObject;
        }
    }

    [TearDown]
    public void PlayerTearDown()
    {
        inputTestFixture.TearDown();
        //SceneManager.UnloadSceneAsync(sceneName);
    }
    #endregion

    #region Item and Inventory
    [UnityTest]
    public IEnumerator PlayerItemDetector_PlayerInventory_PickAndUseDummyFlashlight()
    {
        yield return new WaitWhile(() => sceneLoaded == false);
        inputTestFixture.Press(KeyBoardMouseTestFixture.RegisteredInput.PickItem);
        yield return null;
        GameObject flashlight = player.transform.Find("Rotate/InteractZone/DummyFlashlight").gameObject;
        Assert.NotNull(flashlight);

        IInventory inventory = player.transform.Find("Rotate/InteractZone").GetComponent<IInventory>();
        Assert.AreEqual(1, inventory.GetNumOfItem());
        Assert.NotNull(inventory.GetActiveItem());
        Assert.AreEqual(0, inventory.GetActiveIdx());

        inputTestFixture.Press(KeyBoardMouseTestFixture.RegisteredInput.UseItem);
        yield return null;
        Assert.IsTrue(flashlight.GetComponentInChildren<Light>().enabled);
    }

    [UnityTest]
    public IEnumerator PlayerInventory_ScrollActiveItem()
    {
        yield return new WaitWhile(() => sceneLoaded == false);
        IInventory inventory = player.transform.Find("Rotate/InteractZone").GetComponent<IInventory>();
        int idxBefore = inventory.GetActiveIdx();
        inputTestFixture.Set("Scroll/Y",inventory.GetScrollStep());
        yield return null;

        Assert.AreEqual(idxBefore + 1, inventory.GetActiveIdx());
        Assert.AreEqual(inventory.GetItemByIndex(inventory.GetActiveIdx()), inventory.GetActiveItem());
    }
    #endregion

    #region Interactable
    [UnityTest]
    public IEnumerator PlayerInteractableDetector_InteractLightSwitch()
    {
        yield return new WaitWhile(() => sceneLoaded == false);
        inputTestFixture.Press(KeyBoardMouseTestFixture.RegisteredInput.Interact);
        yield return null;

        ILightSwitchController lightSwitchController = GameObject.Find("LightSwitch").GetComponent<ILightSwitchController>();
        Assert.IsTrue(lightSwitchController.GetState());
    }
    #endregion

    #region Sprint
    [UnityTest]
    public IEnumerator PlayerMovement_Sprint()
    {
        yield return new WaitWhile(() => sceneLoaded == false);
        inputTestFixture.Press(KeyBoardMouseTestFixture.RegisteredInput.Sprint);
        yield return new WaitForSeconds(0.3f);

        IPlayerMovement playerMovement = player.GetComponent<IPlayerMovement>();
        IPlayerBase playerBase = player.GetComponent<IPlayerBase>();
        Assert.IsTrue(playerMovement.GetSprintBool());
        Assert.AreEqual(playerBase.GetSprintSpeed(), playerMovement.GetMovementSpeed());

        inputTestFixture.Release(KeyBoardMouseTestFixture.RegisteredInput.Sprint);
        yield return new WaitForSeconds(0.3f);
        Assert.IsFalse(playerMovement.GetSprintBool());
        Assert.AreEqual(playerBase.GetPlayerMovementSpeed(), playerMovement.GetMovementSpeed());
    }
    #endregion
}
