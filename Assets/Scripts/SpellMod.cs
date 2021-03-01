using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WrightWay.YellowVR
{
	/// <summary>
	/// Modifier for spells, such as a chain lightning effect or a loop cast. Fires at events or something.
	/// </summary>
	public abstract class SpellMod : MonoBehaviour
	{
		/// <summary>
		/// Link this <see cref="SpellMod"/> to the <paramref name="instance"/>.
		/// </summary>
		/// <param name="instance">The <see cref="SpellInstance"/> to link to.</param>
		public abstract void LinkInstance(SpellInstance instance);
	}
}
