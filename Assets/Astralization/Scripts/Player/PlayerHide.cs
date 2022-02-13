using UnityEngine;

public class PlayerHide : MonoBehaviour
{
    #region Variables - Player position adjustment
    [Space]
    [Tooltip("Active player height offset")]
    public Vector3 ActivePlayerOffset;
    #endregion

    #region Enable - Disable
    private void OnEnable()
    {
        ClosetsController.HidePlayer += SetPlayerHidePosition;
    }

    private void OnDisable()
    {
        ClosetsController.HidePlayer -= SetPlayerHidePosition;
    }
    #endregion

    public void SetPlayerHidePosition(Interactable closets, bool state)
    {
        if (state)
        {
            this.gameObject.GetComponent<PlayerMovement>().enabled = false;
            this.transform.parent = closets.transform.parent;
            this.transform.position = closets.transform.parent.position + ActivePlayerOffset;
        }
        else
        {
            this.transform.parent = null;
            this.transform.position = this.transform.position - new Vector3(0,0,1);
            //this.gameObject.GetComponent<PlayerMovement>().enabled = true;
        }
    }
}
