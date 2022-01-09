using UnityEngine;

/*
 * Item abstract class.
 * Parent class for all item objects.
 * Implement Use() function on each child class.
 */
[RequireComponent(typeof(MeshRenderer))]
public abstract class Item : Interactable
{
    public abstract void Use();

    public override void OnInteraction()
    {
        Pick();
    }
    private void Pick()
    {
        SetCollider(false);
        SetMeshRenderer(false);
    }
    public void SetMeshRenderer(bool enabled)
    {
        GetComponent<MeshRenderer>().enabled = enabled;
    }

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
