using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WrightWay.YellowVR.SpellEvents;

namespace WrightWay.YellowVR.Spells
{
	/// <summary>
	/// A <see cref="Spell"/> that attaches affected entities to the <see cref="Caster"/>(???).
	/// <para>
	/// <example>Some examples:
	/// <list type="bullet">
	/// <item>
	///		<description>A mana capture attaches a spell to this <see cref="SpellInstance.caster"/>. At least for <see cref="Touch"/>. Not sure about anything else.</description>
	/// </item>
	/// </list>
	/// </example>
	/// </para>
	/// </summary>
	public class Capture : Spell
	{
		public override void CollideWithSpell(object sender, CollideWithSpellEventArgs args)
		{
			if (primaryElement == args.senderInstance.transferer.manaInterface.element)
			{
				Debug.Log("Capturing spell");
				// This just physically connects spells to the caster. See Link.cs for linking spells to the caster.
				
			}
		}

		public override string ToString() => GetDisplayName("Capture", primaryElement);
	}
}