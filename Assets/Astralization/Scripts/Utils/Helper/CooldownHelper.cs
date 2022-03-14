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
        [Tooltip("True if cooldown is finished")]
        private bool _isFinished;
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
        public void SetAccumulatedTime(float accumulatedTime)
        {
            _accumulatedTime = accumulatedTime;
        }
        public bool IsFinished()
        {
            return _isFinished;
        }
        #endregion

        #region Handler
        public void AddAccumulatedTime()
        {
            _accumulatedTime += Time.deltaTime;
            _isFinished = (_accumulatedTime >= _duration);
        }
        #endregion
    }
}
