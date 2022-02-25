using System.Collections;
using UnityEngine.InputSystem;

public class InitPlayerState : PlayerState
{
    #region MonoBehaviour
    protected override void Awake()
    {
        base.Awake();
        StartCoroutine(Init());
    }
    #endregion

    private IEnumerator Init()
    {
        // set up here for future use
        yield return null;
        owner.ChangeState<DefaultPlayerState>();
    }
}
