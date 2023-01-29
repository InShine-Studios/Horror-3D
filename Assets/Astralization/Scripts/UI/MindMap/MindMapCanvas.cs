using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MindMapCanvas : MonoBehaviour
{
    #region Variables
    private ChapterNavigation _chapterNavigation;
    private NodeNavigation _nodeNavigation;
    #endregion

    #region MonoBehaviour
    private void Awake()
    {
        _chapterNavigation = GetComponentInChildren<ChapterNavigation>();
        _nodeNavigation = GetComponentInChildren<NodeNavigation>();
    }

    private void OnEnable()
    {
        SetUp();
    }

    private void OnDisable()
    {
        TearDown();
    }
    #endregion

    #region SetUpTeardown
    private void SetUp()
    {
        _nodeNavigation.SetUp();
    }

    private void TearDown()
    {
        _nodeNavigation.TearDown();
    }
    #endregion
}
