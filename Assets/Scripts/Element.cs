using UnityEngine;

namespace WrightWay.YellowVR
{
	/// <summary>
	/// Some standard info for some elements. TODO: Should not be <see cref="ScriptableObject"/>.
	/// </summary>
	[CreateAssetMenu()]
	public class Element : ScriptableObject
	{
		public string displayName;
		public ParticleSystem particleSystem;

		public override string ToString() => displayName;
	}
}