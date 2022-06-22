using UnityEngine;

public interface IItem
{
    void Discard();
    Sprite GetHudLogo();
    void Pick();
    void ShowItem(bool isShown);
    bool Use();
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
    [Header("Item Logo")]
    [SerializeField]
    [Tooltip("The item logo for HUD")]
    protected Sprite HudLogo;

    // Determine item usage behavior
    public Utils.ItemHelper.UseBehaviourType UseBehaviourType { get; protected set; }
    // Determine world condition type so the item can be used
    public Utils.ItemHelper.WorldConditionType WorldConditionType { get; protected set; }
    
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
        transform.Find("Model").gameObject.SetActive(enabled);
    }

    public void ShowItem(bool isShown) 
    {
        //Debug.Log("[ITEM] Show " + this.name + " visibility to:" + isShown);
        this.gameObject.SetActive(isShown);
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
    }
    #endregion

    #region Use
    protected bool IsUsableOnCurrentWorld()
    {
        IWorldState currentWorldState = (IWorldState)WorldStateMachine.Instance.CurrentState;
        if (currentWorldState is IWorldRealState && WorldConditionType.HasFlag(Utils.ItemHelper.WorldConditionType.Real))
        {
            return true;
        }
        else if (currentWorldState is IWorldAstralState && WorldConditionType.HasFlag(Utils.ItemHelper.WorldConditionType.Astral))
        {
            return true;
        }
        else if (WorldStateMachine.Instance.IsKillPhase() && WorldConditionType.HasFlag(Utils.ItemHelper.WorldConditionType.KillPhase))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool Use()
    {
        if (!IsUsableOnCurrentWorld())
        {
            return false;
        }

        ActivateFunctionality();
        return true;
    }
    protected abstract void ActivateFunctionality();
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
