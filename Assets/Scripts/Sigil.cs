using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WrightWay.YellowVR.Spells;

public class Sigil : MonoBehaviour
{
    public Spell spell;

    public SpellInstance CreateInstance(Caster caster)
	{
		SpellInstance instance = spell.CreateInstance(caster);
		instance.spell = spell;
		return instance;
	}
}
