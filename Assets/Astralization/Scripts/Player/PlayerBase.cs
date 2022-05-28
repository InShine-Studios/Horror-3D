using UnityEngine;

public interface IPlayerBase
{
    float GetPlayerMovementSpeed();
    //float GetRotationSpeed();
    float GetSprintSpeed();
}

/*
 * Class to keep all player constants.
 * Use Setter and Getter to access the variables.
 */
[System.Serializable]
public class PlayerBase : IPlayerBase
{
    #region Variables
    [Header("Movement Constants")]
    [Tooltip("Walk Speed")]
    [SerializeField] private float _movementSpeed;

    [Header("Sprint Constants")]
    [Tooltip("Sprint Speed")]
    [SerializeField] private float _sprintSpeed;
    #endregion

    #region SetGet
    public float GetPlayerMovementSpeed()
    {
        return _movementSpeed;
    }

    public float GetSprintSpeed()
    {
        return _sprintSpeed;
    }
    #endregion
}
