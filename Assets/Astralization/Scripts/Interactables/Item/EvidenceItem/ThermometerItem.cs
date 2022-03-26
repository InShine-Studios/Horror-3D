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
    public override void Use()
    {
        _thermometerManager.ChangeState<ThermometerActiveState>();
    }

    public override void OnInteraction()
    {
        base.OnInteraction();
        _thermometerManager.ChangeState<ThermometerInactiveState>();
    }

    public override void OnGhostInteraction()
    {
        if (!(_thermometerManager.GetCurrentState() is ThermometerInactiveState)) DetermineEvidence();
    }
    #endregion

    #region EvidenceHandler
    public override void DetermineEvidence()
    {
        // TODO this dummy behavior at the moment, wait for Ghost Implementation
        if (_thermometerManager.GetCurrentState() is ThermometerNegativeState)
        {
            _thermometerManager.ChangeState<ThermometerPositiveState>();
        }
        else _thermometerManager.ChangeState<ThermometerNegativeState>();
    }
    #endregion
}
