using UnityEngine;
using UnityEngine.UI;

public class MindMapModal : MonoBehaviour
{
    #region Variables
    private GameObject _modal;
    private Text _title;
    private Text _desc;
    #endregion

    #region MonoBehaviour
    private void Awake()
    {
        _modal = transform.Find("Modal").gameObject;
        _title = _modal.transform.Find("Title").GetComponent<Text>();
        _desc = _modal.transform.Find("Description").GetComponent<Text>();
    }
    private void OnEnable()
    {
        MindMapTree.ActivatedModal += ActivatedModal;
        MindMapTree.SetNodeInfo += SetNodeInfo;
    }

    private void OnDisable()
    {
        MindMapTree.ActivatedModal -= ActivatedModal;
        MindMapTree.SetNodeInfo -= SetNodeInfo;
    }
    #endregion

    #region ModalHandler
    private void ActivatedModal(bool is_active)
    {
        _modal.SetActive(is_active);
    }
    private void SetNodeInfo(MindMapNode node)
    {
        _title.text = node.NodeName;
        _desc.text = node.NodeDescription;
    }
    #endregion
}
