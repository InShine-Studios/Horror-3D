using UnityEngine;
using UnityEngine.InputSystem;

namespace Utils
{
    /*
    * Class that has methods for geometry calculation
    */
    public static class GeometryCalcu
    {
        public static float GetDistance3D(Transform feetPosition, Transform objPosition)
        {
            return Vector3.Distance(feetPosition.transform.position, objPosition.transform.position);
        }
    }
}
