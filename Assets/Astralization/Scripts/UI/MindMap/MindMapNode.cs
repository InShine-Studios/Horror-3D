using System;
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
    [HideInInspector]
    public List<MindMapNode> Children = new List<MindMapNode>();
    public bool IsDiscovered = false;
    public MindMapNode Parent = null;
    #endregion

    #region SetGet
    #endregion

    #region MonoBehaviour
    #endregion
}
