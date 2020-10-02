using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WrightWay.YellowVR.Spells
{
	public class Link : Spell
	{
		public Element affectedElement;
		public Element linkingElement;

		public override void Collide(SpellInstance instance, Collision collision)
		{
			SpellInstance collideInstance = collision.gameObject.GetComponent<SpellInstance>();
			
			if (affectedElement == instance.transferer.manaInterface.element && collideInstance)
			{
				Debug.Log("Linking spell");
				if (linkingElement == instance.transferer.soulInterface.element)
				{
					collideInstance.owner = instance.owner;
				}
			}
		}

		public override SpellInstance CreateInstance(Caster caster)
		{
			return target.CreateInstance(linkingElement, caster);
		}

		public override string ToString() => GetDisplayName("Link", affectedElement, linkingElement);
	}
}