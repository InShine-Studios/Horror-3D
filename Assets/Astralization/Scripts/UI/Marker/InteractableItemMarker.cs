using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractableItemMarker : MonoBehaviour
{
    private Canvas _canvas;
    private CircleGraphic _border;
    private Image _logo;
    private bool _enabled;
    private Vector3 _basePosition;
    private Animator _animator;

    public void SetEnabled(bool isEnabled)
    {
        _border.enabled = isEnabled;
        _logo.enabled = isEnabled;
        _enabled = isEnabled;
    }

    public void SetAnimation(int animationParam)
    {
        _animator.SetInteger("States", animationParam);
    }

    public void SetAnimator(RuntimeAnimatorController animatorController, int animationParam = -1)
    {
        _animator.runtimeAnimatorController = animatorController;
        if (animationParam != -1) SetAnimation(animationParam);
    }

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _canvas = GetComponent<Canvas>();
        _canvas.worldCamera = Camera.main;

        _border = GetComponentInChildren<CircleGraphic>();
        _logo = GetComponentInChildren<Image>();

        _basePosition = transform.position;

        transform.localScale = Utils.GeometryCalcu.ExcludeScalingFromParent(transform.localScale,transform.parent.localScale);

        SetEnabled(false);
    }

    private void LateUpdate()
    {
        if (_enabled)
        {
            Vector3 rotateDirection = (transform.position - Camera.main.transform.position).normalized;
            Vector3 newDirection = Vector3.RotateTowards(transform.forward, rotateDirection, 360f, 0f);

            transform.rotation = Quaternion.LookRotation(newDirection);
        }
    }
}
