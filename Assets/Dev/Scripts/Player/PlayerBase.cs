using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBase : MonoBehaviour
{
    #region Variables
    [SerializeField] private float playerMoveentSpeed;

    public float GetPlayerMovementSpeed(){
        return playerMoveentSpeed;
    }

    #endregion
}
