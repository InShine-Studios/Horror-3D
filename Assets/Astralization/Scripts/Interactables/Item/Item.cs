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
    [Tooltip("The Sprite for the logo")]
    private Sprite _itemLogo;

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
        //Debug.Log("[ITEM] Show item " + this.name + " visibility to:" + isShown);
        this.gameObject.SetActive(isShown);
    }

    public Sprite GetItemLogo()
    {
        return _itemLogo;
    }

    public bool IsDiscardedWhenUsed()
    {
        return _discardedWhenUsed;
    }
    #endregion

    public abstract void Use();

    public virtual void StopUse() { }

    public override void OnInteraction()
    {
        Pick();
    }

    private void Pick()
    {
        SetCollider(false);
        SetMeshRenderer(false);
        ShowGuideIcon(false);
    }
    public void Discard()
    {
        SetCollider(true);
        SetMeshRenderer(true);
    }
}
