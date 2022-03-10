using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using UnityEngine.InputSystem;

public class InteractableTest: TestBase
{
    private GameObject _hud;

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
                _hud = gameObject;
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

    #region Interactable
    [UnityTest]
    public IEnumerator PlayerInteractableDetector_InteractLightSwitch()
    {
        yield return new WaitWhile(() => sceneLoaded == false);
        GameObject lightSwitch = GameObject.Find("LightSwitch");
        float moveDuration = GetMovementDurationTowards(lightSwitch.transform);
        yield return SimulateInput(KeyboardMouseTestFixture.RegisteredInput.MoveRight, false, moveDuration);

        ILightSwitchController lightSwitchController = lightSwitch.GetComponent<ILightSwitchController>();

        yield return SimulateInput(KeyboardMouseTestFixture.RegisteredInput.Interact);
        Assert.IsTrue(lightSwitchController.GetState());

        yield return SimulateInput(KeyboardMouseTestFixture.RegisteredInput.Interact);
        Assert.IsFalse(lightSwitchController.GetState());
    }

    [UnityTest]
    public IEnumerator PlayerInteractableDetector_InteractWoodenDoor()
    {
        yield return new WaitWhile(() => sceneLoaded == false);
        float moveDuration = GetMovementDurationTowards(GameObject.Find("WoodenDoor").transform);
        yield return SimulateInput(KeyboardMouseTestFixture.RegisteredInput.MoveForward, false, moveDuration);

        yield return SimulateInput(KeyboardMouseTestFixture.RegisteredInput.MoveLeft, false, 0.1f);

        IDoorController door = GameObject.Find("WoodenDoor/Model/Rotate").GetComponent<IDoorController>();
        float currentRotation = door.GetAngle();

        yield return SimulateInput(KeyboardMouseTestFixture.RegisteredInput.Interact);
        yield return new WaitForSeconds(1f);
        Assert.IsTrue(door.GetState());

        yield return SimulateInput(KeyboardMouseTestFixture.RegisteredInput.MoveForward, false, 0.4f);

        yield return SimulateInput(KeyboardMouseTestFixture.RegisteredInput.Interact);
        yield return new WaitForSeconds(1f);
        Assert.IsFalse(door.GetState());
    }

    [UnityTest]
    public IEnumerator PlayerInteractableDetector_InteractClosets()
    {
        yield return new WaitWhile(() => sceneLoaded == false);
        float moveDuration = GetMovementDurationTowards(GameObject.Find("Closets").transform);
        yield return SimulateInput(KeyboardMouseTestFixture.RegisteredInput.MoveLeft, false, 0.5f);
        yield return SimulateInput(KeyboardMouseTestFixture.RegisteredInput.MoveForward, false, moveDuration);

        PlayerInput _playerInput = player.GetComponent<PlayerInput>();
        Animator anim = _hud.transform.Find("HidingOverlay").GetComponent<Animator>();
        Transform closetsModel = GameObject.Find("Closets").transform.Find("Model");
        yield return SimulateInput(KeyboardMouseTestFixture.RegisteredInput.Interact);
        Vector3 closetsPosition = closetsModel.position;
        Vector3 calOffset = closetsModel.rotation * new Vector3(0.97f, 1.1f, -0.47f);
        Assert.AreEqual(player.transform.position, closetsPosition + calOffset);
        Assert.True(anim.GetBool("isHiding"));
        Assert.AreEqual(_playerInput.currentActionMap.name, "Hiding");

        yield return SimulateInput(KeyboardMouseTestFixture.RegisteredInput.Interact);
        Assert.False(anim.GetBool("isHiding"));
        Assert.AreEqual(_playerInput.currentActionMap.name, "Default");
    }

    [UnityTest]
    public IEnumerator PlayerInteractableDetector_ShowClosestInteractableIcon()
    {
        yield return new WaitWhile(() => sceneLoaded == false);
        GameObject lightSwitch = GameObject.Find("LightSwitch");
        GameObject lightSwitch2 = GameObject.Find("LightSwitch(2)");
        GameObject overworldFlash = GameObject.Find("OverworldItems/DummyFlashlight(2)");
        Transform markLight1 = lightSwitch.transform.Find("ExclamationMarkSwitch");
        Transform markLight2 = lightSwitch2.transform.Find("ExclamationMarkSwitch");
        Transform markFlash = overworldFlash.transform.Find("ExclamationMarkItem");
        float moveDuration = GetMovementDurationTowards(lightSwitch.transform);
        yield return SimulateInput(KeyboardMouseTestFixture.RegisteredInput.MoveRight, false, moveDuration);
        Assert.True(markLight1.gameObject.activeInHierarchy);
        Assert.False(markLight2.gameObject.activeInHierarchy);
        Assert.False(markFlash.gameObject.activeInHierarchy);
        yield return SimulateInput(KeyboardMouseTestFixture.RegisteredInput.MoveForward, false, 0.4f);
        Assert.False(markLight1.gameObject.activeInHierarchy);
        Assert.True(markLight2.gameObject.activeInHierarchy);
        Assert.True(markFlash.gameObject.activeInHierarchy);
    }
    #endregion
}
