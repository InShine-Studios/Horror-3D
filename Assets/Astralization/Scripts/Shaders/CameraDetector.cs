using System.Collections.Generic;
using UnityEngine;

public class CameraDetector : MonoBehaviour
{

    [SerializeField] private List<int> _fadeObjectListId;
    [SerializeField] private List<int> _fadedObjectListId;
    [SerializeField] private Transform _player;
    [SerializeField] private float _transparantScale;

    private bool _processingTransparant = false;
    private bool _processingSolid = false;
    private bool _alreadyProcessTransparant = false;
    private Transform _camera;

    private void Awake()
    {
        _fadeObjectListId = new List<int>();
        _fadedObjectListId = new List<int>();

        _camera = transform;
    }

    private void Update()
    {
        DetectAllObjectsToPlater();
        MakeTransparant();
        MakeSolid();
    }

    private void DetectAllObjectsToPlater()
    {
        _fadeObjectListId.Clear();

        float cameraToPlayerDistance = Vector3.Magnitude(_camera.position - _player.position);

        Ray rayCameraToPlayer = new Ray(_camera.position, _player.position - _camera.position);
        Ray rayPlayerToCamera = new Ray(_player.position, _camera.position - _player.position);

        var rayCastCameraToPlayer = Physics.RaycastAll(rayCameraToPlayer, cameraToPlayerDistance);
        var rayCastPlayerToCamera = Physics.RaycastAll(rayPlayerToCamera, cameraToPlayerDistance);

        foreach (var obj in rayCastCameraToPlayer)
        {
            if (!obj.collider.CompareTag("Player"))
            {
                int objId = obj.collider.GetInstanceID();
                if (!_fadeObjectListId.Contains(objId))
                {
                    _fadeObjectListId.Add(objId);
                    obj.transform.gameObject.name = objId.ToString();
                }
            }
        }

        foreach (var obj in rayCastPlayerToCamera)
        {
            if (!obj.collider.CompareTag("Player"))
            {
                int objId = obj.collider.GetInstanceID();
                if (!_fadeObjectListId.Contains(objId))
                {
                    _fadeObjectListId.Add(objId);
                    obj.transform.gameObject.name = objId.ToString();
                }
            }
        }
    }

    private void MakeTransparant()
    {
        if (_alreadyProcessTransparant || _processingSolid)
        {
            return;
        }

        _alreadyProcessTransparant = true;
        _processingTransparant = true;
        foreach (int id in _fadeObjectListId.ToArray())
        {
            if (!_fadedObjectListId.Contains(id))
            {
                GameObject obj = GameObject.Find(id.ToString());
                Color changeObjOp = obj.GetComponent<Renderer>().material.color;
                changeObjOp.a = _transparantScale;
                obj.GetComponent<Renderer>().material.color = changeObjOp;

                _fadedObjectListId.Add(id);
            }
        }
        _processingTransparant = false;
    }

    private void MakeSolid()
    {
        if (!_alreadyProcessTransparant || _processingTransparant)
        {
            return;
        }

        _alreadyProcessTransparant = false;
        _processingSolid = true;
        foreach (int id in _fadedObjectListId.ToArray())
        {
            if (!_fadeObjectListId.Contains(id))
            {
                GameObject obj = GameObject.Find(id.ToString());
                Color changeObjOp = obj.GetComponent<Renderer>().material.color;
                changeObjOp.a = 1;
                obj.GetComponent<Renderer>().material.color = changeObjOp;

                _fadedObjectListId.Remove(id);
            }

        }
        _processingSolid = false;
    }
}
