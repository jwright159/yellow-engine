using UnityEngine;

namespace WrightWay.YellowVR.Targets
{
	public abstract class Target : MonoBehaviour
	{
		public bool overrideOwnerColor;

		public abstract float minimumManaCost { get; }

		public abstract SpellInstance CreateInstance(Element element, Caster caster);

		public abstract void Fire(SpellInstance instance);

		public abstract void UpdateInstance(SpellInstance instance);

		public abstract void SetSpellColor(SpellInstance instance, Color color);
	}
}