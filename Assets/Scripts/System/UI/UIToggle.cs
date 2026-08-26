using System;
using UnityEngine.UI;

public class UIToggle : Toggle
{
    private Action _onSelected;

    protected override void Awake()
    {
        base.Awake();
        onValueChanged.AddListener(OnValueChanged);
    }

    private void OnValueChanged(bool InOn)
    {
        if (InOn)
            _onSelected?.Invoke();
    }

    public void Bind(Action InAction)
    {
        _onSelected += InAction;
    }

    public void UnBind(Action InAction)
    {
        _onSelected -= InAction;
    }

    public void Clear()
    {
        _onSelected = null;
    }

    protected override void OnDestroy()
    {
        onValueChanged.RemoveListener(OnValueChanged);
        _onSelected = null;

        base.OnDestroy();
    }
}