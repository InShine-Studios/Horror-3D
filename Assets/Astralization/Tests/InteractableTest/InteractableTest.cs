using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using UnityEngine.InputSystem;

public class InteractableTest: TestBase
{
    protected override void FindGameObjects(Scene scene)
    {
        GameObject[] gameObjects = scene.GetRootGameObjects();
        foreach (GameObject gameObject in gameObjects)
        {
            if (gameObject.name == "Player")
            {
                player = gameObject.transform.Find("Character").gameObject;
                playerMovement = player.GetComponent<IPlayerMovement>();
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

        IDoorController door = GameObject.Find("WoodenDoor/Model/Rotate").GetComponent<IDoorController>();
        float currentRotation = door.GetAngle();

        yield return SimulateInput(KeyboardMouseTestFixture.RegisteredInput.Interact);
        yield return new WaitForSeconds(1f);
        Assert.IsTrue(door.IsOpen);

        yield return SimulateInput(KeyboardMouseTestFixture.RegisteredInput.MoveForward, false, 0.4f);
        yield return SimulateInput(KeyboardMouseTestFixture.RegisteredInput.MoveRight, false, 0.3f);

        yield return SimulateInput(KeyboardMouseTestFixture.RegisteredInput.Interact);
        yield return new WaitForSeconds(1f);
        Assert.IsFalse(door.IsOpen);

        yield return SimulateInput(KeyboardMouseTestFixture.RegisteredInput.MoveForward, false, 1f);
        yield return SimulateInput(KeyboardMouseTestFixture.RegisteredInput.Interact);
        Assert.IsFalse(door.IsOpen);
    }

    [UnityTest]
    public IEnumerator PlayerInteractableDetector_InteractClosets()
    {
        yield return new WaitWhile(() => sceneLoaded == false);
        GameObject closets = GameObject.Find("Closets");
        float moveDuration = GetMovementDurationTowards(closets.transform);
        yield return SimulateInput(KeyboardMouseTestFixture.RegisteredInput.MoveLeft, false, 0.7f);
        yield return SimulateInput(KeyboardMouseTestFixture.RegisteredInput.MoveForward, false, moveDuration);

        PlayerInput _playerInput = player.GetComponent<PlayerInput>();
        //Animator anim = _hud.transform.Find("HidingOverlay").GetComponent<Animator>();
        yield return SimulateInput(KeyboardMouseTestFixture.RegisteredInput.Interact);
        yield return new WaitForSeconds(1.0f);
        Vector3 calOffset = closets.GetComponentInChildren<Renderer>().bounds.center;
        calOffset.y = 0;
        //Assert.Less(Utils.GeometryCalcu.GetDistance3D(calOffset,player.transform.position),1f); //TODO #312
        //Assert.True(anim.GetBool("isHiding"));
        Assert.AreEqual(_playerInput.currentActionMap.name, "Hiding");

        // Closets camera
        HidingCameraConfigs hidingCameraConfigs = closets.GetComponentInChildren<HidingCameraConfigs>();
        Vector3 startingPosition = hidingCameraConfigs.StartingPosition;
        GameObject cameraHiding = GameObject.Find("Player/Camera/Camera Hiding");
        Cinemachine.CinemachineVirtualCamera vcam = cameraHiding.GetComponent<Cinemachine.CinemachineVirtualCamera>();
        Vector3 cameraPosition = vcam.m_Follow.position;
        Assert.AreEqual(cameraPosition, startingPosition);

        // Try moving while hiding
        Transform currentPosition = player.transform;
        yield return SimulateInput(KeyboardMouseTestFixture.RegisteredInput.MoveBack, false, 1f);
        Assert.Less(Utils.GeometryCalcu.GetDistance3D(calOffset, player.transform.position), 1f);

        // Unhide
        yield return SimulateInput(KeyboardMouseTestFixture.RegisteredInput.Interact);
        //Assert.False(anim.GetBool("isHiding"));
        yield return new WaitForSeconds(1.0f);
        Assert.AreEqual(_playerInput.currentActionMap.name, "Default");

        // Try moving after hiding
        currentPosition = player.transform;
        yield return SimulateInput(KeyboardMouseTestFixture.RegisteredInput.MoveBack, false, 1f);
        Assert.AreNotEqual(currentPosition, player.transform.position);
    }

    [UnityTest]
    public IEnumerator PlayerInteractableDetector_ShowClosestInteractableMarker()
    {
        yield return new WaitWhile(() => sceneLoaded == false);
        GameObject lightSwitch = GameObject.Find("LightSwitch");
        GameObject lightSwitch2 = GameObject.Find("LightSwitch(2)");
        GameObject overworldAnkh = GameObject.Find("OverworldItems/Ankh (2)");
        IInteractableItemMarker markLight1 = lightSwitch.transform.GetComponentInChildren<IInteractableItemMarker>();
        IInteractableItemMarker markLight2 = lightSwitch2.transform.GetComponentInChildren<IInteractableItemMarker>();
        IInteractableItemMarker markFlash = overworldAnkh.transform.GetComponentInChildren<IInteractableItemMarker>();
        float moveDuration = GetMovementDurationTowards(lightSwitch.transform);
        yield return SimulateInput(KeyboardMouseTestFixture.RegisteredInput.MoveRight, false, moveDuration);
        Assert.True(markLight1.IsEnabled());
        Assert.False(markLight2.IsEnabled());
        Assert.False(markFlash.IsEnabled());
        yield return SimulateInput(KeyboardMouseTestFixture.RegisteredInput.MoveForward, false, 0.6f);
        Assert.False(markLight1.IsEnabled());
        Assert.True(markLight2.IsEnabled()); 
        Assert.True(markFlash.IsEnabled());
    }
    #endregion
}
