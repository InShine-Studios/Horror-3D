using System.Collections;
using Astralization.Enemy;
using Astralization.Enemy.EnemyStates;
using Astralization.Managers;
using Astralization.Managers.StageSystem;
using Astralization.SPI;
using Astralization.States.TimeslotStates;
using Astralization.States.WorldStates;
using Astralization.Utils.Calculation;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GhostTest : TestBase
{
    private GameObject ghost;
    private IGhostMovement ghostMovement;
    private GhostStateMachine ghostStateMachine;
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
                ghostStateMachine = ghost.GetComponent<GhostStateMachine>();
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
    //[UnityTest]  // Deprecated test case
    public IEnumerator Ghost_WanderToTargetRoom()
    {
        yield return new WaitWhile(() => sceneLoaded == false);

        string targetRoomName = "Laundry Room";
        IStageManager stageManager = GameObject.Find("Building/StageManager").GetComponent<IStageManager>();
        StagePoint targetRoom = stageManager.GetRoomCoordinate(targetRoomName);

        yield return new WaitWhile(() => ghostStateMachine.CurrentState is GhostIdleState);
        ghost.GetComponent<IGhostWanderState>().SetWanderTarget(targetRoomName,false);
        yield return new WaitWhile(ghostMovement.IsOnRoute);
        float delta = Mathf.Abs(
            GeometryCalcu.GetDistance3D(
                targetRoom.GetPosition(),
                ghost.transform.position
            )
        );
        Assert.IsTrue(delta <= ghostMovement.GetDistanceThreshold());
    }

    //[UnityTest] // Deprecated test case
    public IEnumerator Ghost_WanderingRandomly()
    {
        yield return new WaitWhile(() => sceneLoaded == false);
        Vector3 initialPosition = ghost.transform.position;

        yield return new WaitWhile(() => ghostStateMachine.CurrentState is GhostIdleState);
        yield return new WaitWhile(ghostMovement.IsOnRoute);
        Assert.AreNotEqual(initialPosition, ghost.transform.position);
    }

    //[UnityTest] // Deprecated test case
    public IEnumerator Ghost_RotatingWhileIdle()
    {
        yield return new WaitWhile(() => sceneLoaded == false);
        Quaternion initialRotation = ghost.transform.rotation;

        yield return new WaitForSeconds(1f);
        Assert.AreNotEqual(initialRotation, ghost.transform.rotation);
    }
    #endregion

    #region GhostKillAndChase
    //[UnityTest] // Deprecated test case
    public IEnumerator Ghost_ChasingWhileKillPhase()
    {
        yield return new WaitWhile(() => sceneLoaded == false);

        GameObject volume = GameObject.Find("WorldState");
        GameObject volumeAstral = volume.transform.Find("VOL_AstralWorld").gameObject;
        GameObject volumeReal = volume.transform.Find("VOL_RealWorld").gameObject;
        IStateMachine volumeStateMachine = volume.GetComponent<IStateMachine>();
        IGhostFieldOfView ghostFieldOfView = ghost.GetComponent<IGhostFieldOfView>();

        yield return new WaitUntil(() => ghostStateMachine.CurrentState is GhostWanderState);
        ((GhostWanderState)ghostStateMachine.CurrentState).SetWanderTarget("Living Room", true);

        yield return new WaitUntil(() => ghostManager.IsKillPhase() == true);
        Assert.True(volumeStateMachine.CurrentState is IWorldAstralState);
        Assert.True(volumeAstral.activeInHierarchy);
        Assert.False(volumeReal.activeInHierarchy);

        yield return new WaitWhile(() => ghostFieldOfView.ChasingTarget is null);
        Assert.True(ghostStateMachine.CurrentState is GhostChasingState);
        float distance = GeometryCalcu.GetDistance3D(ghostFieldOfView.ChasingTarget.position, ghostMovement.NavMeshAgent.destination);
        Assert.True(distance < 1f);

        ghost.transform.rotation = Quaternion.Inverse(ghost.transform.rotation); // Make the ghost unsee the player
        yield return new WaitUntil(() => ghostFieldOfView.ChasingTarget is null);
        Assert.True(ghostStateMachine.CurrentState is GhostIdleState);
        Assert.True(ghostFieldOfView.ChasingTarget is null);

        yield return new WaitUntil(() => ghostManager.IsKillPhase() == false);
        Assert.True(volumeStateMachine.CurrentState is IWorldRealState);
        Assert.False(volumeAstral.activeInHierarchy);
        Assert.True(volumeReal.activeInHierarchy);
        Assert.True(ghostStateMachine.CurrentState is GhostIdleState);
    }
    #endregion
}
