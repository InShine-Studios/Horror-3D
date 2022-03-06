using UnityEngine;

namespace Utils
{
    /*
    * Class that has methods related to cooldown
    */
    public class CooldownHelper
    {
        #region Variable
        [Tooltip("Float value for accumulated time")]
        private float _accumulatedTime = 0f;
        [Tooltip("Float value for the cooldown")]
        private float _holdTime;
        [Tooltip("Bool value for cooldown conditions")]
        private bool _isFinished;
        #endregion

        public CooldownHelper(float holdTime)
        {
            _holdTime = holdTime;
        }

        #region SetGet
        public float GetAccumulatedTime()
        {
            return _accumulatedTime;
        }
        public bool GetFinishedConditions()
        {
            return _isFinished;
        }
        public void SetAccumulatedTime(float accumulatedTime)
        {
            _accumulatedTime = accumulatedTime;
        }
        #endregion

        #region CooldownFunctions
        public void AddAccumulatedTime()
        {
            _accumulatedTime += Time.deltaTime;
            _isFinished = (_accumulatedTime >= _holdTime);
        }
        #endregion
    }
}
