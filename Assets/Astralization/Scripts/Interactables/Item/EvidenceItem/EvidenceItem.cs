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
    #region Variables
    [Header("States")]
    [SerializeField]
    [Tooltip("Current state")]
    public EvidenceItemState state = EvidenceItemState.BASE;
    #endregion

    #region SetGet
    public void SetState(EvidenceItemState state)
    {
        this.state = state;
        HandleChange();
    }
    #endregion

    #region Use
    public override void Use()
    {
        SetState(EvidenceItemState.ACTIVE);
    }
    #endregion

    #region Handler
    public override void OnInteraction()
    {
        SetState(EvidenceItemState.BASE);
        base.OnInteraction();
    }

    public void OnGhostInteraction()
    {
        if (state != EvidenceItemState.BASE) DetermineEvidence();
    }

    public abstract void HandleChange();

    #endregion

    #region Evidence related
    public abstract void DetermineEvidence();
    #endregion
}
