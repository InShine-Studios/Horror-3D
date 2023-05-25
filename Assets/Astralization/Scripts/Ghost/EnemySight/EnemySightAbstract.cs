using System.Collections;
using UnityEngine;

public abstract class EnemySightAbstract : MonoBehaviour
{
    #region Variables
    protected Utils.GhostHelper.GhostLineOfSightType ghostSightType;
    #endregion

    #region MonoBehaviour
    protected abstract void SetGhostSightType();

    protected Utils.GhostHelper.GhostLineOfSightType GetGhostSightType()
    {
        return ghostSightType;
    }
    #endregion
}