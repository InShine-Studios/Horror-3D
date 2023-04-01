using System.Collections;
using Astralization.Items.EvidenceItems;
using Astralization.Player;
using Astralization.SPI;
using Astralization.UI.ItemHud;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using UnityEngine.UI;

public class EvidenceItemTest : TestBase
{

    protected IItemHudDisplay itemHud;

    protected override void FindGameObjects(Scene scene)
    {
        GameObject[] gameObjects = scene.GetRootGameObjects();
        foreach (GameObject gameObject in gameObjects)
        {
            if (gameObject.name == "Player")
            {
                player = gameObject.transform.Find("Character").gameObject;
                playerMovement = player.GetComponent<IPlayerMovement>();
                itemHud = gameObject.transform.GetComponentInChildren<IItemHudDisplay>();
            }
        }
    }

    #region Setup Teardown
    [SetUp]
    public override void SetUp()
    {
        sceneName = "EvidenceItemTestScene";
        base.SetUp();
    }
    #endregion

    #region Discard When Used
    [UnityTest]
    public IEnumerator EvidenceItem_DiscardWhenUsedEvidenceItem()
    {
        yield return new WaitWhile(() => sceneLoaded == false);

        foreach (string gameObjectName in
            new ArrayList() {
                "Thermometer",
                "SilhouetteBowl",
                "Clock",
        })
        {
            GameObject gameObject = GameObject.Find(gameObjectName);
            float moveDuration = GetMovementDurationTowards(gameObject.transform);

            yield return SimulateInput(KeyboardMouseTestFixture.RegisteredInput.MoveForward, false, moveDuration);
            yield return SimulateInput(KeyboardMouseTestFixture.RegisteredInput.PickItem);
            yield return SimulateInput(KeyboardMouseTestFixture.RegisteredInput.UseItem);

            gameObject = GameObject.Find("OverworldItems/" + gameObjectName);
            Image img = itemHud.GetSelectedItemLogo();
            IInventory inventory = player.transform.Find("Rotate/InteractZone").GetComponent<IInventory>();

            Assert.NotNull(gameObject);
            Assert.IsFalse(img.enabled);
            Assert.AreEqual(0, inventory.GetNumOfItem());
            Assert.IsNull(inventory.GetActiveItem());
        }
    }
    #endregion

    #region  Activated and Not Activated
    [UnityTest]
    public IEnumerator EvidenceItem_ActivatedWhenUsed()
    {
        yield return new WaitWhile(() => sceneLoaded == false);

        foreach (string gameObjectName in
            new ArrayList() {
                "Thermometer",
                "SilhouetteBowl",
                "Clock",
        })
        {
            GameObject gameObject = GameObject.Find(gameObjectName);
            float moveDuration = GetMovementDurationTowards(gameObject.transform);

            yield return SimulateInput(KeyboardMouseTestFixture.RegisteredInput.MoveForward, false, moveDuration);
            yield return SimulateInput(KeyboardMouseTestFixture.RegisteredInput.PickItem);
            yield return SimulateInput(KeyboardMouseTestFixture.RegisteredInput.UseItem);

            gameObject = GameObject.Find("OverworldItems/" + gameObjectName);

            if (gameObjectName == "Thermometer")
            {
                GameObject thermometerModel = GameObject.Find("OverworldItems/Thermometer/Model");
                Material stateMaterial = thermometerModel.GetComponent<MeshRenderer>().material;
                Assert.AreEqual("MAT_Thermometer_Active (Instance)", stateMaterial.name);
            }
            else if (gameObjectName == "Clock")
            {
                GameObject clockAudioSourceRef = GameObject.Find("OverworldItems/Clock/AudioPlayer/StateAudio");
                AudioClip stateAudioClip = clockAudioSourceRef.GetComponent<AudioSource>().clip;
                Assert.AreEqual("SFX_Clock_3_Loop", stateAudioClip.name);
            }
            else if (gameObjectName == "SilhouetteBowl")
            {
                GameObject headless = GameObject.Find("OverworldItems/SilhouetteBowl/Model/Headless");
                GameObject heartless = GameObject.Find("OverworldItems/SilhouetteBowl/Model/Heartless");
                Assert.IsFalse(headless.activeInHierarchy);
                Assert.IsFalse(heartless.activeInHierarchy);
            }

            IStateMachine script = gameObject.GetComponent<IStateMachine>();
            Assert.True(script.CurrentState is IActiveState);
        }
    }

