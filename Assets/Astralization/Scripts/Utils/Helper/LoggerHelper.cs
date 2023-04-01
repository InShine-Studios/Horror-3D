using UnityEngine;

namespace Astralization.Utils.Helper
{
    public static class LoggerHelper
    {
        #region Handler
        public static void PrintStateDefaultLog(string statefulObject, string methodName)
        {
            Debug.Log(string.Format("[{0} STATE] NotImplementedWarning: {1} is not implemented in this state", new string[] { statefulObject.ToUpper(), methodName }));
        }
        #endregion
    }
}
