using System.Collections;
using UnityEngine;

public class TeleportEnemyCapture : EnemyCaptureAbstract
{
    protected override void SetGhostCaptureProperty()
    {
        ghostCaptureDurationType = Utils.GhostHelper.GhostCaptureDurationType.Delay;
        ghostCaptureZoneType = Utils.GhostHelper.GhostCaptureZoneType.AOE_Sphere;
        ghostCaptureReachType = Utils.GhostHelper.GhostCaptureReachType.Average;
    }
}