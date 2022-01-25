using NUnit.Framework;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class TestBase
{
    protected string sceneName = "SceneBase";
    protected bool sceneLoaded = false;
    protected GameObject player;
    protected GameObject hud;
    protected IPlayerMovement playerMovement;
    protected KeyboardMouseTestFixture inputTestFixture = new KeyboardMouseTestFixture();

    protected float GetMovementDurationTowards(Transform target)
    {
        float moveDistance = Utils.GeometryCalcu.GetDistance3D(player.transform.position, target.transform.position);
        float moveDuration = Utils.PlayerHelper.DistanceToMoveDuration(playerMovement, moveDistance);
        return moveDuration;
    }

    public IEnumerator SimulateInput(
        KeyboardMouseTestFixture.RegisteredInput action,
        bool oneFrame = true,
        float waitTime = 0.1f
    )
    {
        inputTestFixture.Press(action);
        if (oneFrame) yield return null;
        else yield return new WaitForSeconds(waitTime);
        inputTestFixture.Release(action);
        yield return null;
    }

    #region Setup Teardown
    [SetUp]
    public virtual void SetUp()
    {
        inputTestFixture.Setup();
        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.LoadScene(sceneName);
    }

    protected virtual void OnSceneLoaded(Scene scene, LoadSceneMode mode)
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
        }
    }

    [TearDown]
    public virtual void TearDown()
    {
        inputTestFixture.TearDown();
        //SceneManager.UnloadSceneAsync(sceneName);
    }
    #endregion
}
