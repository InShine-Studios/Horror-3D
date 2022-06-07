using UnityEngine;

public interface IItem
{
    void Discard();
    Sprite GetHudLogo();
    bool IsDiscardedWhenUsed();
    void Pick();
    void ShowItem(bool isShown);
    void Use();
}

/*
 * Item abstract class.
 * Parent class for all item objects.
 * Implement Use() function on each child class.
 */
[RequireComponent(typeof(MeshRenderer))]
public abstract class Item : MonoBehaviour, IItem
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

    [Space]
    [Header("Audio")]
    [Tooltip("Audio Manager")]
    private AudioPlayer _audioPlayerObj;

    [Header("Item Icons")]
    [Tooltip("True if there is an icon to be used")]
    [SerializeField]
    private bool _useIcon;

    [Tooltip("The icon mark for guidance")]
    [SerializeField]
    private GameObject _guideIcon;
    #endregion

    #region SetGet
    private void SetMeshRenderer(bool enabled)
    {
        _model.SetActive(enabled);
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

    public void SetCollider(bool state)
    {
        //Debug.Log("[INTERACTABLE] Setting collider " + this.name + " to " + state);
        GetComponent<Collider>().enabled = state;
    }

    public void ShowGuideIcon(bool state)
    {
        if (_useIcon)
        {
            //Debug.Log("[INTERACTABLE] Setting icon " + this.name + " to " + state);
            _guideIcon.SetActive(state);
        }
    }

    public void SetUseIcon(bool useIcon)
    {
        _useIcon = useIcon;
    }
    #endregion

    #region Monobehavior
    protected virtual void Awake()
    {
        _audioPlayerObj = GetComponentInChildren<AudioPlayer>();
        _model = transform.Find("Model").gameObject;
    }
    #endregion

    #region Use
    public abstract void Use();
    #endregion

    #region Handler

    protected void PlayAudio(string name)
    {
        _audioPlayerObj.Play(name);
    }
    #endregion

    #region Pick - Discard
    public virtual void Pick()
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
