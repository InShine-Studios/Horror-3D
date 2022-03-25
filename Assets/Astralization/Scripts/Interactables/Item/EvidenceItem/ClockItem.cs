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

    #region ItemHandler
    public override void Use()
    {
        _clockManager.ChangeState<ClockActiveState>();
        PlayAudio(_stateAudio);
    }

    public override void OnInteraction()
    {
        _clockManager.ChangeState<ClockInactiveState>();
        PlayAudio(_stateAudio);
        base.OnInteraction();
    }

    public override void OnGhostInteraction()
    {
        if (!(_clockManager.GetCurrentState() is ClockInactiveState)) DetermineEvidence();
    }
    #endregion

    #region EvidenceHandler
    public override void DetermineEvidence()
    {
        // TODO this dummy behavior at the moment, wait for Ghost Implementation
        if (_clockManager.GetCurrentState() is ClockNegativeState)
        {
            _clockManager.ChangeState<ClockPositiveState>();
        }
        else {
            _clockManager.ChangeState<ClockNegativeState>();
        }
        PlayAudio(_stateAudio);
    }
    #endregion
}
