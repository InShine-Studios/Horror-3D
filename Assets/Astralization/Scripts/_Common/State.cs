using UnityEngine;
using UnityEngine.InputSystem;

// State base class for all states in the game
public abstract class State : MonoBehaviour
{
    public virtual void Enter()
    {
        AddListeners();
    }

    public virtual void Exit()
    {
        RemoveListeners();
    }
    protected virtual void OnDestroy()
    {
        RemoveListeners();
    }
    protected virtual void AddListeners()
    {
    }

    protected virtual void RemoveListeners()
    {
    }
}
