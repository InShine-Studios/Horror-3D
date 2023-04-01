using Astralization.Items.EvidenceItems;
using UnityEngine;

namespace Astralization.Items.EvidenceItem.ThermometerStates
{
    public class ThermometerInactiveState : ThermometerState, IInactiveState
    {
        #region MonoBehaviour
        protected override void Awake()
        {
            base.Awake();
            materialInUse = Resources.Load("EvidenceItem/MAT_Thermometer_Base", typeof(Material)) as Material;
        }
        #endregion
    }
}