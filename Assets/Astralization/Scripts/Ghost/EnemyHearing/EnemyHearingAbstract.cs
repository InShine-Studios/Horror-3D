using System.Collections;
using UnityEngine;
public abstract class EnemyHearingAbstract : MonoBehaviour
{
    #region Variables
    protected Utils.GhostHelper.GhostHearingRadiusType ghostHearingRadiusType;
    #endregion

    #region MonoBehaviour
    protected abstract void SetGhostHearingRadiusType();

    protected Utils.GhostHelper.GhostHearingRadiusType GetGhostHearingRadiusType()
    {
        return ghostHearingRadiusType;
    }
    #endregion
}