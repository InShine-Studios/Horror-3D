using System.Collections;
using UnityEngine;

public class TeleportEnemyMovement : EnemyMovementAbstract
{
    protected override void SetGhostMovementProperty()
    {
        ghostMovementType = Utils.GhostHelper.GhostMovementType.Teleport;
        ghostMovementSpeedType = Utils.GhostHelper.GhostMovementSpeedType.Slow;
        ghostChaseType = Utils.GhostHelper.GhostChaseType.None;
        ghostChaseSpeedType = Utils.GhostHelper.GhostChaseSpeedType.None;
    }
}