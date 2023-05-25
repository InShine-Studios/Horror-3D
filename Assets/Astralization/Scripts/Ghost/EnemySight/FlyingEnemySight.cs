using System.Collections;
using UnityEngine;

public class FlyingEnemySight : EnemySightAbstract
{
    protected override void SetGhostSightType()
    {
        ghostSightType = Utils.GhostHelper.GhostLineOfSightType.Ground;
    }
}