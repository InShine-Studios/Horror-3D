using Astralization.Items.EvidenceItems;
using UnityEngine;

namespace Astralization.Items.EvidenceItem.ClockStates
{
    public class ClockPositiveState : ClockState, IPositiveState
    {
        #region MonoBehaviour
        protected override void Awake()
        {
            base.Awake();
            audioInUse = Resources.Load("EvidenceItem/SFX_Clock_2_Loop", typeof(AudioClip)) as AudioClip;
        }
        #endregion
    }
}