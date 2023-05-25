using System.Collections;
using UnityEngine;

public class FlyingEnemyCapture : EnemyCaptureAbstract
{
    protected override void SetGhostCaptureProperty()
    {
        ghostCaptureDurationType = Utils.GhostHelper.GhostCaptureDurationType.Delay;
        ghostCaptureZoneType = Utils.GhostHelper.GhostCaptureZoneType.Spotlight;
        ghostCaptureReachType = Utils.GhostHelper.GhostCaptureReachType.Ground;
    }
}