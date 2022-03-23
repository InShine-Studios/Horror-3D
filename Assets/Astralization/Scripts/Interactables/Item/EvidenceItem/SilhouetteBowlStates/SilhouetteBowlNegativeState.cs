using UnityEngine;

public class SilhouetteBowlNegativeState : SilhouetteBowlState
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
