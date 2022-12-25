using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class MindMapCameraManager : MonoBehaviour
{
    #region Variables
    private CinemachineVirtualCamera[] _virtualCameras;
    private HidingCameraConfigs _povCameraConfig;
    private CinemachinePOVExtension[] _cinemachinePovExtensions;
    private CinemachineBrain _cinemachineBrain;

    private int _activeCameraIndex;
    private int _cameraCount;
    private bool _onTransition = false;
    public bool IsOnTransition { get { return _onTransition; } }
    #endregion

    #region SetGet

    #endregion

    #region MonoBehaviour
    private void Awake()
    {
        _cinemachineBrain = GetComponent<CinemachineBrain>();
        _virtualCameras = GetComponentsInChildren<CinemachineVirtualCamera>();

        _cameraCount = _virtualCameras.Length;
        _activeCameraIndex = 0;
        for (int i = 0; i < _cameraCount; i++)
        {
            if (i != _activeCameraIndex)
            {
                _virtualCameras[_activeCameraIndex].enabled = false;
            }
        }
    }
    #endregion

    #region Camera Handler
    private IEnumerator Transitioning()
    {
        yield return new WaitForSeconds(_cinemachineBrain.m_DefaultBlend.m_Time);
        _onTransition = false;
    }

    public void FocusOn(Transform follow, Transform lookAt)
    {
        _onTransition = true;

        int prevIndex = _activeCameraIndex;
        _activeCameraIndex = Utils.MathCalcu.mod(_activeCameraIndex + 1, _cameraCount);
        _virtualCameras[prevIndex].enabled = false;
        _virtualCameras[_activeCameraIndex].enabled = true;
        _virtualCameras[_activeCameraIndex].Follow = follow;
        _virtualCameras[_activeCameraIndex].LookAt = lookAt;

        StartCoroutine(Transitioning());
    }
    #endregion
}
