using System;
using UnityEngine;

public class SpellManager
{
    public enum SpellValues { Explosion = 1, GravityBall }
    private Transform _player;

    private bool _casting;

    private Spell _currentSpell;

    private static SpellManager _instance;
    public static SpellManager Instance
    {
        get
        {
            return _instance ?? (_instance = new SpellManager());
        }
    }

    private SpellManager()
    {
        _player = GameObject.FindWithTag("Player").transform;
        _casting = false;
    }

    public void CastSpell(int value)
    {
        if (!_casting)
        {
            GameObject spellObject = GetSpellObject((SpellValues)value);
            if (spellObject)
            {
                spellObject.transform.parent = _player;
                _currentSpell = spellObject.GetComponent<Spell>();
                _casting = true;
            }
        }
    }

    private GameObject GetSpellObject(SpellValues value)
    {
        GameObject spellResource = GetSpellResource(value);
        if (spellResource)
        {
            return (GameObject)UnityEngine.Object.Instantiate(spellResource, _player.position + Vector3.up, _player.rotation);
        }
        return null;
    }

    private GameObject GetSpellResource(SpellValues value)
    {
        string resourceAddress = GetSpellResourceAddress(value);
        if (!string.IsNullOrEmpty(resourceAddress))
        {
            return (GameObject)Resources.Load(resourceAddress);
        }
        return null;
    }

    private string GetSpellResourceAddress(SpellValues value)
    {
        switch (value)
        {
            case SpellValues.Explosion:
                return "SpellObjectExplosion";
            case SpellValues.GravityBall:
                return "GravityBall";
        }
        return "";
    }

    public void NextActivity()
    {
        if (!_currentSpell.NextActivity())
        {
            FinishSpellCast();
        }
    }

    private void FinishSpellCast()
    {
        UnityEngine.Object spellObject = _currentSpell.gameObject;
        _currentSpell = null;
        UnityEngine.Object.Destroy(spellObject);
        _casting = false;
    }
}
