using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WrightWay.YellowVR
{
	/// <summary>
	/// Basic implementation of <see cref="Caster"/>, but evil and not usable by the player.
	/// </summary>
	[RequireComponent(typeof(Caster))]
	public class EnemyFireballSpawner : MonoBehaviour
	{
		public float maxTime;
		private float time;

		private Caster caster;

		private void Awake()
		{
			caster = GetComponent<Caster>();
		}

		private void Start()
		{
			
		}

		private void Update()
		{
			time += Time.deltaTime;
			if (time > maxTime)
			{
				time = 0;
				caster.Fire();
			}
			else if (time > 1 && !caster.hasSpellInstance)
			{
				caster.StartCharging();
			}
		}
	}
}