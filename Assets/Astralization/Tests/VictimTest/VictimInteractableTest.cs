using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Ink.Runtime;

public class VictimInteractableTest : TestBase
{
    private GameObject _victim;
    private IDialogueManager _dialogueManager;
    private GameObject _dialogue;

    protected override void FindGameObjects(Scene scene)
    {
        GameObject[] gameObjects = scene.GetRootGameObjects();
        foreach (GameObject gameObject in gameObjects)
        {
            if (gameObject.name == "Player")
            {
                player = gameObject.transform.Find("Character").gameObject;
                playerMovement = gameObject.GetComponentInChildren<IPlayerMovement>();
                _dialogueManager = gameObject.GetComponentInChildren<IDialogueManager>();
                _dialogue = gameObject.transform.Find(UiCanvas + "/Dialogue Box").gameObject;
            }
            else if (gameObject.name == "Victim")
            {
                _victim = gameObject;
            }
        }
    }

    #region Setup Teardown
    [SetUp]
    public override void SetUp()
    {
        sceneName = "VictimInteractableTestScene";
        base.SetUp();
    }
    #endregion

    #region VictimInteraction
    [UnityTest]
    public IEnumerator PlayerInteractableDetector_InteractVictim()
    {
        yield return new WaitWhile(() => sceneLoaded == false);
        IInteractableItemMarker interactableMarker = _victim.transform.GetComponentInChildren<IInteractableItemMarker>();

        float moveDuration = GetMovementDurationTowards(_victim.transform);

        Assert.IsFalse(interactableMarker.IsEnabled());
        yield return SimulateInput(KeyboardMouseTestFixture.RegisteredInput.MoveForward, false, moveDuration);
        Assert.IsTrue(interactableMarker.IsEnabled());

        PlayerInput playerInput = player.GetComponent<PlayerInput>();

        Assert.AreEqual("Default", playerInput.currentActionMap.ToString().Split(':')[1]);
        Assert.IsFalse(_dialogueManager.GetAnimator().GetBool("IsOpen"));
        yield return SimulateInput(KeyboardMouseTestFixture.RegisteredInput.Interact);
        yield return new WaitForSeconds(1.0f);

        Assert.AreEqual("UI", playerInput.currentActionMap.ToString().Split(':')[1]);
        Assert.IsTrue(_dialogueManager.GetAnimator().GetBool("IsOpen"));
    }

    [UnityTest]
    public IEnumerator PlayerInteractableDetector_NextLineAndChoiceOption()
    {
        yield return new WaitWhile(() => sceneLoaded == false);
        Button continueButton = _dialogue.transform.Find("Continue").GetComponent<Button>();
        Button choiceButton1 = _dialogue.transform.Find("Choice1").GetComponent<Button>();
        Button choiceButton2 = _dialogue.transform.Find("Choice2").GetComponent<Button>();

        float moveDuration = GetMovementDurationTowards(_victim.transform);

        yield return SimulateInput(KeyboardMouseTestFixture.RegisteredInput.MoveForward, false, moveDuration);

        yield return SimulateInput(KeyboardMouseTestFixture.RegisteredInput.Interact);

        yield return new WaitForSeconds(0.3f);
        yield return SimulateInput(KeyboardMouseTestFixture.RegisteredInput.NextDialogueEnter);
        yield return new WaitForSeconds(0.3f);
        continueButton.onClick.Invoke();
        Assert.AreEqual(2, _dialogueManager.GetDialogStory().currentChoices.Count);

        yield return new WaitForSeconds(1f);
        choiceButton2.onClick.Invoke();
        yield return new WaitForSeconds(1f);
        continueButton.onClick.Invoke();
        Assert.AreEqual(2, _dialogueManager.GetDialogStory().currentChoices.Count);

        yield return new WaitForSeconds(1f);
        choiceButton1.onClick.Invoke();
        yield return new WaitForSeconds(1f);
        continueButton.onClick.Invoke();
        yield return new WaitForSeconds(1f);
        continueButton.onClick.Invoke();

        Assert.IsFalse(_dialogueManager.IsDialogBoxOpen());
    }
    #endregion
}
