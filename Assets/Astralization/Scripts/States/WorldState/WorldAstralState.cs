using Astralization.States.WorldStates;
using Astralization.Utils.Helper;

namespace Astralization.States.TimeslotStates
{
    public interface IWorldAstralState { }

    public class WorldAstralState : WorldState, IWorldAstralState
    {
        #region MonoBehaviour
        protected override void Awake()
        {
            base.Awake();
            colorInUse = ColorHelper.ParseHex("#5F466A");
            volumeInUse = transform.Find("VOL_AstralWorld").gameObject;
        }
        #endregion

        #region StateHandler
        public override void Enter()
        {
            base.Enter();
            astralMeterLogic.SetAstralRate();
        }
        #endregion
    }
}