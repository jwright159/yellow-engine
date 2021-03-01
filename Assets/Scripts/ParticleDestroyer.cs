using UnityEngine;

namespace WrightWay.YellowVR
{
	/// <summary>
	/// Destroys a <see cref="GameObject"/> after a <see cref="ParticleSystem"/> dies.
	/// </summary>
	[RequireComponent(typeof(ParticleSystem))]
	public class ParticleDestroyer : MonoBehaviour
	{
		private new ParticleSystem particleSystem;

		private void Awake()
		{
			particleSystem = GetComponent<ParticleSystem>();
		}

		private void Update()
		{
			if (!particleSystem.IsAlive())
				Destroy(gameObject);
		}
	}
}