using System.Collections;

public class InitClockState : ClockState
{
    #region MonoBehaviour
    protected override void Awake()
    {
        base.Awake();
        StartCoroutine(Init());
    }
    #endregion

    #region Handler
    private IEnumerator Init()
    {
        // set up here for future use
        yield return null;
        owner.ChangeState<ClockInactiveStates>();
    }
    #endregion
}
