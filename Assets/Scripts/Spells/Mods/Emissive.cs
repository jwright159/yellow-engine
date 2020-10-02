using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WrightWay.YellowVR.Spells.SpellMods
{
	public class Emissive : SpellMod
	{
		public Spell casted;

		public override string ToString() => $"{casted} Emissive";
	}
}

