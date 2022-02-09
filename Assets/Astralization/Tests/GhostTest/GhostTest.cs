using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class GhostTest : TestBase
{
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
        GameObject ghost = GameObject.Find("Ghost");
        IGhostMovement ghostMovement = ghost.GetComponent<IGhostMovement>();

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
        GameObject ghost = GameObject.Find("Ghost");
        IGhostMovement ghostMovement = ghost.GetComponent<IGhostMovement>();
        Vector3 initialPosition = ghost.transform.position;

        yield return new WaitWhile(ghostMovement.IsOnRoute);
        Assert.AreNotEqual(initialPosition, ghost.transform.position);
    }
    #endregion
}
