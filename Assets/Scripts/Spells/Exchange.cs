using System;
using UnityEngine;
using WrightWay.YellowVR.SpellEvents;
using WrightWay.YellowVR.Targets;

namespace WrightWay.YellowVR.Spells
{
	public class Exchange : Spell
	{
		public Element consumedElement;

		public override void CollideWithSpell(object sender, CollideWithSpellEventArgs args)
		{
			// This isn't supposed to be here.
			EnergyTransferer transferer = args.collisionInstance.GetComponent<EnergyTransferer>();
			if (transferer)
			{
				transferer.Absorb(primaryElement, args.senderInstance.mana);
				//Debug.Log($"Vel: {instance.transferer.rigidbody.velocity.sqrMagnitude}, Mass: {instance.transferer.rigidbody.mass}, KE: {instance.transferer.GetKineticEnergy().sqrMagnitude}");
				//transferer.AbsorbKineticEnergy(instance.transferer.GetKineticEnergy()); // ya know what, rigidbody already does this
			}
		}

		public override string ToString() => GetDisplayName("Exchange", primaryElement, consumedElement);
	}
}
