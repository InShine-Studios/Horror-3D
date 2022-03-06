using UnityEngine;
using System.Collections;

namespace Utils
{
    /*
    * Class that has methods related to player
    */
    public class CooldownHelper
    {
        #region Variable
        private float _accumulatedTime = 0f;
        private float _holdTime;
        public bool isFinished;
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
        public void SetAccumulatedTime(float accumulatedTime)
        {
            _accumulatedTime = accumulatedTime;
        }
        #endregion

        public void AddAccumulatedTime()
        {
            _accumulatedTime += Time.deltaTime;
        }

        public bool CheckTimer()
        {
            return (_accumulatedTime >= _holdTime);
        }
    }
}
