using UnityEngine;
using UnityEngine.UI;

public interface IMindMapModal
{
    GameObject GetModal();
    string GetTitle();
    string GetDescription();
}

public class MindMapModal : MonoBehaviour, IMindMapModal
{
    #region Variables
    private GameObject _modal;
    private Text _title;
    private Text _desc;
    #endregion

    #region SetGet
    public GameObject GetModal()
    {
        return _modal;
    }

    public string GetTitle()
    {
        return _title.text;
    }

    public string GetDescription()
    {
        return _desc.text;
    }
    #endregion

    #region MonoBehaviour
    private void Awake()
    {
        _modal = transform.Find("Modal").gameObject;
        _title = _modal.transform.Find("Title").GetComponent<Text>();
        _desc = _modal.transform.Find("Description").GetComponent<Text>();
    }
    #endregion

    #region ModalHandler
    public void ActivateModal(bool isActive)
    {
        _modal.SetActive(isActive);
    }
    public void SetNodeInfo(MindMapNode node)
    {
        _title.text = node.NodeName;
        _desc.text = node.NodeDescription;
    }
    #endregion
}
