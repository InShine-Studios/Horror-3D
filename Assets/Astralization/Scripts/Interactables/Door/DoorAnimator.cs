using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Astralization.Interactables.Door
{
    public class DoorAnimator : MonoBehaviour
    {
        #region Variables
        private DoorController _doorController;
        #endregion

        #region MonoBehaviour
        private void Awake()
        {
            _doorController = GetComponentInChildren<DoorController>();
        }
        #endregion

        #region Handler
        public void StartTransition()
        {
            _doorController.SetIsTransitioning(false);
        }

        public void FinishTransition()
        {
            _doorController.SetIsTransitioning(true);
        }
        #endregion
    }
}