using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeslotState : State
{
    #region Variables
    protected TimeslotStateMachine owner;
    protected int TimeNum;
    #endregion

    #region SetGet
    public int GetTimeNum()
    {
        return TimeNum;
    }
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
