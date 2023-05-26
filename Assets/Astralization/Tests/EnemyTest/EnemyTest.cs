using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class EnemyTest : TestBase
{
    private GameObject enemy;
    private IEnemyMovement enemyMovement;
    private EnemyStateMachine enemyStateMachine;
    private IEnemyManager enemyManager;

    protected override void FindGameObjects(Scene scene)
    {
        GameObject[] gameObjects = scene.GetRootGameObjects();
        foreach (GameObject gameObject in gameObjects)
        {
            if (gameObject.name == "Enemy")
            {
                enemy = gameObject;
                enemyMovement = enemy.GetComponent<IEnemyMovement>();
                enemyStateMachine = enemy.GetComponent<EnemyStateMachine>();
                enemyManager = enemy.GetComponent<IEnemyManager>();
            }
        }
    }

    #region Setup Teardown
    [SetUp]
    public override void SetUp()
    {
        sceneName = "HouseAbigail_EnemyTest";
        base.SetUp();
    }
    #endregion

    #region Enemy Movement
    //[UnityTest]  // Deprecated test case
    public IEnumerator Enemy_WanderToTargetRoom()
    {
        yield return new WaitWhile(() => sceneLoaded == false);

        string targetRoomName = "Laundry Room";
        IStageManager stageManager = GameObject.Find("Building/StageManager").GetComponent<IStageManager>();
        StagePoint targetRoom = stageManager.GetRoomCoordinate(targetRoomName);

        yield return new WaitWhile(() => enemyStateMachine.CurrentState is EnemyIdleState);
        enemy.GetComponent<IEnemyWanderState>().SetWanderTarget(targetRoomName, false);
        yield return new WaitWhile(enemyMovement.IsOnRoute);
        float delta = Mathf.Abs(
            Utils.GeometryCalcu.GetDistance3D(
                targetRoom.GetPosition(),
                enemy.transform.position
            )
        );
        Assert.IsTrue(delta <= enemyMovement.GetDistanceThreshold());
    }

    //[UnityTest] // Deprecated test case
    public IEnumerator Enemy_WanderingRandomly()
    {
        yield return new WaitWhile(() => sceneLoaded == false);
        Vector3 initialPosition = enemy.transform.position;

        yield return new WaitWhile(() => enemyStateMachine.CurrentState is EnemyIdleState);
        yield return new WaitWhile(enemyMovement.IsOnRoute);
        Assert.AreNotEqual(initialPosition, enemy.transform.position);
    }

    //[UnityTest] // Deprecated test case
    public IEnumerator Enemy_RotatingWhileIdle()
    {
        yield return new WaitWhile(() => sceneLoaded == false);
        Quaternion initialRotation = enemy.transform.rotation;

        yield return new WaitForSeconds(1f);
        Assert.AreNotEqual(initialRotation, enemy.transform.rotation);
    }
    #endregion

    #region EnemyKillAndChase
    //[UnityTest] // Deprecated test case
    public IEnumerator Enemy_ChasingWhileKillPhase()
    {
        yield return new WaitWhile(() => sceneLoaded == false);

        GameObject volume = GameObject.Find("WorldState");
        GameObject volumeAstral = volume.transform.Find("VOL_AstralWorld").gameObject;
        GameObject volumeReal = volume.transform.Find("VOL_RealWorld").gameObject;
        IStateMachine volumeStateMachine = volume.GetComponent<IStateMachine>();
        IEnemyFieldOfView enemyFieldOfView = enemy.GetComponent<IEnemyFieldOfView>();

        yield return new WaitUntil(() => enemyStateMachine.CurrentState is EnemyWanderState);
        ((EnemyWanderState)enemyStateMachine.CurrentState).SetWanderTarget("Living Room", true);

        yield return new WaitUntil(() => enemyManager.IsKillPhase() == true);
        Assert.True(volumeStateMachine.CurrentState is IWorldAstralState);
        Assert.True(volumeAstral.activeInHierarchy);
        Assert.False(volumeReal.activeInHierarchy);

        yield return new WaitWhile(() => enemyFieldOfView.ChasingTarget is null);
        Assert.True(enemyStateMachine.CurrentState is EnemyChasingState);
        float distance = Utils.GeometryCalcu.GetDistance3D(enemyFieldOfView.ChasingTarget.position, enemyMovement.NavMeshAgent.destination);
        Assert.True(distance < 1f);

        enemy.transform.rotation = Quaternion.Inverse(enemy.transform.rotation); // Make the enemy unsee the player
        yield return new WaitUntil(() => enemyFieldOfView.ChasingTarget is null);
        Assert.True(enemyStateMachine.CurrentState is EnemyIdleState);
        Assert.True(enemyFieldOfView.ChasingTarget is null);

        yield return new WaitUntil(() => enemyManager.IsKillPhase() == false);
        Assert.True(volumeStateMachine.CurrentState is IWorldRealState);
        Assert.False(volumeAstral.activeInHierarchy);
        Assert.True(volumeReal.activeInHierarchy);
        Assert.True(enemyStateMachine.CurrentState is EnemyIdleState);
    }
    #endregion
}
