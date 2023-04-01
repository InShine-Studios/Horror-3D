using UnityEngine;
using UnityEngine.UI;

namespace Astralization.UI
{

    /*
     * Class to control Hiding Overlay
     */
    public class HidingOverlay : MonoBehaviour
    {
        #region Constants
        private const string _animParam = "isHiding";
        #endregion

        #region Variables
        [SerializeField]
        [Header("Animation")]
        private Animator _animator;

        private Image _overlay;
        #endregion

        #region MonoBehaviour
        private void Awake()
        {
            _overlay = transform.Find("Overlay").GetComponent<Image>();
        }
        #endregion

        #region Handler
        public void StartAnim(bool isHiding)
        {
            ChangeColor();
            _animator.SetBool(_animParam, isHiding);
        }

        private void ChangeColor()
        {
            _overlay.color = RenderSettings.fogColor;
        }
        #endregion
    }
}