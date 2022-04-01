using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class AstralWorldEntryTest : TestBase
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
        sceneName = "AstralWorldTestScene";
        base.SetUp();
    }
    #endregion

    #region Astral World
    [UnityTest]
    public IEnumerator AstralWorld_MoveToAstralWorld()
    {
        yield return new WaitWhile(() => sceneLoaded == false);
        IAstralMeterLogic astralMeterLogic = GameObject.Find("WorldState").GetComponent<IAstralMeterLogic>();
        Assert.IsTrue(astralMeterLogic.GetConstantRate() == astralMeterLogic.GetRealRate());

        GameObject ankhOW = GameObject.Find("OverworldItems/Ankh");
        float moveDuration = GetMovementDurationTowards(ankhOW.transform);
        yield return SimulateInput(KeyboardMouseTestFixture.RegisteredInput.MoveLeft, false, moveDuration);
        yield return SimulateInput(KeyboardMouseTestFixture.RegisteredInput.PickItem);

        yield return null;
        GameObject ankh = player.transform.Find("Rotate/InteractZone/Ankh").gameObject;
        Assert.NotNull(ankh);

        IInventory inventory = player.transform.Find("Rotate/InteractZone").GetComponent<IInventory>();
        Assert.AreEqual(1, inventory.GetNumOfItem());
        Assert.NotNull(inventory.GetActiveItem());
        Assert.AreEqual(0, inventory.GetActiveIdx());

        yield return SimulateInput(KeyboardMouseTestFixture.RegisteredInput.UseItem);
        yield return null;

        GameObject volume = GameObject.Find("WorldState");
        IStateMachine script = volume.GetComponent<IStateMachine>();
        Assert.True(script.CurrentState is IWorldAstralState);

        Assert.IsTrue(astralMeterLogic.GetConstantRate() == astralMeterLogic.GetAstralRate());
    }
    #endregion
}
