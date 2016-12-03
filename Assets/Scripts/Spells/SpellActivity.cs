using UnityEngine;

public abstract class SpellActivity
{
    private bool _isFinished = false;
    public bool IsFinished
    {
        get { return _isFinished; }
    }

    protected Transform _anchor;
    protected float _duration;
    protected float _remainingTime;

    public virtual void Start()
    {
        OnStart();
        if (_duration <= 0)
        {
            Finish();
        }
        else
        {
            _remainingTime = _duration;
        }
    }

    protected virtual void OnStart() { }

    public virtual void Update()
    {
        _remainingTime -= Time.deltaTime;
        if (_remainingTime <= 0)
        {
            Finish();
        }
        else
        {
            OnUpdate();
        }
    }

    protected virtual void OnUpdate() { }

    public virtual void FixedUpdate()
    {
        OnFixedUpdate();
    }

    protected virtual void OnFixedUpdate() { }

    public virtual void Finish()
    {
        OnFinish();
        _isFinished = true;
        SpellManager.Instance.NextActivity();
    }

    protected virtual void OnFinish() { }
}
