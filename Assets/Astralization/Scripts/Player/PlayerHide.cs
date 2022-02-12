using UnityEngine;

public class PlayerHide : MonoBehaviour
{
    #region Variables - Player position adjustment
    [Space]
    [Tooltip("Active player height offset")]
    public Vector3 ActivePlayerOffset = new Vector3(-0.92f, -0.49f, 0.5f);
    #endregion

    #region Enable - Disable
    private void OnEnable()
    {
        ClosetsController.MovePlayer += SetPlayerPosition;
    }

    private void OnDisable()
    {
        ClosetsController.MovePlayer -= SetPlayerPosition;
    }
    #endregion

    public void SetPlayerPosition(Interactable closets, bool state)
    {
        if (state)
        {
            this.transform.parent = closets.transform.parent;
            Debug.Log(closets.transform.position);
            this.transform.position = closets.transform.position + ActivePlayerOffset;
            Debug.Log(this.transform.position);
        }
        else
        {
            this.transform.parent = null;
        }
    }
}
