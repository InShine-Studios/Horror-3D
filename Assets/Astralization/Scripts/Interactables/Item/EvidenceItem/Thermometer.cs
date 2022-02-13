using UnityEngine;
using System.Collections.Generic;

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

    private Dictionary<EvidenceItemState, Material> stateToMatMapping;

    protected override void Awake()
    {
        base.Awake();
        stateToMatMapping = new Dictionary<EvidenceItemState, Material>() {
            {EvidenceItemState.BASE, this.baseMaterial},
            {EvidenceItemState.ACTIVE, this.activeMaterial},
            {EvidenceItemState.POSITIVE, this.positiveMaterial},
            {EvidenceItemState.NEGATIVE, this.negativeMaterial},
        };
    }

    private void SetStateMaterial(Material stateMaterial)
    {
        MeshRenderer mesh = model.GetComponentInChildren<MeshRenderer>(true);
        mesh.material = stateMaterial;
    }

    public override void HandleChange()
    {
        SetStateMaterial(stateToMatMapping[this.state]);
    }

    public override void DetermineEvidence()
    {
        // NOTE this dummy behavior at the moment, wait for Ghost Implementation
        if (state == EvidenceItemState.NEGATIVE) SetState(EvidenceItemState.POSITIVE);
        else SetState(EvidenceItemState.NEGATIVE);
    }
}
