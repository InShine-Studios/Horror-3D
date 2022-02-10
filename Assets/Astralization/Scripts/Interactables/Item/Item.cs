using UnityEngine;

public interface IItem: IInteractable
{
    void HideItem();
    void SetMeshRenderer(bool enabled);
    void ShowItem();
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
    [SerializeField]
    [Tooltip("The Sprite for the logo")]
    private Sprite _itemLogo;

    public abstract void Use();

    public override void OnInteraction()
    {
        Pick();
    }
    private void Pick()
    {
        SetCollider(false);
        SetMeshRenderer(false);
        SetInteractableIcon(false);
    }
    public void Discard()
    {
        SetCollider(true);
        SetMeshRenderer(true);
    }

    #region Setter Getter
    public void SetMeshRenderer(bool enabled)
    {
        transform.Find("Model").gameObject.SetActive(enabled);
    }

    public Sprite GetItemLogo()
    {
        return _itemLogo;
    }
    #endregion

    #region Show - Hide Item
    public void HideItem()
    {
        //Debug.Log("[ITEM] Hiding item " + this.name);
        this.gameObject.SetActive(false);
    }

    public void ShowItem()
    {
        //Debug.Log("[ITEM] Showing item " + this.name);
        this.gameObject.SetActive(true);
    }
    #endregion
}
