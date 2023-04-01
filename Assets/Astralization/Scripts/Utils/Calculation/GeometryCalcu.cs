using UnityEngine;

namespace Astralization.Utils.Calculation
{
    /*
    * Class that has methods for geometry calculation
    */
    public static class GeometryCalcu
    {
        public static float GetDistance3D(Vector3 source, Vector3 target)
        {
            return Vector3.Distance(source, target);
        }

        public static Quaternion GetAngleDelta(Quaternion source, Quaternion target)
        {
            return target * Quaternion.Inverse(source);
        }

        public static Collider[] FindOverlapsFromCollider(Transform source, CapsuleCollider CapsuleDetector)
        {
            Vector3 startCapsulePos = source.position + new Vector3(0f, CapsuleDetector.height / 2f) +
                CapsuleDetector.center;
            Vector3 finalCapsulePos = source.position - new Vector3(0f, CapsuleDetector.height / 2f) +
                CapsuleDetector.center;
            return Physics.OverlapCapsule(startCapsulePos, finalCapsulePos, CapsuleDetector.radius);
        }

        public static Collider[] FindOverlapsFromCollider(Transform source, BoxCollider BoxDetector)
        {
            Vector3 detectorCenter = source.position + BoxDetector.center;
            return Physics.OverlapBox(detectorCenter, BoxDetector.size / 2);
        }

        public static Vector3 ExcludeScalingFromParent(Vector3 targetLocalScale, Vector3 parentLocalScale)
        {
            Vector3 scaleTmp = targetLocalScale;
            scaleTmp.x /= parentLocalScale.x;
            scaleTmp.y /= parentLocalScale.y;
            scaleTmp.z /= parentLocalScale.z;
            return scaleTmp;
        }
    }
}
