using System.Collections;
using UnityEngine;

public class BlindEnemySight : EnemySightAbstract
{
    protected override void SetGhostSightType()
    {
        ghostSightType = Utils.GhostHelper.GhostLineOfSightType.None;
    }
}