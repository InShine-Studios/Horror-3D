using System;
using UnityEngine;

namespace Astralization.Interactables.TimeslotChanger
{
    public class DummyTimeslotChanger : Interactable
    {
        #region Events
        public static event Action<int> TimeslotIncrementEvent;
        #endregion

        #region Variable
        [SerializeField]
        [Range(1, 2)]
        [Tooltip("Timeslot increment count when interacted")]
        private int _timeslotIncrement = 1;
        #endregion

        #region SetGet
        #endregion

        #region MonoBehavior
        #endregion

        #region InteractableHandler
        public override void OnInteraction()
        {
            TimeslotIncrementEvent.Invoke(_timeslotIncrement);
        }
        #endregion
    }
}