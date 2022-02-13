using UnityEngine;

public enum EvidenceItemState
{
    BASE,
    ACTIVE,
    POSITIVE,
    NEGATIVE
}

public interface IEvidenceItem : IItem
{
    void DetermineEvidence();
    void HandleChange();
    void OnGhostInteraction();
    void SetState(EvidenceItemState state);
}

/*
 * EvidenceItem abstract class.
 * Parent class for all evidence item objects.
 * Implement DetermineEvidence() and HandleChange() function on each child class.
 */
public abstract class EvidenceItem : Item, IEvidenceItem
{

    #region States
    [Header("States")]
    [SerializeField]
    [Tooltip("Current state")]
    public EvidenceItemState state = EvidenceItemState.BASE;
    #endregion

    public override void Use()
    {
        SetState(EvidenceItemState.ACTIVE);
    }

    public override void OnInteraction()
    {
        SetState(EvidenceItemState.BASE);
        base.OnInteraction();
    }

    public void SetState(EvidenceItemState state)
    {
        this.state = state;
        HandleChange();
    }

    public void OnGhostInteraction()
    {
        if (state != EvidenceItemState.BASE) DetermineEvidence();
    }

    public abstract void DetermineEvidence();

    public abstract void HandleChange();

}