    [UnityTest]
    public IEnumerator EvidenceItem_NotActivatedWhenDiscarded()
    {
        yield return new WaitWhile(() => sceneLoaded == false);

        foreach (string gameObjectName in
            new ArrayList() {
                "Thermometer",
                "SilhouetteBowl",
                "Clock",
        })
        {
            GameObject gameObject = GameObject.Find(gameObjectName);
            float moveDuration = GetMovementDurationTowards(gameObject.transform);

            yield return SimulateInput(KeyboardMouseTestFixture.RegisteredInput.MoveForward, false, moveDuration);
            yield return SimulateInput(KeyboardMouseTestFixture.RegisteredInput.PickItem);
            yield return SimulateInput(KeyboardMouseTestFixture.RegisteredInput.DiscardItem);

            gameObject = GameObject.Find("OverworldItems/" + gameObjectName);

            if (gameObjectName == "Thermometer")
            {
                GameObject thermometerModel = GameObject.Find("OverworldItems/Thermometer/Model");
                Material stateMaterial = thermometerModel.GetComponent<MeshRenderer>().material;
                Assert.AreEqual("MAT_Thermometer_Base (Instance)", stateMaterial.name);
            }
            else if (gameObjectName == "Clock")
            {
                GameObject clockAudioSourceRef = GameObject.Find("OverworldItems/Clock/AudioPlayer/StateAudio");
                AudioClip stateAudioClip = clockAudioSourceRef.GetComponent<AudioSource>().clip;
                Assert.IsNull(stateAudioClip);
            }
            else if (gameObjectName == "SilhouetteBowl")
            {
                GameObject headless = GameObject.Find("OverworldItems/SilhouetteBowl/Model/Headless");
                GameObject heartless = GameObject.Find("OverworldItems/SilhouetteBowl/Model/Heartless");
                Assert.IsFalse(headless.activeInHierarchy);
                Assert.IsFalse(heartless.activeInHierarchy);
            }

            IStateMachine script = gameObject.GetComponent<IStateMachine>();
            Assert.True(script.CurrentState is IInactiveState);
        }
    }

    [UnityTest]
    public IEnumerator EvidenceItem_UseInAstralWorld()
    {
        yield return new WaitWhile(() => sceneLoaded == false);
        yield return SimulateInput(KeyboardMouseTestFixture.RegisteredInput.MoveForward, false, 0.01f);
        yield return SimulateInput(KeyboardMouseTestFixture.RegisteredInput.PickItem);
        yield return SimulateInput(KeyboardMouseTestFixture.RegisteredInput.UseItem);
        yield return SimulateInput(KeyboardMouseTestFixture.RegisteredInput.DiscardItem);

        foreach (string gameObjectName in
            new ArrayList() {
                "Thermometer",
                "SilhouetteBowl",
                "Clock",
        })
        {
            GameObject gameObject = GameObject.Find(gameObjectName);
            float moveDuration = GetMovementDurationTowards(gameObject.transform);

            yield return SimulateInput(KeyboardMouseTestFixture.RegisteredInput.MoveForward, false, moveDuration);
            yield return SimulateInput(KeyboardMouseTestFixture.RegisteredInput.PickItem);
            yield return SimulateInput(KeyboardMouseTestFixture.RegisteredInput.UseItem);

            gameObject = GameObject.Find("OverworldItems/" + gameObjectName);

            if (gameObjectName == "Thermometer")
            {
                GameObject thermometerModel = GameObject.Find("OverworldItems/Thermometer/Model");
                Material stateMaterial = thermometerModel.GetComponent<MeshRenderer>().material;
                Assert.AreEqual("MAT_Thermometer_Active (Instance)", stateMaterial.name);
            }
            else if (gameObjectName == "Clock")
            {
                GameObject clockAudioSourceRef = GameObject.Find("OverworldItems/Clock/AudioPlayer/StateAudio");
                AudioClip stateAudioClip = clockAudioSourceRef.GetComponent<AudioSource>().clip;
                Assert.AreEqual("SFX_Clock_3_Loop", stateAudioClip.name);
            }
            else if (gameObjectName == "SilhouetteBowl")
            {
                GameObject headless = GameObject.Find("OverworldItems/SilhouetteBowl/Model/Headless");
                GameObject heartless = GameObject.Find("OverworldItems/SilhouetteBowl/Model/Heartless");
                Assert.IsFalse(headless.activeInHierarchy);
                Assert.IsFalse(heartless.activeInHierarchy);
            }

            IStateMachine script = gameObject.GetComponent<IStateMachine>();
            Assert.True(script.CurrentState is IActiveState);
        }
    }
    #endregion

