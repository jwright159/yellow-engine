using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WrightWay.YellowVR
{
	public class Sigil : MonoBehaviour
	{
		[SerializeField]
		private Spell spell;

		public SpellInstance CreateInstance(Caster caster)
		{
			SpellInstance instance = spell.CreateInstance(caster);
			instance.spell = spell;
			return instance;
		}

		public override string ToString() => $"{spell}\nCost: {spell.chargeRate} mps, min {spell.minimumManaCost} mana";
	}
}