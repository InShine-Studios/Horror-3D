using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemyStateMachine
{

}

public class EnemyStateMachine : StateMachine, IEnemyStateMachine
{
    #region MonoBehaviour
    private void Awake()
    {
        ChangeState<EnemyInitState>();
    }
    #endregion

    #region Handler
    public void AttemptChasing()
    {
        ChangeState<EnemyChasingState>();
        ((EnemyChasingState)CurrentState).EnemyChasing();
    }
    #endregion
}
