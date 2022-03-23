using UnityEngine;

/*
 * Clock class.
 * Override DetermineEvidence and HandleChange from base EvidenceItem class according to murder time stamp evidence mechanics.
 */
public class ClockItem : EvidenceItem
{
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

    #region Use
    public override void Use()
    {
        _clockManager.ChangeState<ClockActiveStates>();
        PlayAudio("StateAudio");
    }
    #endregion

    #region Handler
    public override void OnInteraction()
    {
        _clockManager.ChangeState<ClockInactiveStates>();
        PlayAudio("StateAudio");
        base.OnInteraction();
    }

    public override void OnGhostInteraction()
    {
        if (!(_clockManager.GetCurrentState() is ClockInactiveStates)) DetermineEvidence();
    }
    #endregion

    #region Evidence related
    public override void DetermineEvidence()
    {
        // TODO this dummy behavior at the moment, wait for Ghost Implementation
        if (_clockManager.GetCurrentState() is ClockNegativeState)
        {
            _clockManager.ChangeState<ClockPositiveState>();
            PlayAudio("StateAudio");
        }
        else {
            _clockManager.ChangeState<ClockNegativeState>();
            PlayAudio("StateAudio");
        }
    }
    #endregion
}
