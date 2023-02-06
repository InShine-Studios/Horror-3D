using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    #region Variables
    private Dictionary<string, CinemachineBrain> _cinemachineBrains = new Dictionary<string, CinemachineBrain>();
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
    }
    #endregion

    #region CameraManagement
    public void DeactivateAllCinemachineBrain()
    {
        foreach(CinemachineBrain c in _cinemachineBrains.Values)
        {
            c.enabled = false;
        }
    }

    public void ActivateCinemachineBrain(string cineBrainName)
    {
        //DeactivateAllCinemachineBrain();
        _cinemachineBrains[cineBrainName].enabled = true;
    }
    #endregion
}
