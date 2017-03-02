using UnityEngine;

public class InputHandler : MonoBehaviour
{
    private SpellManager _spellManager;

    private const int _numSpells = 10;

    // Use this for initialization
    void Start()
    {
        _spellManager = SpellManager.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < _numSpells; ++i)
        {
            if (Input.GetKeyDown(i.ToString()))
            {
                _spellManager.CastSpell(i);
            }
        }
    }
}
