using UnityEngine;
using System.Collections.Generic;

public abstract class Spell : MonoBehaviour {

    protected Queue<SpellActivity> activities;
    private SpellActivity currentActivity;

    void Start()
    {
        activities = new Queue<SpellActivity>();
        OnStart();
        SpellManager.Instance.NextActivity();
    }

    // Use OnStart in child class to enqueue SpellActivities and do other important setup
    protected virtual void OnStart() { }

    void Update()
    {
        currentActivity.Update();
    }

    private void FixedUpdate()
    {
        currentActivity.FixedUpdate();
    }

    public bool NextActivity()
    {
        if (activities.Count > 0)
        {
            currentActivity = activities.Dequeue();
            currentActivity.Start();
            return true;
        }
        return false;
    }
}
