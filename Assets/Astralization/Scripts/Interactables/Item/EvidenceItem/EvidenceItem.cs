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
    void OnGhostInteraction();
    void SetState(EvidenceItemState state);
}

public abstract class EvidenceItem : Item, IEvidenceItem
{

    #region States
    [Header("States")]

    [SerializeField]
    [Tooltip("Current state")]
    public EvidenceItemState state = EvidenceItemState.BASE;

    #endregion

    #region State Materials
    [Header("State Materials")]
    [SerializeField]
    [Tooltip("Material for Base evidence")]
    private Material baseMaterial;

    [SerializeField]
    [Tooltip("Material for Detect Evidence evidence")]
    private Material activeMaterial;

    [SerializeField]
    [Tooltip("Material for Positive evidence")]
    private Material positiveMaterial;

    [SerializeField]
    [Tooltip("Material for Negative evidence")]
    private Material negativeMaterial;
    #endregion

    public override void Use()
    {
        SetState(EvidenceItemState.ACTIVE);
    }

    public new void Discard()
    {
        Debug.Log("[ITEM] Discarded " + this.name);
        SetState(EvidenceItemState.BASE);
        base.Discard();
    }

    public void SetStateMaterial(Material stateMaterial)
    {
        MeshRenderer[] meshes = InteractableIcon.GetComponentsInChildren<MeshRenderer>();
        foreach (MeshRenderer mesh in meshes)
        {
            mesh.material = stateMaterial;
        }
    }

    public void SetState(EvidenceItemState state)
    {
        this.state = state;
        switch (this.state)
        {
            case EvidenceItemState.BASE:
                SetStateMaterial(baseMaterial);
                break;
            case EvidenceItemState.ACTIVE:
                SetStateMaterial(activeMaterial);
                break;
            case EvidenceItemState.POSITIVE:
                SetStateMaterial(positiveMaterial);
                break;
            case EvidenceItemState.NEGATIVE:
                SetStateMaterial(negativeMaterial);
                break;
            default:
                SetStateMaterial(baseMaterial);
                break;
        }
    }

    public void OnGhostInteraction()
    {
        if (state != EvidenceItemState.BASE) DetermineEvidence();
    }

    public abstract void DetermineEvidence();

}