using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WrightWay.YellowVR.Targets
{
	/// <summary>
	/// A <see cref="Target"/> that has effects only while charging.
	/// </summary>
	public class Touch : Target
	{
		public override float minimumManaCost => 10;

		public override void Fire(SpellInstance instance)
		{
			instance.Kill();
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
