using System;
using UnityEngine;
using WrightWay.YellowVR.SpellEvents;

namespace WrightWay.YellowVR
{
	public abstract class Spell : MonoBehaviour
	{
		public float chargeRate;
		public float releaseRate;
		public SpellMod[] mods;
		public Retrigger[] retriggers;
		[SerializeField]
		protected Target target;
		public Element primaryElement;

		public float minimumManaCost => target.minimumManaCost;

		public SpellInstance CreateInstance(Caster caster)
		{
			SpellInstance instance = target.CreateInstance(primaryElement, caster);
			foreach (SpellMod mod in mods)
				mod.LinkInstance(instance);
			foreach (Retrigger retrigger in retriggers)
				retrigger.LinkInstance(instance);
			return instance;
		}

		public void Fire(SpellInstance instance)
		{
			target.Fire(instance);
		}

		public void UpdateInstance(SpellInstance instance)
		{
			target.UpdateInstance(instance);
		}

		public void DestroyInstance(SpellInstance instance)
		{
			target.DestroyInstance(instance);
		}

		public abstract void CollideWithSpell(object sender, CollideWithSpellEventArgs args);

		public float ChargeCost(float time)
		{
			return chargeRate * time;
		}

		public void SetParticleColor(GameObject parent, Color color)
		{
			if (!target.overrideOwnerColor)
				foreach (ParticleMultiplier multiplier in parent.GetComponentsInChildren<ParticleMultiplier>())
					multiplier.SetColor(color);
		}

		public string GetDisplayName(string name, params Element[] elements) => $"{target}{(elements.Length > 0 ? $" {string.Join("-", (object[])elements)}" : "")}{(mods.Length > 0 ? $" {string.Join(" ", (object[])mods)}" : "")} {name}";
	}
}