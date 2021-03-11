using UnityEngine;

namespace WrightWay.YellowVR
{
	/// <summary>
	/// Some standard info for some elements. TODO: Should not be <see cref="ScriptableObject"/>.
	/// </summary>
	[CreateAssetMenu()]
	public class Element : ScriptableObject
	{
		/// <summary>
		/// The name of the element, probably latin or something.
		/// </summary>
		public string displayName;

		/// <summary>
		/// A <see cref="ParticleSystem"/> to attach to <see cref="SpellInstance"/>s at <see cref="SpellInstance.particleParents"/>.
		/// </summary>
		public ParticleSystem particleSystem;

		public override string ToString() => displayName;
	}
}