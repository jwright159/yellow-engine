using UnityEngine;

namespace WrightWay.YellowVR
{
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