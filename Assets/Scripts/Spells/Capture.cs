using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WrightWay.YellowVR.SpellEvents;

namespace WrightWay.YellowVR.Spells
{
    public class Capture : Spell
	{
		public override void CollideWithSpell(object sender, CollideWithSpellEventArgs args)
		{
			if (primaryElement == args.senderInstance.transferer.manaInterface.element)
			{
				Debug.Log("Capturing spell");
				// This just physically connects spells to the caster. See Link.cs for linking spells to the owner.
				
			}
		}

		public override string ToString() => GetDisplayName("Capture", primaryElement);
	}
}