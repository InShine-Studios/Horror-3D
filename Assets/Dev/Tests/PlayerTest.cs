using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.TestTools;

class KeyboardMouseTestFixture: InputTestFixture
{
    public enum RegisteredInput
    {
        MoveForward,
        MoveLeft,
        MoveRight,
        MoveBack,
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
            {RegisteredInput.MoveForward, keyboard.wKey },
            {RegisteredInput.MoveLeft, keyboard.aKey },
            {RegisteredInput.MoveRight, keyboard.dKey },
            {RegisteredInput.MoveBack, keyboard.sKey },
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
    private IPlayerMovement playerMovement;
    private KeyboardMouseTestFixture inputTestFixture = new KeyboardMouseTestFixture();

    private float GetMovementDurationTowards(Transform target)
    {
        float moveDistance = Utils.GeometryCalcu.GetDistance3D(player.transform.position, target.transform.position);
        float moveDuration = Utils.PlayerHelper.DistanceToMoveDuration(playerMovement, moveDistance);
        return moveDuration;
    }

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
            if (gameObject.name == "Player")
            {
                player = gameObject;
                playerMovement = player.GetComponent<IPlayerMovement>();
            }
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
        inputTestFixture.Press(KeyboardMouseTestFixture.RegisteredInput.PickItem);
        yield return null;
        GameObject flashlight = player.transform.Find("Rotate/InteractZone/DummyFlashlight").gameObject;
        Assert.NotNull(flashlight);

        IInventory inventory = player.transform.Find("Rotate/InteractZone").GetComponent<IInventory>();
        Assert.AreEqual(1, inventory.GetNumOfItem());
        Assert.NotNull(inventory.GetActiveItem());
        Assert.AreEqual(0, inventory.GetActiveIdx());

        inputTestFixture.Press(KeyboardMouseTestFixture.RegisteredInput.UseItem);
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
        GameObject lightSwitch = GameObject.Find("LightSwitch");
        inputTestFixture.Press(KeyboardMouseTestFixture.RegisteredInput.MoveRight);
        float moveDuration = GetMovementDurationTowards(lightSwitch.transform);
        yield return new WaitForSeconds(moveDuration);
        inputTestFixture.Release(KeyboardMouseTestFixture.RegisteredInput.MoveRight);

        ILightSwitchController lightSwitchController = lightSwitch.GetComponent<ILightSwitchController>();

        inputTestFixture.Press(KeyboardMouseTestFixture.RegisteredInput.Interact);
        yield return null;
        inputTestFixture.Release(KeyboardMouseTestFixture.RegisteredInput.Interact);
        yield return null;
        Assert.IsTrue(lightSwitchController.GetState());

        inputTestFixture.Press(KeyboardMouseTestFixture.RegisteredInput.Interact);
        yield return null;
        inputTestFixture.Release(KeyboardMouseTestFixture.RegisteredInput.Interact);
        yield return null;
        Assert.IsFalse(lightSwitchController.GetState());
    }

    [UnityTest]
    public IEnumerator PlayerInteractableDetector_InteractWoodenDoor()
    {
        yield return new WaitWhile(() => sceneLoaded == false);
        inputTestFixture.Press(KeyboardMouseTestFixture.RegisteredInput.MoveForward);
        float moveDuration = GetMovementDurationTowards(GameObject.Find("WoodenDoor").transform);
        yield return new WaitForSeconds(moveDuration);
        inputTestFixture.Release(KeyboardMouseTestFixture.RegisteredInput.MoveForward);

        inputTestFixture.Press(KeyboardMouseTestFixture.RegisteredInput.MoveLeft);
        yield return new WaitForSeconds(0.1f);
        inputTestFixture.Release(KeyboardMouseTestFixture.RegisteredInput.MoveLeft);

        IDoorController door = GameObject.Find("WoodenDoor/Rotate").GetComponent<IDoorController>();
        float currentRotation = door.GetAngle();

        inputTestFixture.Press(KeyboardMouseTestFixture.RegisteredInput.Interact);
        yield return null;
        inputTestFixture.Release(KeyboardMouseTestFixture.RegisteredInput.Interact);
        yield return new WaitForSeconds(1f);
        Assert.IsTrue(door.GetState());

        inputTestFixture.Press(KeyboardMouseTestFixture.RegisteredInput.MoveForward);
        yield return new WaitForSeconds(0.4f);
        inputTestFixture.Release(KeyboardMouseTestFixture.RegisteredInput.MoveForward);

        inputTestFixture.Press(KeyboardMouseTestFixture.RegisteredInput.Interact);
        yield return null;
        inputTestFixture.Release(KeyboardMouseTestFixture.RegisteredInput.Interact);
        yield return new WaitForSeconds(1f);
        Assert.IsFalse(door.GetState());
    }
    #endregion

    #region Sprint
    [UnityTest]
    public IEnumerator PlayerMovement_Sprint()
    {
        yield return new WaitWhile(() => sceneLoaded == false);
        IPlayerMovement playerMovement = player.GetComponent<IPlayerMovement>();
        IPlayerBase playerBase = playerMovement.GetPlayerBase();

        inputTestFixture.Press(KeyboardMouseTestFixture.RegisteredInput.Sprint);
        yield return new WaitForSeconds(0.3f);
        Assert.IsTrue(playerMovement.GetSprintBool());
        Assert.AreEqual(playerBase.GetSprintSpeed(), playerMovement.GetCurMoveSpeed());

        inputTestFixture.Release(KeyboardMouseTestFixture.RegisteredInput.Sprint);
        yield return new WaitForSeconds(0.3f);
        Assert.IsFalse(playerMovement.GetSprintBool());
        Assert.AreEqual(playerBase.GetPlayerMovementSpeed(), playerMovement.GetCurMoveSpeed());
    }
    #endregion
}
