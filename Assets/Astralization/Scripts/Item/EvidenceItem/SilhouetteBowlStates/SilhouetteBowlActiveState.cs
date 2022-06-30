using UnityEngine;

public class SilhouetteBowlActiveState : SilhouetteBowlState, IActiveState
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
        negativeModel.SetActive(false);
    }
    #endregion
}
