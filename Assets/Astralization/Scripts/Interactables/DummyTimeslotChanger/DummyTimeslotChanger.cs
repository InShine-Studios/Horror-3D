using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyTimeslotChanger : Interactable
{
    #region Events
    public static event Action<int> TimeJumpEvent;
    #endregion

    #region Variable
    [SerializeField]
    [Range(1, 2)]
    [Tooltip("Timeslot jump count when interacted")]
    private int _timeJumpCount = 1;
    #endregion

    #region SetGet
    #endregion

    #region MonoBehavior
    #endregion

    #region InteractableHandler
    public override void OnInteraction()
    {
        TimeJumpEvent.Invoke(_timeJumpCount);
    }
    #endregion
}
