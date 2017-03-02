using UnityEngine;
using System.Collections.Generic;

public abstract class Spell : MonoBehaviour {

    protected Queue<SpellActivity> _activities;
    private SpellActivity _currentActivity;

    void Start()
    {
        _activities = new Queue<SpellActivity>();
        OnStart();
        SpellManager.Instance.NextActivity();
    }

    // Use OnStart in child class to enqueue SpellActivities and do other important setup
    protected virtual void OnStart() { }

    void Update()
    {
        _currentActivity.Update();
    }

    private void FixedUpdate()
    {
        _currentActivity.FixedUpdate();
    }

    public bool NextActivity()
    {
        if (_activities.Count > 0)
        {
            _currentActivity = _activities.Dequeue();
            _currentActivity.Start();
            return true;
        }
        return false;
    }
}
