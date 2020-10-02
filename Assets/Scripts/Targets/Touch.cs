using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WrightWay.YellowVR.Targets
{
	public class Touch : Target
	{
		public override float minimumManaCost => 0;

		public override SpellInstance CreateInstance(Element element, Caster caster)
		{
			throw new System.NotImplementedException();
		}

		public override void Fire(SpellInstance instance)
		{
			throw new System.NotImplementedException();
		}

		public override void SetSpellColor(SpellInstance instance, Color color)
		{
			throw new System.NotImplementedException();
		}

		public override void UpdateInstance(SpellInstance instance)
		{
			throw new System.NotImplementedException();
		}
	}
}
