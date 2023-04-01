using Astralization.Items.EvidenceItems;
using UnityEngine;

namespace Astralization.Items.EvidenceItem.ClockStates
{
    public class ClockNegativeState : ClockState, INegativeState
    {
        #region MonoBehaviour
        protected override void Awake()
        {
            base.Awake();
            audioInUse = Resources.Load("EvidenceItem/SFX_Clock_1_Loop", typeof(AudioClip)) as AudioClip;
        }
        #endregion
    }
}