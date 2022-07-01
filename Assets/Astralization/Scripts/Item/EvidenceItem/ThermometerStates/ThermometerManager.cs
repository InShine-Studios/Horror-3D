using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThermometerManager : StateMachine
{
    #region Const
    private readonly Dictionary<string, int> _typeStateMapper = new Dictionary<string, int>()
    {
        {nameof(ThermometerInactiveState), 0},
        {nameof(ThermometerActiveState), 1},
        {nameof(ThermometerPositiveState), 2},
        {nameof(ThermometerNegativeState), 3}
    };
    #endregion

    #region SetGet
    public int GetStateNum()
    {
        return _typeStateMapper[CurrentState.GetType().Name];
    }
    #endregion

    #region MonoBehaviour
    private void Awake()
    {
        ChangeState<ThermometerInitState>();
    }
    #endregion
}
