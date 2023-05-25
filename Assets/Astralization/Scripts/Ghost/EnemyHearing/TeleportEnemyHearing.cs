using System.Collections;
using UnityEngine;

public class TeleportEnemyHearing : EnemyHearingAbstract
{
    protected override void SetGhostHearingRadiusType()
    {
        ghostHearingRadiusType = Utils.GhostHelper.GhostHearingRadiusType.Average;
    }
}