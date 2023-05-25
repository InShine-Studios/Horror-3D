using System.Collections;
using UnityEngine;

public class TeleportEnemySight : EnemySightAbstract
{
    protected override void SetGhostSightType()
    {
        ghostSightType = Utils.GhostHelper.GhostLineOfSightType.Average;
    }
}