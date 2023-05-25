using System.Collections;
using UnityEngine;

public class FlyingEnemyHearing : EnemyHearingAbstract
{
    protected override void SetGhostHearingRadiusType()
    {
        ghostHearingRadiusType = Utils.GhostHelper.GhostHearingRadiusType.Short;
    }
}