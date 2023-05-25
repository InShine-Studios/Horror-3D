using System.Collections;
using UnityEngine;
public class StandardEnemyCapture : EnemyCaptureAbstract
{
    protected override void SetGhostCaptureProperty()
    {
        ghostCaptureDurationType = Utils.GhostHelper.GhostCaptureDurationType.Instant;
        ghostCaptureZoneType = Utils.GhostHelper.GhostCaptureZoneType.In_Front;
        ghostCaptureReachType = Utils.GhostHelper.GhostCaptureReachType.Average;
    }
}