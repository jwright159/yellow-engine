using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WrightWay.YellowVR.SpellEvents;

namespace WrightWay.YellowVR.Spells
{
	/// <summary>
	/// A <see cref="Spell"/> that moves affected entities.
	/// <para>
	/// <example>Some examples:
	/// <list type="bullet">
	/// <item>
	///		<description>uh</description>
	/// </item>
	/// </list>
	/// </example>
	/// </para>
	/// </summary>
	public class Displacement : Spell
	{
		public Element displacedElement;

		public override void CollideWithSpell(object sender, CollideWithSpellEventArgs args)
		{
			
		}

		public override string ToString() => GetDisplayName("Displacement", primaryElement);
	}
}