    #region Ghost Interaction Simulation

    // [TODO] Adjust/remove when Ghost Interaction fully implemented
    [UnityTest]
    public IEnumerator EvidenceItem_SimulateGhostInteractionWhenNotActivated()
    {
        yield return new WaitWhile(() => sceneLoaded == false);

        foreach (string gameObjectName in
            new ArrayList() {
                "Thermometer",
                "SilhouetteBowl",
                "Clock",
        })
        {
            GameObject gameObject = GameObject.Find(gameObjectName);
            float moveDuration = GetMovementDurationTowards(gameObject.transform);

            yield return SimulateInput(KeyboardMouseTestFixture.RegisteredInput.MoveForward, false, moveDuration);
            yield return SimulateInput(KeyboardMouseTestFixture.RegisteredInput.PickItem);
            yield return SimulateInput(KeyboardMouseTestFixture.RegisteredInput.DiscardItem);
            yield return SimulateInput(KeyboardMouseTestFixture.RegisteredInput.SimulateGhostInteract);

            gameObject = GameObject.Find("OverworldItems/" + gameObjectName);

            if (gameObjectName == "Thermometer")
            {
                GameObject thermometerModel = GameObject.Find("OverworldItems/Thermometer/Model");
                Material stateMaterial = thermometerModel.GetComponent<MeshRenderer>().material;
                Assert.AreEqual("MAT_Thermometer_Base (Instance)", stateMaterial.name);
            }
            else if (gameObjectName == "Clock")
            {
                GameObject clockAudioSourceRef = GameObject.Find("OverworldItems/Clock/AudioPlayer/StateAudio");
                AudioClip stateAudioClip = clockAudioSourceRef.GetComponent<AudioSource>().clip;
                Assert.IsNull(stateAudioClip);
            }
            else if (gameObjectName == "SilhouetteBowl")
            {
                GameObject headless = GameObject.Find("OverworldItems/SilhouetteBowl/Model/Headless");
                GameObject heartless = GameObject.Find("OverworldItems/SilhouetteBowl/Model/Heartless");
                Assert.IsFalse(headless.activeInHierarchy);
                Assert.IsFalse(heartless.activeInHierarchy);
            }

            IStateMachine script = gameObject.GetComponent<IStateMachine>();
            Assert.True(script.CurrentState is IInactiveState);
        }
    }

