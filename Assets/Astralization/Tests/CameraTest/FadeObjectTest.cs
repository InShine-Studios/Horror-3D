using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;

public class FadeObjectTest : TestBase
{
    #region Variable
    public enum SurfaceType
    {
        Opaque,
        Transparent
    }
    #endregion

    protected override void FindGameObjects(Scene scene)
    {
        GameObject[] gameObjects = scene.GetRootGameObjects();
        foreach (GameObject gameObject in gameObjects)
        {
            if (gameObject.name == "Player")
            {
                player = gameObject.transform.Find("Character").gameObject;
                playerMovement = player.GetComponent<IPlayerMovement>();
            }
        }
    }

    #region Setup Teardown
    [SetUp]
    public override void SetUp()
    {
        sceneName = "FadeObjectTestScene";
        base.SetUp();
    }
    #endregion

    #region FadeObject
    [UnityTest]
    public IEnumerator CameraDetector_FadeObject()
    {
        yield return new WaitWhile(() => sceneLoaded == false);
        GameObject obj1 = GameObject.Find("M_HD_Shelf_Filled_1");

        foreach (Material material in obj1.GetComponent<Renderer>().materials)
        {
            SurfaceType surfaceType = (SurfaceType)material.GetFloat("_Surface");
            Assert.IsTrue(surfaceType == SurfaceType.Opaque);
        }
        yield return new WaitForSeconds(1.0f);

        float moveDuration = GetMovementDurationTowards(obj1.transform) - 0.5f;
        yield return SimulateInput(KeyboardMouseTestFixture.RegisteredInput.MoveRight, false, moveDuration);

        foreach (Material material in obj1.GetComponent<Renderer>().materials)
        {
            SurfaceType surfaceType = (SurfaceType)material.GetFloat("_Surface");
            Assert.IsTrue(surfaceType == SurfaceType.Transparent);
        }
        yield return new WaitForSeconds(1.0f);

        GameObject obj2 = GameObject.Find("M_HD_Shelf_Filled_2");

        foreach (Material material in obj2.GetComponent<Renderer>().materials)
        {
            SurfaceType surfaceType = (SurfaceType)material.GetFloat("_Surface");
            Assert.IsTrue(surfaceType == SurfaceType.Opaque);
        }
        yield return new WaitForSeconds(1.0f);

        moveDuration = GetMovementDurationTowards(obj2.transform);
        yield return SimulateInput(KeyboardMouseTestFixture.RegisteredInput.MoveLeft, false, moveDuration);
        yield return new WaitForSeconds(1.0f);

        foreach (Material material in obj2.GetComponent<Renderer>().materials)
        {
            SurfaceType surfaceType = (SurfaceType)material.GetFloat("_Surface");
            Assert.IsTrue(surfaceType == SurfaceType.Opaque);
        }
        yield return new WaitForSeconds(1.0f);
    }
    #endregion
}
