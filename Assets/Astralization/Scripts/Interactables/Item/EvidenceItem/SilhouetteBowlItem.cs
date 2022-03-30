using UnityEngine;
using System.Collections.Generic;

/*
 * SilhouetteBowl class.
 * Override DetermineEvidence and HandleChange from base EvidenceItem class according to murder silhouette evidence mechanics.
 */
public class SilhouetteBowlItem : EvidenceItem
{
    #region Variables
    private SilhouetteBowlManager _silhouetteBowlManager;
    #endregion

    #region MonoBehaviour
    protected override void Awake()
    {
        base.Awake();
        _silhouetteBowlManager = GetComponent<SilhouetteBowlManager>();
    }
    #endregion

    #region ItemInputHandler
    public override void Use()
    {
        _silhouetteBowlManager.ChangeState<SilhouetteBowlActiveState>();
    }

    public override void OnInteraction()
    {
        base.OnInteraction();
        _silhouetteBowlManager.ChangeState<SilhouetteBowlInactiveState>();
    }

    public override void OnGhostInteraction()
    {
        if (!(_silhouetteBowlManager.GetCurrentState() is SilhouetteBowlInactiveState)) DetermineEvidence();
    }
    #endregion

    #region EvidenceHelper
    public override void DetermineEvidence()
    {
        // TODO this dummy behavior at the moment, wait for Ghost Implementation
        if (_silhouetteBowlManager.GetCurrentState() is SilhouetteBowlNegativeState)
        {
            _silhouetteBowlManager.ChangeState<SilhouetteBowlPositiveState>();
        }
        else _silhouetteBowlManager.ChangeState<SilhouetteBowlNegativeState>();
    }
    #endregion
}
