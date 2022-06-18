using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class PlayerTest: TestBase
{
    protected override void FindGameObjects(Scene scene)
    {
        GameObject[] gameObjects = scene.GetRootGameObjects();
        foreach (GameObject gameObject in gameObjects)
        {
            if (gameObject.name == "Player")
            {
                player = gameObject.transform.Find("Character").gameObject;
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

    #region Movement
    [UnityTest]
    public IEnumerator PlayerMovement_Sprint()
    {
        yield return new WaitWhile(() => sceneLoaded == false);
        IPlayerMovement playerMovement = player.GetComponent<IPlayerMovement>();
        IPlayerBase playerBase = playerMovement.GetPlayerBase();

        inputTestFixture.Press(KeyboardMouseTestFixture.RegisteredInput.Sprint);
        yield return new WaitForSeconds(0.3f);
        Assert.IsTrue(playerMovement.IsSprinting());
        Assert.AreEqual(playerBase.GetSprintSpeed(), playerMovement.GetCurMoveSpeed());

        inputTestFixture.Release(KeyboardMouseTestFixture.RegisteredInput.Sprint);
        yield return new WaitForSeconds(0.3f);
        Assert.IsFalse(playerMovement.IsSprinting());
        Assert.AreEqual(playerBase.GetPlayerMovementSpeed(), playerMovement.GetCurMoveSpeed());
    }
    #endregion

    #region Flashlight
    [UnityTest]
    public IEnumerator Player_Flashlight()
    {
        yield return new WaitWhile(() => sceneLoaded == false);
        IFlashlight playerFlashlight = player.GetComponentInChildren<IFlashlight>();

        bool prevFlashlightState = playerFlashlight.IsOn;

        inputTestFixture.Press(KeyboardMouseTestFixture.RegisteredInput.ToggleFlashlight);
        yield return null;
        Assert.AreEqual(!prevFlashlightState, playerFlashlight.IsOn);

        inputTestFixture.Press(KeyboardMouseTestFixture.RegisteredInput.ToggleFlashlight);
        yield return null;
        Assert.AreEqual(!prevFlashlightState, playerFlashlight.IsOn);
    }
    #endregion
}