    [UnityTest]
    public IEnumerator EvidenceItem_SimulateGhostInteractionPositive()
    {
        yield return new WaitWhile(() => sceneLoaded == false);

        foreach (string gameObjectName in
            new ArrayList() {
                "Thermometer",
                "SilhouetteBowl",
                "Clock",
        })
        {
            GameObject gameObject = GameObject.Find(gameObjectName);
            float moveDuration = GetMovementDurationTowards(gameObject.transform);

            yield return SimulateInput(KeyboardMouseTestFixture.RegisteredInput.MoveForward, false, moveDuration);
            yield return SimulateInput(KeyboardMouseTestFixture.RegisteredInput.PickItem);
            yield return SimulateInput(KeyboardMouseTestFixture.RegisteredInput.UseItem);
            yield return SimulateInput(KeyboardMouseTestFixture.RegisteredInput.SimulateGhostInteract);
            yield return SimulateInput(KeyboardMouseTestFixture.RegisteredInput.SimulateGhostInteract);

            gameObject = GameObject.Find("OverworldItems/" + gameObjectName);

            if (gameObjectName == "Thermometer")
            {
                GameObject thermometerModel = GameObject.Find("OverworldItems/Thermometer/Model");
                Material stateMaterial = thermometerModel.GetComponent<MeshRenderer>().material;
                Assert.AreEqual("MAT_Thermometer_Positive (Instance)", stateMaterial.name);
            }
            else if (gameObjectName == "Clock")
            {
                GameObject clockAudioSourceRef = GameObject.Find("OverworldItems/Clock/AudioPlayer/StateAudio");
                AudioClip stateAudioClip = clockAudioSourceRef.GetComponent<AudioSource>().clip;
                Assert.AreEqual("SFX_Clock_2_Loop", stateAudioClip.name);
            }
            else if (gameObjectName == "SilhouetteBowl")
            {
                GameObject headless = GameObject.Find("OverworldItems/SilhouetteBowl/Model/Headless");
                GameObject heartless = GameObject.Find("OverworldItems/SilhouetteBowl/Model/Heartless");
                Assert.IsTrue(headless.activeInHierarchy);
                Assert.IsFalse(heartless.activeInHierarchy);
            }

            IStateMachine script = gameObject.GetComponent<IStateMachine>();
            Assert.True(script.CurrentState is IPositiveState);
        }
    }

    [UnityTest]
    public IEnumerator EvidenceItem_SimulateGhostInteractionNegative()
    {
        yield return new WaitWhile(() => sceneLoaded == false);

        foreach (string gameObjectName in
            new ArrayList() {
                "Thermometer",
                "SilhouetteBowl",
                "Clock",
        })
        {
            GameObject gameObject = GameObject.Find(gameObjectName);
            float moveDuration = GetMovementDurationTowards(gameObject.transform);

            yield return SimulateInput(KeyboardMouseTestFixture.RegisteredInput.MoveForward, false, moveDuration);
            yield return SimulateInput(KeyboardMouseTestFixture.RegisteredInput.PickItem);
            yield return SimulateInput(KeyboardMouseTestFixture.RegisteredInput.UseItem);
            yield return SimulateInput(KeyboardMouseTestFixture.RegisteredInput.SimulateGhostInteract);

            gameObject = GameObject.Find("OverworldItems/" + gameObjectName);

            if (gameObjectName == "Thermometer")
            {
                GameObject thermometerModel = GameObject.Find("OverworldItems/Thermometer/Model");
                Material stateMaterial = thermometerModel.GetComponent<MeshRenderer>().material;
                Assert.AreEqual("MAT_Thermometer_Negative (Instance)", stateMaterial.name);
            }
            else if (gameObjectName == "Clock")
            {
                GameObject clockAudioSourceRef = GameObject.Find("OverworldItems/Clock/AudioPlayer/StateAudio");
                AudioClip stateAudioClip = clockAudioSourceRef.GetComponent<AudioSource>().clip;
                Assert.AreEqual("SFX_Clock_1_Loop", stateAudioClip.name);
            }
            else if (gameObjectName == "SilhouetteBowl")
            {
                GameObject headless = GameObject.Find("OverworldItems/SilhouetteBowl/Model/Headless");
                GameObject heartless = GameObject.Find("OverworldItems/SilhouetteBowl/Model/Heartless");
                Assert.IsFalse(headless.activeInHierarchy);
                Assert.IsTrue(heartless.activeInHierarchy);
            }

            IStateMachine script = gameObject.GetComponent<IStateMachine>();
            Assert.True(script.CurrentState is INegativeState);
        }
    }
    #endregion
}
