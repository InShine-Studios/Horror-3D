using UnityEngine;

/*
 * Class to keep all player constants.
 * Use Setter and Getter to access the variables.
 */
public class PlayerBase : MonoBehaviour
{
    #region Variables
    [Header("Movement Constants")]
    [Tooltip("Walk Speed")]
    [SerializeField] private float playerMovementSpeed;

    [Header("Rotation Constants")]
    [Tooltip("Rotation Speed")]
    [SerializeField] private float rotationSpeed;
    #endregion

    public float GetPlayerMovementSpeed(){
        return playerMovementSpeed;
    }

    public float GetRotationSpeed()
    {
        return rotationSpeed;
    }
}
