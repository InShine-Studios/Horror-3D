using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Ink.Runtime;

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
        IDialogueManager dialogueManager = GameObject.Find("UI/Dialogue Box").GetComponent<IDialogueManager>();

        float moveDuration = GetMovementDurationTowards(npc.transform);

        Assert.IsFalse(exclamationMark.activeInHierarchy);
        yield return SimulateInput(KeyboardMouseTestFixture.RegisteredInput.MoveForward, false, moveDuration);
        Assert.IsTrue(exclamationMark.activeInHierarchy);

        PlayerInput playerInput = player.GetComponent<PlayerInput>();

        Assert.AreEqual("Default", playerInput.currentActionMap.ToString().Split(':')[1]);
        Assert.IsFalse(dialogueManager.GetAnimator().GetBool("IsOpen"));
        yield return SimulateInput(KeyboardMouseTestFixture.RegisteredInput.Interact);
        yield return new WaitForSeconds(1.0f);

        Assert.AreEqual("UI", playerInput.currentActionMap.ToString().Split(':')[1]);
        Assert.IsTrue(dialogueManager.GetAnimator().GetBool("IsOpen"));
    }

    [UnityTest]
    public IEnumerator PlayerInteractableDetector_NextLineAndChoiceOption()
    {
        yield return new WaitWhile(() => sceneLoaded == false);
        GameObject npc = GameObject.Find("NPC");
        GameObject dialogue = GameObject.Find("UI/Dialogue Box");
        Button continueButton = dialogue.transform.Find("Continue").GetComponent<Button>();
        Button choiceButton1 = dialogue.transform.Find("Choice1").GetComponent<Button>();
        Button choiceButton2 = dialogue.transform.Find("Choice2").GetComponent<Button>();

        float moveDuration = GetMovementDurationTowards(npc.transform);

        yield return SimulateInput(KeyboardMouseTestFixture.RegisteredInput.MoveForward, false, moveDuration);

        INpcController npcController = npc.GetComponent<INpcController>();
        IDialogueManager dialogueManager = dialogue.GetComponent<IDialogueManager>();

        yield return SimulateInput(KeyboardMouseTestFixture.RegisteredInput.Interact);

        yield return new WaitForSeconds(0.3f);
        yield return SimulateInput(KeyboardMouseTestFixture.RegisteredInput.NextDialogueEnter);
        yield return new WaitForSeconds(0.3f);
        continueButton.onClick.Invoke();
        Assert.AreEqual(2, dialogueManager.GetDialogStory().currentChoices.Count);

        yield return new WaitForSeconds(1f);
        choiceButton2.onClick.Invoke();
        yield return new WaitForSeconds(1f);
        continueButton.onClick.Invoke();
        Assert.AreEqual(2, dialogueManager.GetDialogStory().currentChoices.Count);

        yield return new WaitForSeconds(1f);
        choiceButton1.onClick.Invoke();
        yield return new WaitForSeconds(1f);
        continueButton.onClick.Invoke();
        yield return new WaitForSeconds(1f);
        continueButton.onClick.Invoke();

        Assert.IsFalse(dialogueManager.IsDialogBoxOpen());
    }
    #endregion
}
