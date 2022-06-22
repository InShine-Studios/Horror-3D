using UnityEngine;

/*
 * Thermometer class.
 * Override DetermineEvidence and HandleChange from base EvidenceItem class according to murder environment temperature evidence mechanics.
 */
public class ThermometerItem : EvidenceItem
{
    #region Variables
    private ThermometerManager _thermometerManager;
    #endregion

    #region MonoBehaviour
    protected override void Awake()
    {
        base.Awake();
        _thermometerManager = GetComponent<ThermometerManager>();
    }
    #endregion

    #region Handler
    protected override void ActivateFunctionality()
    {
        _thermometerManager.ChangeState<ThermometerActiveState>();
    }

    public override void Pick()
    {
        base.Pick();
        _thermometerManager.ChangeState<ThermometerInactiveState>();
    }

    public override void OnGhostInteraction()
    {
        if (!(_thermometerManager.CurrentState is ThermometerInactiveState)) DetermineEvidence();
    }
    #endregion

    #region EvidenceHelper
    public override void DetermineEvidence()
    {
        // TODO this dummy behavior at the moment, wait for Ghost Implementation
        if (_thermometerManager.CurrentState is ThermometerNegativeState)
        {
            _thermometerManager.ChangeState<ThermometerPositiveState>();
        }
        else _thermometerManager.ChangeState<ThermometerNegativeState>();
    }
    #endregion
}
