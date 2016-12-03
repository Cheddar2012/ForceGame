using System;
using UnityEngine;

public class SpellManager
{
    public enum SpellValues { Explosion = 1, GravityBall }
    private Transform player;

    private bool casting;

    private Spell currentSpell;

    private static SpellManager instance;
    public static SpellManager Instance
    {
        get
        {
            return instance ?? (instance = new SpellManager());
        }
    }

    private SpellManager()
    {
        player = GameObject.FindWithTag("Player").transform;
        casting = false;
    }

    public void CastSpell(int value)
    {
        if (!casting)
        {
            GameObject spellObject = getSpellObject((SpellValues)value);
            if (spellObject)
            {
                spellObject.transform.parent = player;
                currentSpell = spellObject.GetComponent<Spell>();
                casting = true;
            }
        }
    }

    private GameObject getSpellObject(SpellValues value)
    {
        GameObject spellResource = getSpellResource(value);
        if (spellResource)
        {
            return (GameObject)UnityEngine.Object.Instantiate(spellResource, player.position + Vector3.up, player.rotation);
        }
        return null;
    }

    private GameObject getSpellResource(SpellValues value)
    {
        string resourceAddress = getSpellResourceAddress(value);
        if (!string.IsNullOrEmpty(resourceAddress))
        {
            return (GameObject)Resources.Load(resourceAddress);
        }
        return null;
    }

    private string getSpellResourceAddress(SpellValues value)
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
        if (!currentSpell.NextActivity())
        {
            FinishSpellCast();
        }
    }

    private void FinishSpellCast()
    {
        UnityEngine.Object spellObject = currentSpell.gameObject;
        currentSpell = null;
        UnityEngine.Object.Destroy(spellObject);
        casting = false;
    }
}
