using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraDetector : MonoBehaviour
{

    [SerializeField] private List<int> fadeObjectListId;
    [SerializeField] private List<int> fadedObjectListId;
    [SerializeField] private Transform player;
    private bool processingTransparant = false;
    private bool processingSolid = false;
    private bool alreadyProcessTransparant = false;
    private Transform camera;

    private void Awake()
    {
        fadeObjectListId = new List<int>();
        fadedObjectListId = new List<int>();

        camera = this.transform;
    }

    private void Update()
    {
        detectAllObjectsToPlater();
        makeTransparant();
        makeSolid();
    }

    private void detectAllObjectsToPlater()
    {
        fadeObjectListId.Clear();

        float cameraToPlayerDistance = Vector3.Magnitude(camera.position - player.position);

        Ray rayCameraToPlayer = new Ray(camera.position, player.position - camera.position);
        Ray rayPlayerToCamera = new Ray(player.position, camera.position - player.position);

        var rayCastCameraToPlayer = Physics.RaycastAll(rayCameraToPlayer, cameraToPlayerDistance);
        var rayCastPlayerToCamera = Physics.RaycastAll(rayPlayerToCamera, cameraToPlayerDistance);

        foreach (var obj in rayCastCameraToPlayer)
        {
            if (obj.collider.tag != "Player")
            {
                int objId = obj.collider.GetInstanceID();
                if (!fadeObjectListId.Contains(objId))
                {
                    fadeObjectListId.Add(objId);
                    obj.transform.gameObject.name = objId.ToString();
                }
                //Debug.Log(obj.collider.GetInstanceID());
                //GameObject hitObject = obj.transform.gameObject;
                //Color changeObjOp = hitObject.GetComponent<Renderer>().material.color;
                //changeObjOp.a = 0.5f;
                //hitObject.GetComponent<Renderer>().material.color = changeObjOp;
            }
        }

        foreach (var obj in rayCastPlayerToCamera)
        {
            if (obj.collider.tag != "Player")
            {
                int objId = obj.collider.GetInstanceID();
                if (!fadeObjectListId.Contains(objId))
                {
                    fadeObjectListId.Add(objId);
                    obj.transform.gameObject.name = objId.ToString();
                }
                //Debug.Log(obj.collider.gameObject.GetInstanceID());
                //GameObject hitObject = obj.transform.gameObject;
                //Color changeObjOp = hitObject.GetComponent<Renderer>().material.color;
                //changeObjOp.a = 0.5f;
                //hitObject.GetComponent<Renderer>().material.color = changeObjOp;
            }
        }
    }

    private void makeTransparant()
    {
        if (alreadyProcessTransparant || processingSolid)
        {
            return;
        }

        alreadyProcessTransparant = true;
        processingTransparant = true;
        foreach (int id in fadeObjectListId.ToArray())
        {
            if (!fadedObjectListId.Contains(id))
            {
                GameObject obj = GameObject.Find(id.ToString());
                Color changeObjOp = obj.GetComponent<Renderer>().material.color;
                changeObjOp.a = 0.5f;
                obj.GetComponent<Renderer>().material.color = changeObjOp;

                fadedObjectListId.Add(id);
            }
        }
        processingTransparant = false;
    }

    private void makeSolid()
    {
        if (!alreadyProcessTransparant || processingTransparant)
        {
            return;
        }

        alreadyProcessTransparant = false;
        processingSolid = true;
        foreach (int id in fadedObjectListId.ToArray())
        {
            if (!fadeObjectListId.Contains(id))
            {
                GameObject obj = GameObject.Find(id.ToString());
                Color changeObjOp = obj.GetComponent<Renderer>().material.color;
                changeObjOp.a = 1;
                obj.GetComponent<Renderer>().material.color = changeObjOp;

                fadedObjectListId.Remove(id);
            }

        }
        processingSolid = false;
    }
}
