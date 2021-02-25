using System;
using UnityEngine;
using WrightWay.YellowVR.SpellEvents;

namespace WrightWay.YellowVR
{
	public class Retrigger : MonoBehaviour
	{
		public SpellEvent trigger;
		public Spell spell;

		public void LinkInstance(SpellInstance instance)
		{
			// Hopefully all these subscribbles get discarded when SpellInstance gets destroyed.
			switch (trigger)
			{
				case SpellEvent.CollideWithSpell:
					instance.CollidedWithSpell += spell.CollideWithSpell;
					break;
				default:
					throw new NullReferenceException("Retrigger trigger cannot be null.");
			}
		}
	}
}
