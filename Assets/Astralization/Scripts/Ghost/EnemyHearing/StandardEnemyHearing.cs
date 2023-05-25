using System.Collections;
using UnityEngine;
public class StandardEnemyHearing : EnemyHearingAbstract
{
    protected override void SetGhostHearingRadiusType()
    {
        ghostHearingRadiusType = Utils.GhostHelper.GhostHearingRadiusType.Average;
    }
}