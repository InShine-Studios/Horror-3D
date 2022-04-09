using System;
using System.Collections.Generic;
using UnityEngine;

public class StageData : ScriptableObject
{
    #region Variables
    public List<Vector3> Positions;
    public List<string> Names;
    public List<float> Rads;

    [Space]
    [Header("Ghost Transition Zone")]
    public List<Vector3> GhostTransitionZonePosition;
    public List<Vector3> GhostTransitionZoneCenter;
    public List<Vector3> GhostTransitionZoneSize;
    public List<TransitionEndpointList> GhostTransitionZoneEndpoint;
    #endregion
}
