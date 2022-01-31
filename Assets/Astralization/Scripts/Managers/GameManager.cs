using System;
using UnityEngine;

public interface IGameManager
{
    bool GetWorld();
    void InvokeChangeWorld();
}

public class GameManager : MonoBehaviour, IGameManager
{
    #region Variables
    [Tooltip("Bool flag to check if the player is in Real World or Astral World")]
    [SerializeField]
    private bool _isInAstralWorld = false;
    #endregion

    public static event Action<bool> ChangeWorldEvent;

    #region Setter Getter
    public bool GetWorld() { return _isInAstralWorld; }
    #endregion

    public void InvokeChangeWorld()
    {
        _isInAstralWorld = !_isInAstralWorld;
        ChangeWorldEvent?.Invoke(_isInAstralWorld);
    }

    private void OnEnable()
    {
        Ankh.ChangeWorldGM += InvokeChangeWorld;
    }

    private void OnDisable()
    {
        Ankh.ChangeWorldGM -= InvokeChangeWorld;
    }
}
