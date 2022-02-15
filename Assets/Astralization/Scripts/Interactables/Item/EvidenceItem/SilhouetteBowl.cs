using UnityEngine;
using System.Collections.Generic;

public class SilhouetteBowl : EvidenceItem
{

    #region Model
    [Header("Model reference")]
    [SerializeField]
    [Tooltip("Positive model game object reference")]
    private GameObject positiveModel;
    
    [SerializeField]
    [Tooltip("Positive model game object reference")]
    private GameObject negativeModel;
    #endregion
    
    public override void HandleChange()
    {
        if (this.state == EvidenceItemState.POSITIVE)
        {
            positiveModel.SetActive(true);
            negativeModel.SetActive(false);
        } else if (this.state == EvidenceItemState.NEGATIVE)
        {
            positiveModel.SetActive(false);
            negativeModel.SetActive(true);
        } else
        {
            positiveModel.SetActive(false);
            negativeModel.SetActive(false);
        }
    }

    public override void DetermineEvidence()
    {
        // NOTE this dummy behavior at the moment, wait for Ghost Implementation
        if (state == EvidenceItemState.NEGATIVE) SetState(EvidenceItemState.POSITIVE);
        else SetState(EvidenceItemState.NEGATIVE);
    }
}
