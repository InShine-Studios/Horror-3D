using UnityEngine;

public interface IItem
{
    void Discard();
    Sprite GetHudLogo();
    bool IsDiscardedWhenUsed();
    void OnInteraction();
    void ShowItem(bool isShown);
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
    [Header("Item Component")]
    [SerializeField]
    [Tooltip("The item logo for HUD")]
    protected Sprite HudLogo;
    private GameObject _model;

    [Header("Item Behavior")]
    [SerializeField]
    [Tooltip("Determine whether discard after used or not")]
    private bool _discardedWhenUsed = false;
    #endregion

    #region SetGet
    private void SetMeshRenderer(bool enabled)
    {
        _model.gameObject.SetActive(enabled);
    }

    public void ShowItem(bool isShown)
    {
        //Debug.Log("[ITEM] Show " + this.name + " visibility to:" + isShown);
        this.gameObject.SetActive(isShown);
    }

    public bool IsDiscardedWhenUsed()
    {
        return _discardedWhenUsed;
    }

    public Sprite GetHudLogo()
    {
        return HudLogo;
    }
    #endregion

    #region MonoBehaviour
    private void Awake()
    {
        _model = transform.Find("Model").gameObject;
    }
    #endregion

    #region Use
    public abstract void Use();
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
