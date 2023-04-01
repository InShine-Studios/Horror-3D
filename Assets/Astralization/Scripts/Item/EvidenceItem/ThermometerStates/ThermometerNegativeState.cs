using Astralization.Items.EvidenceItems;
using UnityEngine;

namespace Astralization.Items.EvidenceItem.ThermometerStates
{
    public class ThermometerNegativeState : ThermometerState, INegativeState
    {
        #region MonoBehaviour
        protected override void Awake()
        {
            base.Awake();
            materialInUse = Resources.Load("EvidenceItem/MAT_Thermometer_Negative", typeof(Material)) as Material;
        }
        #endregion
    }
}