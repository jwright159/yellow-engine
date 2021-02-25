using UnityEngine;

namespace WrightWay.YellowVR
{
	public abstract class Target : MonoBehaviour
	{
		public bool overrideOwnerColor;
		public SpellInstance prefab;
		public ParticleSystem destructionPrefab;

		public abstract float minimumManaCost { get; }

		public SpellInstance CreateInstance(Element element, Caster caster)
		{
			SpellInstance instance = Instantiate(prefab);
			instance.AddParticleSystem(element.particleSystem);
			return instance;
		}

		public abstract void Fire(SpellInstance instance);

		public abstract void UpdateInstance(SpellInstance instance);

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