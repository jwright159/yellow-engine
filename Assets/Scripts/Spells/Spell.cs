using System;
using UnityEngine;
using WrightWay.YellowVR.Spells.SpellMods;
using WrightWay.YellowVR.Targets;

namespace WrightWay.YellowVR.Spells
{
	public abstract class Spell : MonoBehaviour
	{
		public float chargeRate;
		public float releaseRate;
		public SpellMod[] mods;
		public Target target;

		public float minimumManaCost => target.minimumManaCost;

		public abstract SpellInstance CreateInstance(Caster caster);

		public void Fire(SpellInstance instance)
		{
			target.Fire(instance);
		}

		public void UpdateInstance(SpellInstance instance)
		{
			target.UpdateInstance(instance);
		}

		public abstract void Collide(SpellInstance instance, Collision collision);

		public float ChargeCost(float time)
		{
			return chargeRate * time;
		}

		public string GetDisplayName(string name, params Element[] elements) => $"{target}{(elements.Length > 0 ? $" {string.Join("-", (object[])elements)}" : "")}{(mods.Length > 0 ? $" {string.Join(" ", (object[])mods)}" : "")} {name}";
	}
}