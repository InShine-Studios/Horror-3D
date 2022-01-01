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

    public static Collider[] FindOverlapsFromCollider(Transform source, CapsuleCollider CapsuleDetector)
    {
        Vector3 startCapsulePos = source.position + new Vector3(0f, CapsuleDetector.height / 2f) +
            CapsuleDetector.center;
        Vector3 finalCapsulePos = source.position - new Vector3(0f, CapsuleDetector.height / 2f) +
            CapsuleDetector.center;
        return Physics.OverlapCapsule(startCapsulePos, finalCapsulePos, CapsuleDetector.radius);
    }
}
}
