using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WrightWay.YellowVR.Spells
{
	public class Displacement : Spell
	{
		public Element displacedElement;

		public override void Collide(SpellInstance instance, Collision collision)
		{
			
		}

		public override SpellInstance CreateInstance(Caster caster)
		{
			return target.CreateInstance(displacedElement, caster);
		}

		public override string ToString() => GetDisplayName("Displacement", displacedElement);
	}
}
