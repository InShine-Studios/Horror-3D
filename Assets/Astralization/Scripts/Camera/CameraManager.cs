using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    #region Variables
    private Dictionary<string, CinemachineBrain> _cinemachineBrains = new Dictionary<string, CinemachineBrain>();
    private Dictionary<string, Camera> _cameras = new Dictionary<string, Camera>();
    #endregion

    #region SetGet
    private string GetCameraName(Utils.PlayerHelper.States state)
    {
        return state.ToString() + "Camera"; //ALL CAMERA SHOULD FOLLOW THIS NAMING FORMAT
    }
    #endregion

    #region MonoBehaviour
    // Start is called before the first frame update
    void Start()
    {
        CinemachineBrain[] cinemachineBrains = GetComponentsInChildren<CinemachineBrain>();
        foreach (CinemachineBrain c in cinemachineBrains)
        {
            _cinemachineBrains.Add(c.name, c);
        }

        Camera[] cameras = GetComponentsInChildren<Camera>();
        foreach (Camera c in cameras)
        {
            _cameras.Add(c.name, c);
        }
    }

    private void OnEnable()
    {
        GameManager.PlayerStateEvent += ActivateCinemachineBrain;
    }

    private void OnDisable()
    {
        GameManager.PlayerStateEvent -= ActivateCinemachineBrain;
    }
    #endregion

    #region CameraManagement
    public void DeactivateAllCinemachineBrain()
    {
        foreach(CinemachineBrain c in _cinemachineBrains.Values)
        {
            c.enabled = false;
        }

        foreach (Camera c in _cameras.Values)
        {
            c.enabled = false;
        }
    }

    public void ActivateCinemachineBrain(Utils.PlayerHelper.States state)
    {
        DeactivateAllCinemachineBrain();
        _cinemachineBrains[GetCameraName(state)].enabled = true;
        _cameras[GetCameraName(state)].enabled = true;
    }
    #endregion
}
