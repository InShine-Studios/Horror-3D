using System.Collections;
using UnityEngine;

public class StandardEnemyMovement : EnemyMovementAbstract
{
    protected override void SetGhostMovementProperty()
    {
        ghostMovementType = Utils.GhostHelper.GhostMovementType.Walk;
        ghostMovementSpeedType = Utils.GhostHelper.GhostMovementSpeedType.Slow;
        ghostChaseType = Utils.GhostHelper.GhostChaseType.Run;
        ghostChaseSpeedType = Utils.GhostHelper.GhostChaseSpeedType.Fast;
    }
}