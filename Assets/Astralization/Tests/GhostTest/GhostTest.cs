using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class GhostTest : TestBase
{
    private GameObject ghost;
    private IGhostMovement ghostMovement;

    protected override void FindGameObjects(Scene scene)
    {
        GameObject[] gameObjects = scene.GetRootGameObjects();
        foreach (GameObject gameObject in gameObjects)
        {
            if (gameObject.name == "Ghost")
            {
                ghost = gameObject;
                ghostMovement = ghost.GetComponent<IGhostMovement>();
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
        RoomCoordinate targetRoom = StageController.GetRoomCoordinate(targetRoomName);

        ghostMovement.SetWandering(false);
        ghostMovement.WanderTarget(targetRoom,false);
        yield return new WaitWhile(ghostMovement.IsOnRoute);
        float delta = Mathf.Abs(
            Utils.GeometryCalcu.GetDistance3D(
                targetRoom.coordinate,
                ghost.transform.position
            )
        );
        Assert.IsTrue(delta < 3f);
    }

    [UnityTest]
    public IEnumerator Ghost_WanderingRandomly()
    {
        yield return new WaitWhile(() => sceneLoaded == false);
        Vector3 initialPosition = ghost.transform.position;

        yield return new WaitWhile(ghostMovement.IsOnRoute);
        Assert.AreNotEqual(initialPosition, ghost.transform.position);
    }
    #endregion
}
