using System.Collections.Generic;
using UnityEngine;

namespace Astralization.Managers.StageSystem
{
    public class StageTransitionZoneData : ScriptableObject
    {
        #region Variables
        public List<Vector3> GhostTransitionZonePosition;
        public List<Vector3> GhostTransitionZoneCenter;
        public List<Vector3> GhostTransitionZoneSize;
        public List<TransitionEndpointList> GhostTransitionZoneEndpoint;
        #endregion
    }
}