using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MindMapTree : MonoBehaviour
{
    #region Variables
    [SerializeField]
    private MindMapNode root;
    private MindMapNode selectedNode = null;
    #endregion

    #region SetGet
    private MindMapNode GetNode(string nodeName)
    {
        Queue<MindMapNode> queue = new Queue<MindMapNode>();
        queue.Enqueue(root);

        while (queue.Count > 0)
        {
            MindMapNode currentNode = queue.Dequeue();
            if (currentNode.NodeName == nodeName) return currentNode;
            foreach (MindMapNode child in currentNode.Children)
            {
                queue.Enqueue(child);
            }
        }

        return null;
    }

    private void SelectNode(string nodeName)
    {
        selectedNode = GetNode(nodeName);
    }
    #endregion

    #region MonoBehaviour
    private void Start()
    {
        BuildTreeFromRoot();
    }
    #endregion

    #region Loader
    private void BuildTreeFromRoot()
    {
        Stack<MindMapNode> stack = new Stack<MindMapNode>();
        stack.Push(root);

        while(stack.Count > 0)
        {
            MindMapNode currentNode = stack.Pop();
            foreach (MindMapNode child in currentNode.Children)
            {
                child.Parent = currentNode;
                stack.Push(child);
            }
        }
    }
    #endregion

}
