using UnityEngine;

public interface IEvidenceItem : IItem
{
    void DetermineEvidence();
    void OnGhostInteraction();
}

/*
 * EvidenceItem abstract class.
 * Parent class for all evidence item objects.
 * Implement DetermineEvidence() and HandleChange() function on each child class.
 */
public abstract class EvidenceItem : Item, IEvidenceItem
{
    #region Handler
    public abstract void OnGhostInteraction();

    #endregion

    #region Evidence related
    public abstract void DetermineEvidence();
    #endregion
}
