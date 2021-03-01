using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WrightWay.YellowVR.Spells.SpellMods
{
	/// <summary>
	/// Fires additional copies of a <see cref="spell"/>.
	/// </summary>
	public class Emissive : SpellMod
	{
		/// <summary>
		/// The <see cref="Spell"/> to be casted.
		/// </summary>
		public Spell spell;

		public override void LinkInstance(SpellInstance instance)
		{
			throw new System.NotImplementedException();
		}

		public override string ToString() => $"{spell} Emissive";
	}
}

