using UnityEngine;

public class SilhouetteBowlState : State, IEvidenceState
{
    #region Variables
    protected SilhouetteBowlManager owner;
    protected GameObject positiveModel;
    protected GameObject negativeModel;
    #endregion

    #region MonoBehaviour
    protected virtual void Awake()
    {
        owner = GetComponent<SilhouetteBowlManager>();
        positiveModel = transform.Find("Model/Headless").gameObject;
        negativeModel = transform.Find("Model/Heartless").gameObject;
    }
    #endregion
}
