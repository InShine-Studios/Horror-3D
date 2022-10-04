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
    public List<Transform> NodeTransforms = new List<Transform>();
}
