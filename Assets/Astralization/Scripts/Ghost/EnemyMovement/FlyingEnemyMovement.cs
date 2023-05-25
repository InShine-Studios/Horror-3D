using System.Collections;
using UnityEngine;

public class FlyingEnemyMovement : EnemyMovementAbstract
{
    protected override void SetGhostMovementProperty()
    {
        ghostMovementType = Utils.GhostHelper.GhostMovementType.Flies;
        ghostMovementSpeedType = Utils.GhostHelper.GhostMovementSpeedType.Average;
        ghostChaseType = Utils.GhostHelper.GhostChaseType.Flying;
        ghostChaseSpeedType = Utils.GhostHelper.GhostChaseSpeedType.Fast;
    }
}