using System.Collections;
using UnityEngine;

public class StandardEnemySight : EnemySightAbstract
{
    protected override void SetGhostSightType()
    {
        ghostSightType = Utils.GhostHelper.GhostLineOfSightType.Average;
    }
}