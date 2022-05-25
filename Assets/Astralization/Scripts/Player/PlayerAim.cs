using Cinemachine;
using UnityEngine;

/*
 * Class to enable parts of player to rotate.
 * Used in the Rotate game object (child of player).
 * Rotate using mouse position (inpus saction) on screen to world position.
 */
public class PlayerAim : MonoBehaviour
{
    #region Variables
    [Header("External Variables")]
    [Tooltip("Camera Target for Facing Direction")]
    private Transform _cameraTarget;
    private CinemachineOrbitalTransposer _vcam;

    [Header("Rotation Variables")]
    [SerializeField]
    [Tooltip("The rotation speed of camera")]
    private float _rotationSpeed = 0.1f;
    [SerializeField]
    private AxisState _xAxis;
    [SerializeField]
    private AxisState _yAxis;
    #endregion

    #region SetGet
    #endregion

    #region MonoBehaviour
    private void Awake()
    {
        _cameraTarget = transform.Find("Character/CameraTarget");
        _vcam = GetComponentInChildren<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineOrbitalTransposer>();
    }

    //private void FixedUpdate()
    //{
    //    _xAxis.Update(Time.fixedDeltaTime);
    //    _yAxis.Update(Time.fixedDeltaTime);

    //    _cameraTarget.eulerAngles = new Vector3(_yAxis.Value, _xAxis.Value, 0);
    //    Debug.Log(_cameraTarget.eulerAngles);

    //}
    #endregion

    #region CameraAngleAdjuster
    public void SetCameraAngle(Vector2 mouseDelta)
    {
        _cameraTarget.Rotate(0, (mouseDelta.x * _rotationSpeed), 0, Space.World);
        //_vcam.m_FollowOffset.y = Mathf.Clamp((mouseDelta.y * _rotationSpeed), -45, 45);

        //Debug.Log(_cameraTarget.rotation);
    }
    #endregion
}
