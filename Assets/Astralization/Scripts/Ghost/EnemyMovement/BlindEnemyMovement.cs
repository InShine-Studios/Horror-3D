using System.Collections;
using UnityEngine;

public class BlindEnemyMovement : EnemyMovementAbstract
{
    protected override void SetGhostMovementProperty()
    {
        ghostMovementType = Utils.GhostHelper.GhostMovementType.Walk;
        ghostMovementSpeedType = Utils.GhostHelper.GhostMovementSpeedType.Slow;
        ghostChaseType = Utils.GhostHelper.GhostChaseType.Charge;
        ghostChaseSpeedType = Utils.GhostHelper.GhostChaseSpeedType.Fast;
    }
}