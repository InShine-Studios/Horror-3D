using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class MovementTest: TestBase
{
    protected override void FindGameObjects(Scene scene)
    {
        GameObject[] gameObjects = scene.GetRootGameObjects();
        foreach (GameObject gameObject in gameObjects)
        {
            if (gameObject.name == "Party")
            {
                party = gameObject;
                player = party.transform.Find("Iris").gameObject;
                playerMovement = player.GetComponent<IPlayerMovement>();
            }
            else if (gameObject.name == "Canvas")
            {
                hud = gameObject;
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
        Assert.IsTrue(playerMovement.GetSprintBool());
        Assert.AreEqual(playerBase.GetSprintSpeed(), playerMovement.GetCurMoveSpeed());

        inputTestFixture.Release(KeyboardMouseTestFixture.RegisteredInput.Sprint);
        yield return new WaitForSeconds(0.3f);
        Assert.IsFalse(playerMovement.GetSprintBool());
        Assert.AreEqual(playerBase.GetPlayerMovementSpeed(), playerMovement.GetCurMoveSpeed());
    }
    #endregion
}
