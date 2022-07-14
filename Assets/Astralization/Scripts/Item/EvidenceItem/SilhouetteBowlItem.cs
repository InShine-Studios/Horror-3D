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
    protected override void ActivateFunctionality()
    {
        _silhouetteBowlManager.ChangeState<SilhouetteBowlActiveState>();
        LogoState = _silhouetteBowlManager.GetStateNum();
    }

    public override void Pick()
    {
        base.Pick();
        //_silhouetteBowlManager.ChangeState<SilhouetteBowlInactiveState>(); //commented for the sake of logo testing
        LogoState = _silhouetteBowlManager.GetStateNum();
    }

    public override void OnGhostInteraction()
    {
        if (!(_silhouetteBowlManager.CurrentState is SilhouetteBowlInactiveState)) DetermineEvidence();
    }
    #endregion

    #region EvidenceHelper
    public override void DetermineEvidence()
    {
        // TODO this dummy behavior at the moment, wait for Ghost Implementation
        if (_silhouetteBowlManager.CurrentState is SilhouetteBowlNegativeState)
        {
            _silhouetteBowlManager.ChangeState<SilhouetteBowlPositiveState>();
        }
        else _silhouetteBowlManager.ChangeState<SilhouetteBowlNegativeState>();
        LogoState = _silhouetteBowlManager.GetStateNum();
    }
    #endregion
}
