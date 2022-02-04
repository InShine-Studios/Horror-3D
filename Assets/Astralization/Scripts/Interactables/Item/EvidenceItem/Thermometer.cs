using UnityEngine;

public class Thermometer : EvidenceItem
{

    public override void DetermineEvidence()
    {
        // Debug.Log("[ITEM] Determine Evidence " + this.name);
        // NOTE this dummy behavior at the moment, wait for Ghost Implementation
        if (state == EvidenceItemState.NEGATIVE) SetState(EvidenceItemState.POSITIVE);
        else SetState(EvidenceItemState.NEGATIVE);
    }
}
