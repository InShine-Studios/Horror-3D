using UnityEngine;

namespace Utils
{
    /*
    * Class that has methods related to cooldown
    */
    public class CooldownHelper
    {
        #region Variables
        [Tooltip("Time elapsed during cooldown")]
        private float _accumulatedTime = 0f;
        [Tooltip("Cooldown duration")]
        private float _duration;
        public bool IsFinished { get; private set; }
        #endregion

        #region Constructor
        public CooldownHelper(float duration)
        {
            _duration = duration;
        }
        #endregion

        #region SetGet
        public float GetAccumulatedTime()
        {
            return _accumulatedTime;
        }
        public void ResetCooldown()
        {
            _accumulatedTime = 0f;
            IsFinished = false;
        }
        #endregion

        #region Handler
        public void AddAccumulatedTime()
        {
            _accumulatedTime += Time.deltaTime;
            IsFinished = (_accumulatedTime >= _duration);
        }
        #endregion
    }
}
