using UnityEngine;
using System.Collections.Generic;

/*
 * SilhouetteBowl class.
 * Override DetermineEvidence and HandleChange from base EvidenceItem class according to murder silhouette evidence mechanics.
 */
public class SilhouetteBowl : EvidenceItem
{

    #region Model
    [Header("Model reference")]
    [SerializeField]
    [Tooltip("Positive model game object reference")]
    private GameObject _positiveModel;
    
    [Space]
    [SerializeField]
    [Tooltip("Positive model game object reference")]
    private GameObject _negativeModel;
    #endregion
    
    public override void HandleChange()
    {
        if (this.state == EvidenceItemState.POSITIVE)
        {
            _positiveModel.SetActive(true);
            _negativeModel.SetActive(false);
        } else if (this.state == EvidenceItemState.NEGATIVE)
        {
            _positiveModel.SetActive(false);
            _negativeModel.SetActive(true);
        } else
        {
            _positiveModel.SetActive(false);
            _negativeModel.SetActive(false);
        }
    }

    public override void DetermineEvidence()
    {
        // TODO this dummy behavior at the moment, wait for Ghost Implementation
        if (state == EvidenceItemState.NEGATIVE) SetState(EvidenceItemState.POSITIVE);
        else SetState(EvidenceItemState.NEGATIVE);
    }
}
