using System.Collections;
using UnityEngine;

public class BlindEnemyCapture : EnemyCaptureAbstract
{
    protected override void SetGhostCaptureProperty()
    {
        ghostCaptureDurationType = Utils.GhostHelper.GhostCaptureDurationType.Instant;
        ghostCaptureZoneType = Utils.GhostHelper.GhostCaptureZoneType.AOE_Sphere;
        ghostCaptureReachType = Utils.GhostHelper.GhostCaptureReachType.Average;
    }
}