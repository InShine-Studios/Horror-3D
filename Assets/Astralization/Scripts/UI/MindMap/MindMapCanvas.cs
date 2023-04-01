using UnityEngine;

namespace Astralization.UI.MindMap
{
    public class MindMapCanvas : MonoBehaviour
    {
        #region Variables
        private ChapterNavigation _chapterNavigation;
        private NodeNavigation _nodeNavigation;
        private Canvas _canvas;
        #endregion

        #region SetGet
        public void EnableCanvas(bool isEnabled)
        {
            _canvas.enabled = isEnabled;
        }
        #endregion

        #region MonoBehaviour
        private void Awake()
        {
            _chapterNavigation = GetComponentInChildren<ChapterNavigation>();
            _nodeNavigation = GetComponentInChildren<NodeNavigation>();
            _canvas = GetComponent<Canvas>();
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
}