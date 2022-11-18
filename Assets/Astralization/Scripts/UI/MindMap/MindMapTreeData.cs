using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Class for storing mind map tree data as Scriptable Object.
 * Parent reference is represented as index of MindMapNode array.
 */
public class MindMapTreeData : ScriptableObject
{
    public List<int> ParentReferenceIdx = new List<int>();  // -1 means root (no parent)
    public List<string> NodeNames = new List<string>();
    public List<string> NodeDescriptions = new List<string>();
    public List<MindMapNodeType> NodeTypes = new List<MindMapNodeType>();
    public List<RuntimeAnimatorController> NodeAnimationControllers = new List<RuntimeAnimatorController>();
    public List<Vector3> NodePositions = new List<Vector3>();
    public List<Quaternion> NodeRotations = new List<Quaternion>();
    public List<Vector3> NodeScales = new List<Vector3>();
    public List<Vector3> NodeCameraFollowPosition = new List<Vector3>();
    public List<Vector3> NodeCameraLookAtPosition= new List<Vector3>();
}
