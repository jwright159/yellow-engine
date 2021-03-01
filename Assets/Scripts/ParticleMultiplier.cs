using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WrightWay.YellowVR
{
	/// <summary>
	/// Multiplies the rate of emission of a <see cref="ParticleSystem"/> by its <see cref="Transform.lossyScale"/>. Also sets the colors of the system.
	/// </summary>
	[RequireComponent(typeof(ParticleSystem))]
	public class ParticleMultiplier : MonoBehaviour
	{
		private ParticleSystem particles;
		private float baseRate;
		private new ParticleSystemRenderer renderer;
		private Color[] baseColors;

		private void Awake()
		{
			particles = GetComponent<ParticleSystem>();
			baseRate = particles.emission.rateOverTimeMultiplier;
			renderer = particles.GetComponent<Renderer>() as ParticleSystemRenderer;
			baseColors = new Color[renderer.materials.Length];
			for (int i = 0; i < baseColors.Length; i++)
				baseColors[i] = renderer.materials[i].color;
		}

		private void Update()
		{
			ParticleSystem.EmissionModule emission = particles.emission;
			emission.rateOverTimeMultiplier = baseRate * transform.lossyScale.magnitude;
		}

		public void SetColor(Color color)
		{
			for (int i = 0; i < baseColors.Length; i++)
				renderer.materials[i].color = baseColors[i] * color;
		}
	}
}