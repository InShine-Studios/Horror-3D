using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using UnityEngine.UI;

public class AstralTest
{
    private const string sceneName = "AstralTestScene";
    private bool sceneLoaded = false;
    private GameObject player;
    private GameObject hud;
    private GameObject party;
    private IPlayerMovement playerMovement;
    private IAstralMeterLogic astralMeterLogic;
    //private KeyboardMouseTestFixture inputTestFixture = new KeyboardMouseTestFixture();

    private float GetMovementDurationTowards(Transform target)
    {
        float moveDistance = Utils.GeometryCalcu.GetDistance3D(player.transform.position, target.transform.position);
        float moveDuration = Utils.PlayerHelper.DistanceToMoveDuration(playerMovement, moveDistance);
        return moveDuration;
    }

    #region Setup Teardown
    [SetUp]
    public void PlayerSetUp()
    {
        //inputTestFixture.Setup();
        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.LoadScene(sceneName);
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        sceneLoaded = true;
        GameObject[] gameObjects = scene.GetRootGameObjects();
        foreach (GameObject gameObject in gameObjects)
        {
            if (gameObject.name == "Player")
            {
                player = gameObject;
                playerMovement = player.GetComponent<IPlayerMovement>();
            }
            else if (gameObject.name == "Canvas")
            {
                hud = gameObject;
            }
            else if (gameObject.name == "Party")
            {
                party = gameObject;
            }
        }
    }

    [TearDown]
    public void PlayerTearDown()
    {
        //inputTestFixture.TearDown();
        //SceneManager.UnloadSceneAsync(sceneName);
    }
    #endregion

    #region Astral World
    [UnityTest]
    public IEnumerator AstralMeter_RealWorldAccumulation()
    {
        yield return new WaitWhile(() => sceneLoaded == false);
        IAstralMeterLogic astralMeterLogic = party.GetComponent<IAstralMeterLogic>();

        yield return new WaitForSeconds(1.0f);
        Assert.IsTrue(astralMeterLogic.IsOnRealWorld());
        Assert.AreEqual(3.0f, astralMeterLogic.GetAstralMeter() * 60, 0.8f);
    }

    [UnityTest]
    public IEnumerator AstralMeter_AstralWorldAccumulation()
    {
        yield return new WaitWhile(() => sceneLoaded == false);
        IAstralMeterLogic astralMeterLogic = party.GetComponent<IAstralMeterLogic>();
        astralMeterLogic.ChangeWorld();

        yield return new WaitForSeconds(1.0f);
        Assert.IsFalse(astralMeterLogic.IsOnRealWorld());
        Assert.AreEqual(5.0f, astralMeterLogic.GetAstralMeter() * 60, 0.8f);
    }

    [UnityTest]
    public IEnumerator AstralMeter_OnSightAccumulation()
    {
        yield return new WaitWhile(() => sceneLoaded == false);
        IAstralMeterLogic astralMeterLogic = party.GetComponent<IAstralMeterLogic>();
        astralMeterLogic.ChangeWorld();
        astralMeterLogic.ChangeSightState();

        yield return new WaitForSeconds(1.0f);
        Assert.IsFalse(astralMeterLogic.IsOnRealWorld());
        Assert.AreEqual(1.0f, astralMeterLogic.GetAstralMeter(), 0.8f);
    }

    [UnityTest]
    public IEnumerator AstralMeter_NPCWrongAnswer()
    {
        yield return new WaitWhile(() => sceneLoaded == false);
        IAstralMeterLogic astralMeterLogic = party.GetComponent<IAstralMeterLogic>();
        astralMeterLogic.ChangeWorld();
        astralMeterLogic.NPCWrongAnswer();

        Assert.IsFalse(astralMeterLogic.IsOnRealWorld());
        Assert.IsTrue(astralMeterLogic.GetAstralMeter() >= 10.0f);
        Assert.IsTrue(astralMeterLogic.GetAstralMeter() <= 15.0f);
    }

    [UnityTest]
    public IEnumerator AstralMeter_PlayerKilled()
    {
        yield return new WaitWhile(() => sceneLoaded == false);
        IAstralMeterLogic astralMeterLogic = party.GetComponent<IAstralMeterLogic>();
        astralMeterLogic.ChangeWorld();
        astralMeterLogic.PlayerKilled();

        Assert.IsFalse(astralMeterLogic.IsOnRealWorld());
        Assert.IsTrue(astralMeterLogic.GetAstralMeter() >= 15.0f);
        Assert.IsTrue(astralMeterLogic.GetAstralMeter() <= 20.0f);
    }
    #endregion
}
