using Astralization.SPI;
using Astralization.Utils.Calculation;
using System;
using System.Collections.Generic;

namespace Astralization.States.TimeslotStates
{
    public struct DateTimeslot
    {
        public DateTime Date;
        public TimeslotState Timeslot;
    }

    public class TimeslotStateMachine : StateMachine
    {
        #region Events
        public static event Action<DateTimeslot> UpdateTimeslotHudEvent;
        #endregion

        #region Const
        private readonly Dictionary<int, string> timeNumMapper = new Dictionary<int, string>()
    {
        {0, nameof(TimeslotMorningState) },
        {1, nameof(TimeslotAfternoonState) },
        {2, nameof(TimeslotEveningState) }
    };
        #endregion

        #region Variables
        private int _timeslotCount;

        private DateTimeslot _currentDateTimeslot;

        private static TimeslotStateMachine _instance;
        public static TimeslotStateMachine Instance { get { return _instance; } }
        #endregion

        #region SetGet
        private TimeslotState CurrentTime { get { return (TimeslotState)CurrentState; } }

        public DateTimeslot CurrentDateTimeslot { get { return _currentDateTimeslot; } }

        public int TimeslotCount { get { return _timeslotCount; } }
        #endregion

        #region MonoBehaviour
        private void Awake()
        {
            ChangeState<TimeslotMorningState>();

            _timeslotCount = timeNumMapper.Count;
            _currentDateTimeslot.Date = new DateTime(1, 1, 1);
            _currentDateTimeslot.Timeslot = CurrentTime;

            UpdateTimeslotHudEvent?.Invoke(_currentDateTimeslot);
            _instance = this;
        }
        #endregion

        #region StateHandler
        private void ChangeTimeTo(string timeName)
        {
            switch (timeName)
            {
                case nameof(TimeslotMorningState):
                    ChangeState<TimeslotMorningState>();
                    break;
                case nameof(TimeslotAfternoonState):
                    ChangeState<TimeslotAfternoonState>();
                    break;
                case nameof(TimeslotEveningState):
                    ChangeState<TimeslotEveningState>();
                    break;
            }
            _currentDateTimeslot.Timeslot = CurrentTime;
            UpdateTimeslotHudEvent.Invoke(_currentDateTimeslot);
        }

        public void AdvanceTime(int timeStep)
        {
            TimeslotState currentState = CurrentTime;
            int newTime = currentState.TimeNum + timeStep;
            if (newTime >= _timeslotCount)
            {
                _currentDateTimeslot.Date = _currentDateTimeslot.Date.AddDays(1);
            }
            string newTimeName = timeNumMapper[MathCalcu.mod(newTime, _timeslotCount)];

            ChangeTimeTo(newTimeName);

            //Debug.Log(string.Format("[TIMESLOT] Advance timeslot by {0}", timeStep));
        }
        #endregion
    }
}