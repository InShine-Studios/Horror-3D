using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using UnityEngine.UI;

public class EvidenceItemTest : TestBase
{

    protected GameObject hud;

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
                hud = gameObject;
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
        GameObject thermometer = GameObject.Find("Thermometer");
        float moveDuration = GetMovementDurationTowards(thermometer.transform);
        yield return SimulateInput(KeyboardMouseTestFixture.RegisteredInput.MoveBack, false, moveDuration);
        yield return SimulateInput(KeyboardMouseTestFixture.RegisteredInput.PickItem);
        yield return SimulateInput(KeyboardMouseTestFixture.RegisteredInput.UseItem);
        GameObject overworldThermometer = GameObject.Find("OverworldItems/Thermometer");
        Image img = hud.transform.Find("ItemHud/Logo").GetComponent<Image>();
        IInventory inventory = player.transform.Find("Rotate/InteractZone").GetComponent<IInventory>();
        Assert.NotNull(overworldThermometer);
        Assert.IsFalse(img.enabled);
        Assert.AreEqual(0, inventory.GetNumOfItem());
        Assert.IsNull(inventory.GetActiveItem());
    }
    #endregion

    #region  Activated and Not Activated
    [UnityTest]
    public IEnumerator EvidenceItem_ActivatedWhenUsed()
    {
        yield return new WaitWhile(() => sceneLoaded == false);
        GameObject thermometer = GameObject.Find("Thermometer");
        float moveDuration = GetMovementDurationTowards(thermometer.transform);
        yield return SimulateInput(KeyboardMouseTestFixture.RegisteredInput.MoveBack, false, moveDuration);
        yield return SimulateInput(KeyboardMouseTestFixture.RegisteredInput.PickItem);
        yield return SimulateInput(KeyboardMouseTestFixture.RegisteredInput.UseItem);
        GameObject thermometerModel = GameObject.Find("OverworldItems/Thermometer/Model");
        Material stateMaterial = thermometerModel.GetComponent<MeshRenderer>().material;
        Assert.AreEqual("MAT_Thermometer_Active (Instance)", stateMaterial.name);
    }    
    
    [UnityTest]
    public IEnumerator EvidenceItem_NotActivatedWhenUsed()
    {
        yield return new WaitWhile(() => sceneLoaded == false);
        GameObject thermometer = GameObject.Find("Thermometer");
        float moveDuration = GetMovementDurationTowards(thermometer.transform);
        yield return SimulateInput(KeyboardMouseTestFixture.RegisteredInput.MoveBack, false, moveDuration);
        yield return SimulateInput(KeyboardMouseTestFixture.RegisteredInput.PickItem);
        yield return SimulateInput(KeyboardMouseTestFixture.RegisteredInput.DiscardItem);
        GameObject thermometerModel = GameObject.Find("OverworldItems/Thermometer/Model");
        Material stateMaterial = thermometerModel.GetComponent<MeshRenderer>().material;
        Assert.AreEqual("MAT_Thermometer_Base (Instance)", stateMaterial.name);
    }    
    #endregion
    
    #region Ghost Interaction Simulation

    // [TODO] Adjust/remove when Ghost Interaction fully implemented
    [UnityTest]
    public IEnumerator EvidenceItem_SimulateGhostInteractionWhenNotActivated()
    {
        yield return new WaitWhile(() => sceneLoaded == false);
        GameObject thermometer = GameObject.Find("Thermometer");
        float moveDuration = GetMovementDurationTowards(thermometer.transform);
        yield return SimulateInput(KeyboardMouseTestFixture.RegisteredInput.MoveBack, false, moveDuration);
        yield return SimulateInput(KeyboardMouseTestFixture.RegisteredInput.PickItem);
        yield return SimulateInput(KeyboardMouseTestFixture.RegisteredInput.DiscardItem);
        yield return SimulateInput(KeyboardMouseTestFixture.RegisteredInput.SimulateGhostInteract);
        GameObject thermometerModel = GameObject.Find("OverworldItems/Thermometer/Model");
        Material stateMaterial = thermometerModel.GetComponent<MeshRenderer>().material;
        Assert.AreEqual("MAT_Thermometer_Base (Instance)", stateMaterial.name);
    }      
    
    [UnityTest]
    public IEnumerator EvidenceItem_SimulateGhostInteractionPositive()
    {
        yield return new WaitWhile(() => sceneLoaded == false);
        GameObject thermometer = GameObject.Find("Thermometer");
        float moveDuration = GetMovementDurationTowards(thermometer.transform);
        yield return SimulateInput(KeyboardMouseTestFixture.RegisteredInput.MoveBack, false, moveDuration);
        yield return SimulateInput(KeyboardMouseTestFixture.RegisteredInput.PickItem);
        yield return SimulateInput(KeyboardMouseTestFixture.RegisteredInput.UseItem);
        yield return SimulateInput(KeyboardMouseTestFixture.RegisteredInput.SimulateGhostInteract);
        yield return SimulateInput(KeyboardMouseTestFixture.RegisteredInput.SimulateGhostInteract);
        GameObject thermometerModel = GameObject.Find("OverworldItems/Thermometer/Model");
        Material stateMaterial = thermometerModel.GetComponent<MeshRenderer>().material;
        Assert.AreEqual("MAT_Thermometer_Positive (Instance)", stateMaterial.name);
    }    
    
    [UnityTest]
    public IEnumerator EvidenceItem_SimulateGhostInteractionNegative()
    {
        yield return new WaitWhile(() => sceneLoaded == false);
        GameObject thermometer = GameObject.Find("Thermometer");
        float moveDuration = GetMovementDurationTowards(thermometer.transform);
        yield return SimulateInput(KeyboardMouseTestFixture.RegisteredInput.MoveBack, false, moveDuration);
        yield return SimulateInput(KeyboardMouseTestFixture.RegisteredInput.PickItem);
        yield return SimulateInput(KeyboardMouseTestFixture.RegisteredInput.UseItem);
        yield return SimulateInput(KeyboardMouseTestFixture.RegisteredInput.SimulateGhostInteract);
        GameObject thermometerModel = GameObject.Find("OverworldItems/Thermometer/Model");
        Material stateMaterial = thermometerModel.GetComponent<MeshRenderer>().material;
        Assert.AreEqual("MAT_Thermometer_Negative (Instance)", stateMaterial.name);
    }

    #endregion
}
