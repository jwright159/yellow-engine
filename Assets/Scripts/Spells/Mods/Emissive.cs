using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WrightWay.YellowVR.Spells.SpellMods
{
	public class Emissive : SpellMod
	{
		public Spell casted;

		public override void LinkInstance(SpellInstance instance)
		{
			throw new System.NotImplementedException();
		}

		public override string ToString() => $"{casted} Emissive";
	}
}

