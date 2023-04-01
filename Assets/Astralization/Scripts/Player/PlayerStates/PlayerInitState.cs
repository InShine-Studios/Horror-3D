using Astralization.Utils.Helper;
using System.Collections;
using UnityEngine.InputSystem;

namespace Astralization.Player.PlayerStates
{
    public class PlayerInitState : PlayerState
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
            owner.SetPlayerActionMap(PlayerHelper.States.Default);
        }
        #endregion
    }
}