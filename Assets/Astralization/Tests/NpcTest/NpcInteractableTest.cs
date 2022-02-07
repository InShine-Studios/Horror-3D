using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.InputSystem;

public class NPCInteractableTest: TestBase
{
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
        GameObject player = GameObject.Find("Party/Iris");
        GameObject npc = GameObject.Find("NPC");
        GameObject exclamationMark = npc.transform.Find("ExclamationMark").gameObject;
        GameObject dialogue = GameObject.Find("Dialogue/Dialogue Box");

        //Debug.Log(player + "||" + npc + "||" + exclamationMark + "||" + dialogue);
        //Debug.Log(npc.transform.position);
        float moveDuration = GetMovementDurationTowards(npc.transform);

        Assert.IsFalse(exclamationMark.activeInHierarchy);
        yield return SimulateInput(KeyboardMouseTestFixture.RegisteredInput.MoveForward, false, moveDuration);
        Assert.IsTrue(exclamationMark.activeInHierarchy);

        INpcController npcController = npc.GetComponent<INpcController>();
        PlayerInput playerInput = player.GetComponent<PlayerInput>();
        IDialogueManager dialogueManager = dialogue.GetComponent<IDialogueManager>();

        Assert.AreEqual(playerInput.currentActionMap.ToString().Split(':')[1], "Player");
        Assert.IsFalse(dialogueManager.GetAnimator().GetBool("IsOpen"));
        yield return SimulateInput(KeyboardMouseTestFixture.RegisteredInput.Interact);

        yield return new WaitForSeconds(1.0f);
        Assert.AreEqual(playerInput.currentActionMap.ToString().Split(':')[1], "Dialogue");
        Assert.IsTrue(dialogueManager.GetAnimator().GetBool("IsOpen"));
    }

    [UnityTest]
    public IEnumerator PlayerInteractableDetector_NextLine()
    {
        yield return new WaitWhile(() => sceneLoaded == false);
        GameObject npc = GameObject.Find("NPC");
        GameObject dialogue = GameObject.Find("Dialogue/Dialogue Box");

        float moveDuration = GetMovementDurationTowards(npc.transform);

        yield return SimulateInput(KeyboardMouseTestFixture.RegisteredInput.MoveForward, false, moveDuration);

        INpcController npcController = npc.GetComponent<INpcController>();
        IDialogueManager dialogueManager = dialogue.GetComponent<IDialogueManager>();

        yield return SimulateInput(KeyboardMouseTestFixture.RegisteredInput.Interact);

        yield return new WaitForSeconds(0.3f);
        yield return SimulateInput(KeyboardMouseTestFixture.RegisteredInput.NextDialogueEnter);
        Assert.AreEqual(2, dialogueManager.GetIndex());

        yield return new WaitForSeconds(0.3f);
        yield return SimulateInput(KeyboardMouseTestFixture.RegisteredInput.NextDialogueClick);

        Assert.IsFalse(dialogueManager.GetDialogBox());
    }
    #endregion
}
