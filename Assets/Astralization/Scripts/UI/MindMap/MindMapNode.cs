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

    public Transform CameraFollow { get; private set; }
    public Transform CameraLookAt { get; private set; }
    #endregion

    #region SetGet
    public void SetCameraFollowPosition(Vector3 position)
    {
        CameraFollow.transform.localPosition = position;
    }

    public void SetCameraLookAtPosition(Vector3 position)
    {
        CameraLookAt.transform.localPosition = position;
    }
    #endregion

    #region MonoBehaviour
    private void Awake()
    {
        CameraFollow = transform.Find(CameraFollowGameobjectName);
        CameraLookAt = transform.Find(CameraLookAtGameobjectName);
    }
    #endregion
}
