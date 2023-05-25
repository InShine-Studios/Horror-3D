using System.Collections;
using UnityEngine;
public class RangedEnemyHearing : EnemyHearingAbstract
{
    protected override void SetGhostHearingRadiusType()
    {
        ghostHearingRadiusType = Utils.GhostHelper.GhostHearingRadiusType.Average;
    }
}