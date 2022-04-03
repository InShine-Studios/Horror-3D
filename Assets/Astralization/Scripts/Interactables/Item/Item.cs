using UnityEngine;

public interface IItem: IInteractable
{
    void SetMeshRenderer(bool enabled);
    void Use();
}

/*
 * Item abstract class.
 * Parent class for all item objects.
 * Implement Use() function on each child class.
 */
[RequireComponent(typeof(MeshRenderer))]
public abstract class Item : Interactable, IItem
{
    #region Variables
    [Header("Item Logo")]
    [SerializeField]
    [Tooltip("The item logo for HUD")]
    private Sprite _HudLogo;

    [Header("Item Behavior")]
    [SerializeField]
    [Tooltip("Determine whether discard after used or not")]
    private bool _discardedWhenUsed = false;
    #endregion

    #region SetGet
    public void SetMeshRenderer(bool enabled)
    {
        transform.Find("Model").gameObject.SetActive(enabled);
    }

    public void ShowItem(bool isShown)
    {
        //Debug.Log("[ITEM] Show " + this.name + " visibility to:" + isShown);
        this.gameObject.SetActive(isShown);
    }

    public Sprite GetItemLogo()
    {
        return _HudLogo;
    }

    public bool IsDiscardedWhenUsed()
    {
        return _discardedWhenUsed;
    }
    #endregion

    #region Use
    public abstract void Use();

    public virtual void StopUse() { }
    #endregion

    #region Handler
    public override void OnInteraction()
    {
        Pick();
    }
    #endregion

    #region Pick - Discard
    private void Pick()
    {
        SetCollider(false);
        SetMeshRenderer(false);
        ShowGuideIcon(false);
        SetUseIcon(false);
    }
    public void Discard()
    {
        SetCollider(true);
        SetMeshRenderer(true);
        SetUseIcon(true);
    }
    #endregion
}
