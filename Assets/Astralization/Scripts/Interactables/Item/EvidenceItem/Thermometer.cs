using UnityEngine;

public class Thermometer : EvidenceItem
{

    #region Model
    [Header("Model reference")]
    [SerializeField]
    [Tooltip("Material for Base evidence")]
    private GameObject model;
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

    private void SetStateMaterial(Material stateMaterial)
    {
        MeshRenderer mesh = model.GetComponentInChildren<MeshRenderer>(true);
        mesh.material = stateMaterial;
        Debug.Log("[MAT] " + mesh.material.name);
    }

    public override void HandleChange()
    {
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

    public override void DetermineEvidence()
    {
        // NOTE this dummy behavior at the moment, wait for Ghost Implementation
        if (state == EvidenceItemState.NEGATIVE) SetState(EvidenceItemState.POSITIVE);
        else SetState(EvidenceItemState.NEGATIVE);
    }
}
