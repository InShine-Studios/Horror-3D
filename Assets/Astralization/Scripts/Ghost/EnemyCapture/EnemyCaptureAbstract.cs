using System.Collections;
using UnityEngine;

public abstract class EnemyCaptureAbstract : MonoBehaviour
{
    #region Variables
    protected Utils.GhostHelper.GhostCaptureDurationType ghostCaptureDurationType;
    protected Utils.GhostHelper.GhostCaptureZoneType ghostCaptureZoneType;
    protected Utils.GhostHelper.GhostCaptureReachType ghostCaptureReachType;
    #endregion

    #region MonoBehaviour
    protected abstract void SetGhostCaptureProperty();

    protected Utils.GhostHelper.GhostCaptureDurationType GetGhostCaptureDurationType()
    {
        return ghostCaptureDurationType;
    }

    protected Utils.GhostHelper.GhostCaptureZoneType GetGhostCaptureZoneType()
    {
        return ghostCaptureZoneType;
    }

    protected Utils.GhostHelper.GhostCaptureReachType GetGhostCaptureReachType()
    {
        return ghostCaptureReachType;
    }
    #endregion
}