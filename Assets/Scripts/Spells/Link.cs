using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WrightWay.YellowVR.SpellEvents;

namespace WrightWay.YellowVR.Spells
{
	public class Link : Spell
	{
		public Element linkingElement;

		public override void CollideWithSpell(object sender, CollideWithSpellEventArgs args)
		{
			// Only for mana-based spells
			if (primaryElement == args.senderInstance.transferer.manaInterface.element)
			{
				Debug.Log("Linking spell");
				if (linkingElement == args.senderInstance.transferer.soulInterface.element)
				{
					// This just links spells to caster. See Capture.cs for connecting to caster.
					args.collisionInstance.caster = args.senderInstance.caster;
				}
			}
		}

		public override string ToString() => GetDisplayName("Link", primaryElement, linkingElement);
	}
}