using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WrightWay.YellowVR
{
	/// <summary>
	/// Final representation of a <see cref="Spell"/>. Starting point for creating a <see cref="SpellInstance"/>.
	/// </summary>
	public class Sigil : MonoBehaviour
	{
		/// <summary>
		/// The <see cref="Spell"/> used by new <see cref="SpellInstance"/>s.
		/// </summary>
		[SerializeField]
		private Spell spell;

		/// <summary>
		/// Create a new <see cref="SpellInstance"/> with the <see cref="spell"/>.
		/// </summary>
		/// <param name="caster">The <see cref="Caster"/> that is creating the <see cref="SpellInstance"/>.</param>
		/// <returns>A fully created <see cref="SpellInstance"/></returns>
		public SpellInstance CreateInstance(Caster caster)
		{
			return spell.CreateInstance(caster);
		}

		public override string ToString() => $"{spell}\nCost: {spell.chargeRate} mps, min {spell.minimumManaCost} mana";
	}
}