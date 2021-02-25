using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WrightWay.YellowVR.Targets
{
	public class Touch : Target
	{
		public override float minimumManaCost => 10;

		public override void Fire(SpellInstance instance)
		{
			Destroy(instance.gameObject);
		}

		public override void UpdateInstance(SpellInstance instance)
		{
			// This probably won't ever run, right?
			// Can't wait to be wrong later
			throw new System.NotImplementedException();
		}

		public override string ToString() => "Touching";
	}
}
