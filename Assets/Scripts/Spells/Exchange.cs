using System;
using UnityEngine;
using WrightWay.YellowVR.SpellEvents;
using WrightWay.YellowVR.Targets;

namespace WrightWay.YellowVR.Spells
{
	/// <summary>
	/// A <see cref="Spell"/> that does a pure energy exchange.
	/// <para>
	/// <example>Some examples:
	/// <list type="bullet">
	/// <item>
	///		<description>A mana exchange deposits stored mana into an entity.</description>
	/// </item>
	/// <item>
	///		<description>A fire exchange heats up an entity.</description>
	/// </item>
	/// </list>
	/// </example>
	/// </para>
	/// </summary>
	public class Exchange : Spell
	{
		public Element consumedElement;

		public override void CollideWithSpell(SpellInstance senderInstance, SpellInstance collisionInstance)
		{
			// This isn't supposed to be here.
			EnergyTransferer transferer = collisionInstance.GetComponent<EnergyTransferer>();
			if (transferer)
			{
				transferer.Absorb(primaryElement, senderInstance.mana);
				//Debug.Log($"Vel: {instance.transferer.rigidbody.velocity.sqrMagnitude}, Mass: {instance.transferer.rigidbody.mass}, KE: {instance.transferer.GetKineticEnergy().sqrMagnitude}");
				//transferer.AbsorbKineticEnergy(instance.transferer.GetKineticEnergy()); // ya know what, rigidbody already does this
			}
		}

		public override string ToString() => GetDisplayName("Exchange", primaryElement, consumedElement);
	}
}
