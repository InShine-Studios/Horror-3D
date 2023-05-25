using System.Collections;
using UnityEngine;

public class RangedEnemySight : EnemySightAbstract
{
    protected override void SetGhostSightType()
    {
        ghostSightType = Utils.GhostHelper.GhostLineOfSightType.Long;
    }
}