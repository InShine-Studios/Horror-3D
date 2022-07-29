using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeslotStateMachine : StateMachine
{
    #region Variables
    private Dictionary<int, string> timeNumMapper = new Dictionary<int, string>()
    {
        {1, nameof(TimeslotMorningState) },
        {2, nameof(TimeslotAfternoonState) },
        {3, nameof(TimeslotNightState) }
    };

    private int timeslotCount;

    private static TimeslotStateMachine _instance;
    public static TimeslotStateMachine Instance { get { return _instance; } }
    #endregion

    #region SetGet
    public TimeslotState GetCurrentState()
    {
        return Instance.GetCurrentState();
    }
    #endregion

    #region MonoBehaviour
    private void Awake()
    {
        ChangeState<TimeslotInitState>();
        timeslotCount = timeNumMapper.Count;
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
            case nameof(TimeslotNightState):
                ChangeState<TimeslotNightState>();
                break;
        }
    }
    public void AdvanceTime(int timeStep)
    {
        int currentTimeNum = ((TimeslotState)CurrentState).GetTimeNum();
        string newTimeName = timeNumMapper[Utils.MathCalcu.mod(currentTimeNum+timeStep,timeslotCount)];
        ChangeTime(newTimeName);
    }
    #endregion
}
