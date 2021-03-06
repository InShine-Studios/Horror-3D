using UnityEngine;

public class SilhouetteBowlNegativeState : SilhouetteBowlState, INegativeState
{
    #region MonoBehaviour
    protected override void Awake()
    {
        base.Awake();
    }
    #endregion

    #region Handler
    public override void Enter()
    {
        base.Enter();
        positiveModel.SetActive(false);
        negativeModel.SetActive(true);
    }
    #endregion
}
