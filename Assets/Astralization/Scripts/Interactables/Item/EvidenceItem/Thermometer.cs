using UnityEngine;
using System.Collections.Generic;


/*
 * Thermometer class.
 * Override DetermineEvidence and HandleChange from base EvidenceItem class according to murder environment temperature evidence mechanics.
 */
public class Thermometer : EvidenceItem
{

    #region Model
    [Header("Model reference")]
    [SerializeField]
    [Tooltip("Model reference")]
    private GameObject _model;
    #endregion

    #region State Materials
    [Header("State Materials")]
    [SerializeField]
    [Tooltip("Material for Base evidence")]
    private Material _baseMaterial;

    [Space]
    [SerializeField]
    [Tooltip("Material for Detect Evidence evidence")]
    private Material _activeMaterial;

    [Space]
    [SerializeField]
    [Tooltip("Material for Positive evidence")]
    private Material _positiveMaterial;

    [Space]
    [SerializeField]
    [Tooltip("Material for Negative evidence")]
    private Material _negativeMaterial;

    #endregion

    private Dictionary<EvidenceItemState, Material> stateToMatMapping;

    protected override void Awake()
    {
        base.Awake();
        stateToMatMapping = new Dictionary<EvidenceItemState, Material>() {
            {EvidenceItemState.BASE, this._baseMaterial},
            {EvidenceItemState.ACTIVE, this._activeMaterial},
            {EvidenceItemState.POSITIVE, this._positiveMaterial},
            {EvidenceItemState.NEGATIVE, this._negativeMaterial},
        };
    }

    private void SetStateMaterial(Material stateMaterial)
    {
        MeshRenderer mesh = _model.GetComponentInChildren<MeshRenderer>(true);
        mesh.material = stateMaterial;
    }

    public override void HandleChange()
    {
        SetStateMaterial(stateToMatMapping[this.state]);
    }

    public override void DetermineEvidence()
    {
        // TODO this dummy behavior at the moment, wait for Ghost Implementation
        if (state == EvidenceItemState.NEGATIVE) SetState(EvidenceItemState.POSITIVE);
        else SetState(EvidenceItemState.NEGATIVE);
    }
}
