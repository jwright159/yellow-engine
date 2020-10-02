using System;
using UnityEngine;
using WrightWay.YellowVR.Targets;

namespace WrightWay.YellowVR.Spells
{
	public class Exchange : Spell
	{
		public Element producedElement;
		public Element consumedElement;

		public override SpellInstance CreateInstance(Caster caster)
		{
			return target.CreateInstance(producedElement, caster);
		}

		public override void Collide(SpellInstance instance, Collision collision)
		{
			EnergyTransferer transferer = collision.gameObject.GetComponent<EnergyTransferer>();
			if (transferer)
			{
				transferer.Absorb(producedElement, instance.mana);
				//Debug.Log($"Vel: {instance.transferer.rigidbody.velocity.sqrMagnitude}, Mass: {instance.transferer.rigidbody.mass}, KE: {instance.transferer.GetKineticEnergy().sqrMagnitude}");
				//transferer.AbsorbKineticEnergy(instance.transferer.GetKineticEnergy()); // ya know what, rigidbody already does this
			}
		}

		public override string ToString() => GetDisplayName("Exchange", producedElement, consumedElement);
	}
}
