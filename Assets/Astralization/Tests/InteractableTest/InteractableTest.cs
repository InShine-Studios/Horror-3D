using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class InteractableTest: TestBase
{
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

        IDoorController door = GameObject.Find("WoodenDoor/Rotate").GetComponent<IDoorController>();
        float currentRotation = door.GetAngle();

        yield return SimulateInput(KeyboardMouseTestFixture.RegisteredInput.Interact);
        yield return new WaitForSeconds(1f);
        Assert.IsTrue(door.GetState());

        yield return SimulateInput(KeyboardMouseTestFixture.RegisteredInput.MoveForward, false, 0.4f);

        yield return SimulateInput(KeyboardMouseTestFixture.RegisteredInput.Interact);
        yield return new WaitForSeconds(1f);
        Assert.IsFalse(door.GetState());
    }
    #endregion
}
