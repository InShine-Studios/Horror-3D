using System.Collections.Generic;
using UnityEngine;

namespace Astralization.Managers.StageSystem
{
    public class StagePointsData : ScriptableObject
    {
        #region Variables
        public List<Vector3> Positions;
        public List<string> Names;
        public List<float> Rads;
        #endregion
    }
}