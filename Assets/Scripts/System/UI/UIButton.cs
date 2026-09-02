using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIButton : Button
{
    private Action _onClick;

    private Vector3 _originScale;

    [SerializeField]
    private float _hoverScale = 1.05f;

    protected override void Awake()
    {
        base.Awake();

        _originScale = transform.localScale;

        onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        _onClick?.Invoke();
    }

    public void Bind(Action InAction)
    {
        _onClick -= InAction;
        _onClick += InAction;
    }

    public void UnBind(Action InAction)
    {
        _onClick -= InAction;
    }

    public void Clear()
    {
        _onClick = null;
    }

    public override void OnPointerEnter(PointerEventData InEventData)
    {
        base.OnPointerEnter(InEventData);

        transform.localScale = _originScale * _hoverScale;
    }

    public override void OnPointerExit(PointerEventData InEventData)
    {
        base.OnPointerExit(InEventData);

        transform.localScale = _originScale;
    }

    protected override void OnDestroy()
    {
        onClick.RemoveListener(OnClick);
        _onClick = null;

        base.OnDestroy();
    }
}