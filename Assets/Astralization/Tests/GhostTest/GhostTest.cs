using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class GhostTest : TestBase
{
    private GameObject ghost;
    private IGhostMovement ghostMovement;
    private IGhostStateMachine ghostStateMachine;
    private IGhostManager ghostManager;

    protected override void FindGameObjects(Scene scene)
    {
        GameObject[] gameObjects = scene.GetRootGameObjects();
        foreach (GameObject gameObject in gameObjects)
        {
            if (gameObject.name == "Ghost")
            {
                ghost = gameObject;
                ghostMovement = ghost.GetComponent<IGhostMovement>();
                ghostStateMachine = ghost.GetComponent<IGhostStateMachine>();
                ghostManager = ghost.GetComponent<IGhostManager>();
            }
        }
    }

    #region Setup Teardown
    [SetUp]
    public override void SetUp()
    {
        sceneName = "HouseAbigail_GhostTest";
        base.SetUp();
    }
    #endregion

    #region Ghost Movement
    [UnityTest]
    public IEnumerator Ghost_WanderToTargetRoom()
    {
        yield return new WaitWhile(() => sceneLoaded == false);

        string targetRoomName = "Laundry Room";
        IStageManager stageManager = GameObject.Find("Stage/StageManager").GetComponent<IStageManager>();
        WorldPoint targetRoom = stageManager.GetRoomCoordinate(targetRoomName);

        yield return new WaitWhile(() => ghostStateMachine.GetCurrentGhostState() is IdleGhostState);
        ghost.GetComponent<IWanderGhostState>().SetWanderTarget(targetRoomName,false);
        yield return new WaitWhile(ghostMovement.IsOnRoute);
        float delta = Mathf.Abs(
            Utils.GeometryCalcu.GetDistance3D(
                targetRoom.GetPosition(),
                ghost.transform.position
            )
        );
        Assert.IsTrue(delta <= ghostMovement.GetDistanceThreshold());
    }

    [UnityTest]
    public IEnumerator Ghost_WanderingRandomly()
    {
        yield return new WaitWhile(() => sceneLoaded == false);
        Vector3 initialPosition = ghost.transform.position;

        yield return new WaitWhile(() => ghostStateMachine.GetCurrentGhostState() is IdleGhostState);
        yield return new WaitWhile(ghostMovement.IsOnRoute);
        Assert.AreNotEqual(initialPosition, ghost.transform.position);
    }

    [UnityTest]
    public IEnumerator Ghost_RotatingWhileIdle()
    {
        yield return new WaitWhile(() => sceneLoaded == false);
        Quaternion initialRotation = ghost.transform.rotation;

        yield return new WaitForSeconds(1f);
        Assert.AreNotEqual(initialRotation, ghost.transform.rotation);
    }

    [UnityTest]
    public IEnumerator Ghost_KillPhase()
    {
        yield return new WaitWhile(() => sceneLoaded == false);

        GameObject volume = GameObject.Find("WorldState");
        GameObject volumeAstral = volume.transform.Find("VOL_AstralWorld").gameObject;
        GameObject volumeReal = volume.transform.Find("VOL_RealWorld").gameObject;
        IStateMachine script = volume.GetComponent<IStateMachine>();

        ghostManager.StartKillPhase();
        yield return new WaitUntil(() => ghostManager.IsKillPhase() == true);
        Assert.True(script.CurrentState is IWorldAstralState);
        Assert.True(volumeAstral.activeInHierarchy);
        Assert.False(volumeReal.activeInHierarchy);

        yield return new WaitUntil(() => ghostManager.IsKillPhase() == false);
        Assert.True(script.CurrentState is IWorldRealState);
        Assert.False(volumeAstral.activeInHierarchy);
        Assert.True(volumeReal.activeInHierarchy);
    }
    #endregion
}
