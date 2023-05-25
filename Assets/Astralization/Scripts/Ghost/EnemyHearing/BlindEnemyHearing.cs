using System.Collections;
using UnityEngine;
public class BlindEnemyHearing : EnemyHearingAbstract
{
    protected override void SetGhostHearingRadiusType()
    {
        ghostHearingRadiusType = Utils.GhostHelper.GhostHearingRadiusType.Long;
    }
}