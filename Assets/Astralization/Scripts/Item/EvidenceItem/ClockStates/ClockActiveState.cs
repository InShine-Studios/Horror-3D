using Astralization.Items.EvidenceItems;
using UnityEngine;

namespace Astralization.Items.EvidenceItem.ClockStates
{
    public class ClockActiveState : ClockState, IActiveState
    {
        #region MonoBehaviour
        protected override void Awake()
        {
            base.Awake();
            audioInUse = Resources.Load("EvidenceItem/SFX_Clock_3_Loop", typeof(AudioClip)) as AudioClip;
        }
        #endregion
    }
}