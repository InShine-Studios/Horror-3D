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

    #region Use
    public override void Use()
    {
        _silhouetteBowlManager.ChangeState<SilhouetteBowlActiveState>();
    }
    #endregion

    #region Handler
    public override void OnInteraction()
    {
        _silhouetteBowlManager.ChangeState<SilhouetteBowlInactiveState>();
        base.OnInteraction();
    }

    public override void OnGhostInteraction()
    {
        if (!(_silhouetteBowlManager.GetCurrentState() is SilhouetteBowlInactiveState)) DetermineEvidence();
    }
    #endregion

    #region Evidence related
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
