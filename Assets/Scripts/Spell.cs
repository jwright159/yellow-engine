using System;
using UnityEngine;
using WrightWay.YellowVR.SpellEvents;

namespace WrightWay.YellowVR
{
	/// <summary>
	/// Something that does a thing on a <see cref="SpellInstance"/>.
	/// </summary>
	public abstract class Spell : MonoBehaviour
	{
		/// <summary>
		/// The rate at which mana flows from a <see cref="Caster"/> into a <see cref="SpellInstance"/>.
		/// </summary>
		public float chargeRate;

		/// <summary>
		/// The rate at which mana is released from a <see cref="SpellInstance"/>, for controlling explosivity and such. Not implemented.
		/// </summary>
		public float releaseRate;

		/// <summary>
		/// The list of <see cref="SpellMod"/>s that can capture mod events.
		/// </summary>
		public SpellMod[] mods;

		/// <summary>
		/// The list of <see cref="Retrigger"/>s that can capture retrigger events.
		/// </summary>
		public Retrigger[] retriggers;

		/// <summary>
		/// The physical form a <see cref="SpellInstance"/> takes.
		/// </summary>
		[SerializeField]
		protected Target target;

		/// <summary>
		/// Mostly determines the appearance of the <see cref="target"/>.
		/// </summary>
		public Element primaryElement;

		/// <summary>
		/// The bare minimum mana that must be filled by the <see cref="SpellInstance.caster"/> in order to successfully <see cref="Fire(SpellInstance)"/>.
		/// </summary>
		public float minimumManaCost => target.minimumManaCost;

		/// <summary>
		/// Create a <see cref="SpellInstance"/> from the <see cref="target"/>, and link the <see cref="mods"/> and <see cref="retriggers"/> to it.
		/// <para>
		/// Only called from <see cref="Sigil.CreateInstance(Caster)"/>.
		/// </para>
		/// </summary>
		/// <param name="caster">The <see cref="Caster"/> that is creating the <see cref="SpellInstance"/>.</param>
		/// <returns>A partially-created <see cref="SpellInstance"/>.</returns>
		public SpellInstance CreateInstance(Caster caster)
		{
			SpellInstance instance = target.CreateInstance(primaryElement, caster);
			instance.spell = this;
			foreach (SpellMod mod in mods)
				mod.LinkInstance(instance);
			foreach (Retrigger retrigger in retriggers)
				retrigger.LinkInstance(instance);
			return instance;
		}

		/// <summary>
		/// Unparents the <paramref name="instance"/> from the <see cref="SpellInstance.caster"/> and lets the <see cref="target"/> take over.
		/// </summary>
		/// <param name="instance">The <see cref="SpellInstance"/> to be fired.</param>
		public void Fire(SpellInstance instance)
		{
			target.Fire(instance);
		}

		/// <summary>
		/// Lets the <see cref="target"/> control the <paramref name="instance"/> after being fired.
		/// </summary>
		/// <param name="instance"></param>
		public void UpdateInstance(SpellInstance instance)
		{
			target.UpdateInstance(instance);
		}

		/// <summary>
		/// Lets the <see cref="target"/> spawn the <see cref="Target.destructionPrefab"/> when the <paramref name="instance"/> is destroyed.
		/// </summary>
		/// <param name="instance"></param>
		public void DestroyInstance(SpellInstance instance)
		{
			target.DestroyInstance(instance);
		}

		/// <summary>
		/// The basic method to be called when the <see cref="SpellInstance.OnCollideWithSpell"/> event is raised.
		/// </summary>
		/// <param name="sender">Unused. Not sure why a basic <see cref="object"/> is used here to begin with.</param>
		/// <param name="args">The <see cref="CollideWithSpellEventArgs"/> that contains the event data.</param>
		public abstract void CollideWithSpell(SpellInstance senderInstance, SpellInstance collisionInstance);

		/// <summary>
		/// The total mana cost of the spell to a <see cref="SpellInstance.caster"/> over a certain amount of <paramref name="time"/>.
		/// </summary>
		/// <param name="time">How long the <see cref="SpellInstance.caster"/> charges for.</param>
		/// <returns></returns>
		public float ChargeCost(float time)
		{
			return chargeRate * time;
		}

		/// <summary>
		/// Sets the color of every <see cref="ParticleMultiplier"/> associated with the <see cref="primaryElement"/>'s <see cref="Element.particleSystem"/>, assuming <see cref="Target.overrideOwnerColor"/> is false.
		/// </summary>
		/// <param name="parent">The root of the <see cref="SpellInstance"/>.</param>
		/// <param name="color">The <see cref="Color"/> to make the <see cref="ParticleSystem"/>s.</param>
		public void SetParticleColor(GameObject parent, Color color)
		{
			if (!target.overrideOwnerColor)
				foreach (ParticleMultiplier multiplier in parent.GetComponentsInChildren<ParticleMultiplier>())
					multiplier.SetColor(color);
		}

		/// <summary>
		/// The combined text of the <see cref="Spell"/>. Called by subclasses.
		/// </summary>
		/// <param name="name">The name of a subclassed <see cref="Spell"/>.</param>
		/// <param name="elements">A list of <see cref="Element"/>s pertaining to a subclassed <see cref="Spell"/>.</param>
		/// <returns></returns>
		public string GetDisplayName(string name, params Element[] elements) => $"{target}{(elements.Length > 0 ? $" {string.Join("-", (object[])elements)}" : "")}{(mods.Length > 0 ? $" {string.Join(" ", (object[])mods)}" : "")} {name}";
	}
}