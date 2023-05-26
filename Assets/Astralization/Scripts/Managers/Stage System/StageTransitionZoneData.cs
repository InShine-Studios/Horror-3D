using System;
using System.Collections.Generic;
using UnityEngine;

public class StageTransitionZoneData : ScriptableObject
{
    #region Variables
    public List<Vector3> EnemyTransitionZonePosition;
    public List<Vector3> EnemyTransitionZoneCenter;
    public List<Vector3> EnemyTransitionZoneSize;
    public List<TransitionEndpointList> EnemyTransitionZoneEndpoint;
    #endregion
}
