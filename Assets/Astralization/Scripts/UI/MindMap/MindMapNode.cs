using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MindMapNodeType
{
    CHAPTER,
    CORE,
    CLUE
}

public class MindMapNode : MonoBehaviour
{
    #region Const
    public static string CameraFollowGameobjectName = "CameraFollow";
    public static string CameraLookAtGameobjectName = "CameraLookAt";
    #endregion

    #region Variables
    public string NodeName;
    public string NodeDescription;
    public MindMapNodeType NodeType;
    public RuntimeAnimatorController AnimController;
    [HideInInspector]
    public List<MindMapNode> Children = new List<MindMapNode>();
    public bool IsDiscovered = false;
    public MindMapNode Parent = null;

    [SerializeField]
    [Tooltip("Position relative to node")]
    private Vector3 _cameraStartingPosition;
    [SerializeField]
    [Tooltip("Degree around each axis")]
    private Vector3 _cameraStartingAngle;

    private Transform _cameraFollow;
    private Transform _cameraLookAt;
    #endregion

    #region SetGet
    public void SetCameraFollowPosition(Vector3 position)
    {
        if(_cameraFollow == null) 
        {
           _cameraFollow = transform.Find(CameraFollowGameobjectName);
        }
        _cameraFollow.localPosition = position; 
    }

    public void SetCameraLookAtPosition(Vector3 position)
    {
        if (_cameraLookAt == null)
        {
            _cameraLookAt = transform.Find(CameraLookAtGameobjectName);
        }
        _cameraLookAt.localPosition = position;
    }

    public Transform GetCameraFollow()
    {
        return _cameraFollow;
    }

    public Transform GetCameraLookAt()
    {
        return _cameraLookAt;
    }
    #endregion

    #region MonoBehaviour
    private void Awake()
    {
        Debug.Log("Awake Called");
        _cameraFollow = transform.Find(CameraFollowGameobjectName);
        _cameraLookAt = transform.Find(CameraLookAtGameobjectName);
    }
    #endregion
}
