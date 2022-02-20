using UnityEngine;
using UnityEngine.InputSystem;

public abstract class PlayerState : State
{
    protected InputManager owner;
    protected virtual void Awake()
    {
        owner = GetComponent<InputManager>();
    }
}
