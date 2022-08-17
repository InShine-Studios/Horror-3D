using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MindMapNodeType
{
    CHAPTER,
    CORE,
    CLUE
}

public class MindMapNode : MonoBehaviour
{
    #region Variables
    public string NodeName;
    public string NodeDescription;
    public MindMapNodeType NodeType;
    public RuntimeAnimatorController AnimController;
    public List<MindMapNode> Children = new List<MindMapNode>();
    public bool IsDiscovered = false;
    internal MindMapNode Parent = null;

    //private readonly Dictionary<string, MindMapNode> childrenDict = new Dictionary<string,MindMapNode>();  // Do we need optimization for this?
    #endregion

    #region SetGet
    #endregion

    #region MonoBehaviour
    #endregion
}
