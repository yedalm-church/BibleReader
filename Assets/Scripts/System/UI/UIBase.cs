using UnityEngine;

public class UIBase : MonoBehaviour
{
    protected virtual void Awake()
    {
        InitializeBinding();
    }

    private void InitializeBinding()
    {
        UIInjector.Inject(this);
    }

    protected virtual void OnEnable()
    {
        OnActive();
    }

    protected virtual void Start()
    {
        UpdateContent();
    }

    protected virtual void OnDisable()
    {
        OnHide();
    }

    protected virtual void OnDestroy()
    {
        OnClose();
    }

    public virtual void UpdateContent() { }

    public virtual void OnClose() { }

    public virtual void OnActive() { }
    public virtual void OnHide() { }

    public virtual void BindEvent() { }
    public virtual void UnBindEvent() { }
}
