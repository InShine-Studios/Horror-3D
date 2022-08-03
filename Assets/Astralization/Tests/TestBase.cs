using NUnit.Framework;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public abstract class TestBase
{
    protected const string UiCanvas = "HudCanvas";
    protected string sceneName = "SceneBase";
    protected bool sceneLoaded = false;
    protected GameObject player;

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

    protected void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        sceneLoaded = true;
        FindGameObjects(scene);
    }

    protected abstract void FindGameObjects(Scene scene);

    [TearDown]
    public virtual void TearDown()
    {
        inputTestFixture.TearDown();
        //SceneManager.UnloadSceneAsync(sceneName);
    }
    #endregion
}
