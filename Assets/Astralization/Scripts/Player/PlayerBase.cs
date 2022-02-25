using UnityEngine;

public interface IPlayerBase
{
    float GetPlayerMovementSpeed();
    float GetRotationSpeed();
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

    [Header("Rotation Constants")]
    [Tooltip("Rotation Speed")]
    [SerializeField] private float _rotationSpeed;

    [Header("Sprint Constants")]
    [Tooltip("Sprint Speed")]
    [SerializeField] private float _sprintSpeed;
    #endregion

    #region SetGet
    public float GetPlayerMovementSpeed()
    {
        return _movementSpeed;
    }

    public float GetRotationSpeed()
    {
        return _rotationSpeed;
    }

    public float GetSprintSpeed()
    {
        return _sprintSpeed;
    }
    #endregion
}
