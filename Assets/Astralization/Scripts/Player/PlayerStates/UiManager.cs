using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiManager : MonoBehaviour
{
    #region Variables
    #endregion

    #region MonoBehaviour
    private void Awake()
    {

    }

    private void OnEnable()
    {
        // Attach Event listener
    }

    private void OnDisable()
    {
        // Detach Event listener
    }
    #endregion

    #region SetGet
    public void SetPlayerActionMap(Utils.PlayerHelper.States playerState)
    {

        //Debug.Log("[INPUT MAP] New Map: " + _playerInput.currentActionMap);
    }
    #endregion

    #region Input Handler

    #endregion
}
