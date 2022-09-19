using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITimeslotState
{
    string TimeName { get; }
    int TimeNum { get; }
}

public class TimeslotState : State, ITimeslotState
{
    #region Variables
    protected TimeslotStateMachine owner;
    protected int timeNum;
    protected string timeName;
    #endregion

    #region SetGet
    public int TimeNum { get { return timeNum; } }
    public string TimeName { get { return timeName; } }
    #endregion

    #region MonoBehaviour
    protected virtual void Awake()
    {
        owner = GetComponent<TimeslotStateMachine>();
    }
    #endregion

    #region StateHandler
    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }
    #endregion
}
