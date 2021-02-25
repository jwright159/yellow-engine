using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WrightWay.YellowVR.SpellEvents;

namespace WrightWay.YellowVR.Spells
{
	public class Displacement : Spell
	{
		public Element displacedElement;

		public override void CollideWithSpell(object sender, CollideWithSpellEventArgs args)
		{
			
		}

		public override string ToString() => GetDisplayName("Displacement", primaryElement);
	}
}
