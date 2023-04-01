using System;
using UnityEngine;

namespace Astralization.Interactables.Victim
{
    public interface IVictimController
    {
        bool GetOneFinished();
        bool GetTwoFinished();
    }


    /*
     * Class to control Victim states.
     * Inherit Interactable.
     */
    public class VictimController : Interactable, IVictimController
    {
        #region Events
        public static event Action VictimInteractionEvent;
        #endregion

        #region Variables
        private bool _questOneFinished;
        private bool _questTwoFinished;
        #endregion

        #region SetGet
        public bool GetOneFinished()
        {
            return _questOneFinished;
        }

        public bool GetTwoFinished()
        {
            return _questTwoFinished;
        }
        #endregion

        #region Handler
        public override void OnInteraction()
        {
            //Debug.Log("[INTERACTABLE] Victim interacted: " + this.name);
            VictimInteractionEvent?.Invoke();
        }
        #endregion
    }
}