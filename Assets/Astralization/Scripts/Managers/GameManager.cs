using System;
using UnityEngine;

public interface IGameManager
{
    bool GetWorld();
    void InvokeChangeEvent();
}

public class GameManager : MonoBehaviour, IGameManager
{
    #region Variables
    [Tooltip("Bool flag to check if the player is in Real World or Astral World")]
    private bool _isOnRealWorld = true;
    #endregion

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public static event Action ChangeWorldEvent;

    #region Setter Getter
    public bool GetWorld() { return _isOnRealWorld; }
    #endregion

    public void InvokeChangeEvent()
    {
        ChangeWorldEvent?.Invoke();
    }
}
