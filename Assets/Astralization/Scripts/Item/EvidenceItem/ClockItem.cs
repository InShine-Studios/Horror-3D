using UnityEngine;

/*
 * Clock class.
 * Override DetermineEvidence and HandleChange from base EvidenceItem class according to murder time stamp evidence mechanics.
 */
public class ClockItem : EvidenceItem
{
    #region Const
    private const string _stateAudio = "StateAudio";
    #endregion

    #region Variables
    private ClockManager _clockManager;
    #endregion

    #region MonoBehaviour
    protected override void Awake()
    {
        base.Awake();
        _clockManager = GetComponent<ClockManager>();
    }
    #endregion

    #region ItemInputHandler
    protected override void ActivateFunctionality()
    {
        _clockManager.ChangeState<ClockActiveState>();
        LogoState = _clockManager.GetStateNum();
    }

    public override void Pick()
    {
        base.Pick();
        _clockManager.ChangeState<ClockInactiveState>(); //comment this for logo testing of another states
        LogoState = _clockManager.GetStateNum();
    }

    public override void OnGhostInteraction()
    {
        if (!(_clockManager.CurrentState is ClockInactiveState)) DetermineEvidence();
    }
    #endregion

    #region EvidenceHelper
    public override void DetermineEvidence()
    {
        // TODO this dummy behavior at the moment, wait for Ghost Implementation
        if (_clockManager.CurrentState is ClockNegativeState)
        {
            _clockManager.ChangeState<ClockPositiveState>();
        }
        else {
            _clockManager.ChangeState<ClockNegativeState>();
        }
        LogoState = _clockManager.GetStateNum();
        PlayAudio(_stateAudio);
    }
    #endregion
}
