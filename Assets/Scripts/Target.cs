using UnityEngine;

namespace WrightWay.YellowVR
{
	/// <summary>
	/// The physical form of a <see cref="SpellInstance"/>.
	/// </summary>
	public abstract class Target : MonoBehaviour
	{
		/// <summary>
		/// If true, the <see cref="Spell.primaryElement"/>'s <see cref="Element.particleSystem"/>'s natural <see cref="Color"/> will be used instead of the <see cref="SpellInstance.caster"/>'s <see cref="Caster.owner"/>'s <see cref="Character.color"/>.
		/// </summary>
		public bool overrideOwnerColor;

		/// <summary>
		/// The actual literal physical from of the <see cref="SpellInstance"/>.
		/// </summary>
		public SpellInstance prefab;

		/// <summary>
		/// A prefab to be spawned when this <see cref="SpellInstance"/> is destroyed.
		/// </summary>
		public ParticleSystem destructionPrefab;

		/// <summary>
		/// An arbitrary minimum mana cost to use this target with.
		/// </summary>
		public abstract float minimumManaCost { get; }

		/// <summary>
		/// Create the basic form of a <see cref="SpellInstance"/>.
		/// </summary>
		/// <param name="element">The <see cref="Element"/> that is the source of the <see cref="Element.particleSystem"/>.</param>
		/// <param name="caster">The <see cref="Caster"/> creating this <see cref="SpellInstance"/>.</param>
		/// <returns>A barebones <see cref="SpellInstance"/>.</returns>
		public SpellInstance CreateInstance(Element element, Caster caster)
		{
			SpellInstance instance = Instantiate(prefab);
			instance.AddParticleSystem(element.particleSystem);
			return instance;
		}

		/// <summary>
		/// Unparents the <paramref name="instance"/> from the <see cref="SpellInstance.caster"/> and takes over.
		/// </summary>
		/// <param name="instance">The <see cref="SpellInstance"/> to be fired.</param>
		public abstract void Fire(SpellInstance instance);

		/// <summary>
		/// Controls the <paramref name="instance"/> after being fired.
		/// </summary>
		/// <param name="instance"></param>
		public abstract void UpdateInstance(SpellInstance instance);

		/// <summary>
		/// Spawns the <see cref="destructionPrefab"/> when the <paramref name="instance"/> is destroyed.
		/// </summary>
		/// <param name="instance"></param>
		public void DestroyInstance(SpellInstance instance)
		{
			if (destructionPrefab)
			{
				GameObject ps = Instantiate(destructionPrefab.gameObject);
				ps.transform.position = instance.transform.position;
				ps.transform.localScale = instance.transform.localScale;
				instance.SetParticleColor(ps);
			}
		}
	}
}