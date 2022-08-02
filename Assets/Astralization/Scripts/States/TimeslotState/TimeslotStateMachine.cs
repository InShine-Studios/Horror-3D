using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct DateTimeslot
{
    public DateTime Date;
    public ITimeslotState Timeslot;
}

public class TimeslotStateMachine : StateMachine
{
    #region Events
    public static event Action<TimeslotState> UpdateTimeHudEvent;
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

    private ITimeslotHud _timeslotHud;

    private static TimeslotStateMachine _instance;
    public static TimeslotStateMachine Instance { get { return _instance; } }
    #endregion

    #region SetGet
    public TimeslotState GetCurrentState()
    {
        return (TimeslotState)CurrentState;
    }

    public DateTimeslot CurrentDateTimeslot { get { return _currentDateTimeslot; } }
    #endregion

    #region MonoBehaviour
    private void Awake()
    {
        ChangeState<TimeslotMorningState>();

        _timeslotCount = timeNumMapper.Count;
        _currentDateTimeslot.Date = new DateTime(1, 1, 1);
        _currentDateTimeslot.Timeslot = GetCurrentState();

        _timeslotHud = TimeslotHud.Instance;
        _timeslotHud.SetDateDay(_currentDateTimeslot.Date);
        _timeslotHud.SetTimeslot(_currentDateTimeslot.Timeslot);
        _instance = this;
    }
    #endregion

    #region StateHandler
    private void ChangeTime(string timeName)
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
        _currentDateTimeslot.Timeslot = GetCurrentState();
    }

    public void AdvanceTime(int timeStep)
    {
        TimeslotState currentState = GetCurrentState();
        int newTime = (currentState).TimeNum + timeStep;
        if (newTime >= _timeslotCount)
        {
            _currentDateTimeslot.Date = _currentDateTimeslot.Date.AddDays(1);
            _timeslotHud.SetDateDay(_currentDateTimeslot.Date);
        }
        string newTimeName = timeNumMapper[Utils.MathCalcu.mod(newTime,_timeslotCount)];
        
        ChangeTime(newTimeName);
        UpdateTimeHudEvent.Invoke(currentState);

        _timeslotHud.SetTimeslot(_currentDateTimeslot.Timeslot);

        //Debug.Log(string.Format("[TIMESLOT] Advance timeslot by {0}", timeStep));
    }
    #endregion
}
