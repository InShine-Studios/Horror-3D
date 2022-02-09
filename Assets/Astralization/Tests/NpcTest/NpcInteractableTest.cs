using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class NPCInteractableTest: TestBase
{
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
        }
    }

    #region Setup Teardown
    [SetUp]
    public override void SetUp()
    {
        sceneName = "NPCInteractableTestScene";
        base.SetUp();
    }
    #endregion

    #region Interactable
    [UnityTest]
    public IEnumerator PlayerInteractableDetector_InteractNPC()
    {
        yield return new WaitWhile(() => sceneLoaded == false);
        GameObject npc = GameObject.Find("NPC");
        INpcController npcController = npc.GetComponent<INpcController>();
        GameObject exclamationMark = npc.transform.Find("ExclamationMark").gameObject;
        IDialogueManager dialogueManager = GameObject.Find("Dialogue/Dialogue Box").GetComponent<IDialogueManager>();

        float moveDuration = GetMovementDurationTowards(npc.transform);

        Assert.IsFalse(exclamationMark.activeInHierarchy);
        yield return SimulateInput(KeyboardMouseTestFixture.RegisteredInput.MoveForward, false, moveDuration);
        Assert.IsTrue(exclamationMark.activeInHierarchy);

        PlayerInput playerInput = player.GetComponent<PlayerInput>();

        Assert.IsFalse(npcController.GetState());
        Assert.AreEqual(playerInput.currentActionMap.ToString().Split(':')[1], "Player");
        Assert.IsFalse(dialogueManager.GetAnimator().GetBool("IsOpen"));
        yield return SimulateInput(KeyboardMouseTestFixture.RegisteredInput.Interact);
        yield return new WaitForSeconds(1.0f);
        Assert.IsTrue(npcController.GetState());
        Assert.AreEqual(playerInput.currentActionMap.ToString().Split(':')[1], "Dialogue");
        Assert.IsTrue(dialogueManager.GetAnimator().GetBool("IsOpen"));
    }
    #endregion
}
