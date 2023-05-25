using System.Collections;
using UnityEngine;

public abstract class EnemyMovementAbstract : MonoBehaviour
{
    #region Variables
    protected Utils.GhostHelper.GhostMovementType ghostMovementType;
    protected Utils.GhostHelper.GhostMovementSpeedType ghostMovementSpeedType;
    protected Utils.GhostHelper.GhostChaseType ghostChaseType;
    protected Utils.GhostHelper.GhostChaseSpeedType ghostChaseSpeedType;
    #endregion

    #region MonoBehaviour
    protected abstract void SetGhostMovementProperty();

    protected Utils.GhostHelper.GhostMovementType GetGhostMovementType()
    {
        return ghostMovementType;
    }

    protected Utils.GhostHelper.GhostMovementSpeedType GetGhostMovementSpeedType()
    {
        return ghostMovementSpeedType;
    }

    protected Utils.GhostHelper.GhostChaseType GetGhostChaseType()
    {
        return ghostChaseType;
    }

    protected Utils.GhostHelper.GhostChaseSpeedType GetGhostChaseSpeedType()
    {
        return ghostChaseSpeedType;
    }
    #endregion
}