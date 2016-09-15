using UnityEngine;

public class InputHandler : MonoBehaviour
{
    private SpellManager spellManager;

    private const int numSpells = 10;

    // Use this for initialization
    void Start()
    {
        spellManager = SpellManager.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < numSpells; ++i)
        {
            if (Input.GetKeyDown(i.ToString()))
            {
                spellManager.CastSpell(i);
            }
        }
    }
}
