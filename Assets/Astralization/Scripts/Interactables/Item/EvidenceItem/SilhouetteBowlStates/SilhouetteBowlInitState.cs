using System.Collections;

public class SilhouetteBowlInitState : SilhouetteBowlState
{
    #region MonoBehaviour
    protected override void Awake()
    {
        base.Awake();
        StartCoroutine(Init());
    }
    #endregion

    #region Initialization
    private IEnumerator Init()
    {
        // set up here for future use
        yield return null;
        owner.ChangeState<SilhouetteBowlInactiveState>();
    }
    #endregion
}
