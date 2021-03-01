using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WrightWay.YellowVR
{
	/// <summary>
	/// Someone, usually <see cref="Caster.owner"/>.
	/// </summary>
	public class Character : MonoBehaviour
	{
		/// <summary>
		/// The color of the character's mana.
		/// </summary>
		public Color color;

		/// <summary>
		/// The maximum amount of mana a character has.
		/// </summary>
		public float maxMana = 100;

		/// <summary>
		/// The mana regeneration rate in arbitrary units per second.
		/// </summary>
		public float manaRegen = 1;

		/// <summary>
		/// How much mana the character has.
		/// </summary>
		public float mana
		{
			get => _mana;
			set
			{
				_mana = value;
				if (_mana > maxMana) _mana = maxMana;
			}
		}
		private float _mana;

		/// <summary>
		/// Reset the mana.
		/// </summary>
		private void Awake()
		{
			mana = maxMana;
		}

		/// <summary>
		/// Recharge some mana.
		/// </summary>
		private void Update()
		{
			mana += manaRegen * Time.deltaTime;
		}
	}
}