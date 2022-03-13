using System;
using System.Collections;
using UnityEngine;

namespace Utils
{
    /*
    * Class that has methods related to player
    */
    public static class DelayerHelper
    {
        public static IEnumerator Delay(float waitTime, Action action)
        {
            yield return new WaitForSeconds(waitTime);
            action();
        }
    }
}