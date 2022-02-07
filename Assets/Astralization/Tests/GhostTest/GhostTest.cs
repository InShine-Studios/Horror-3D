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
        string targetRoom = "Laundry Room";
        GameObject ghost = GameObject.Find("Ghost");
        IGhostMovement ghostMovement = ghost.GetComponent<IGhostMovement>();

        ghostMovement.SetWandering(false);
        ghostMovement.WanderTarget(StageController.GetRoomCoordinate(targetRoom),false);
        yield return new WaitForSeconds(4f);
        float delta = Mathf.Abs(
            Utils.GeometryCalcu.GetDistance3D(
                StageController.GetRoomCoordinate(targetRoom).coordinate,
                ghost.transform.position
            )
        );
        Assert.IsTrue(delta < 5f);
    }

    [UnityTest]
    public IEnumerator Ghost_WanderingRandomly()
    {
        yield return new WaitWhile(() => sceneLoaded == false);
        GameObject ghost = GameObject.Find("Ghost");
        IGhostMovement ghostMovement = ghost.GetComponent<IGhostMovement>();
        Vector3 initialPosition = ghost.transform.position;

        yield return new WaitForSeconds(5f);
        Assert.AreNotEqual(initialPosition, ghost.transform.position);
    }
    #endregion
}
