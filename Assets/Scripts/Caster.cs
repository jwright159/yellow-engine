using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Valve.VR.InteractionSystem;

namespace WrightWay.YellowVR
{
	/// <summary>
	/// Something that can cast a spell.
	/// </summary>
	public class Caster : MonoBehaviour
	{
		/// <summary>
		/// The <see cref="Sigil"/> containing the <see cref="Spell"/> to cast.
		/// </summary>
		public Sigil sigil
		{
			get => _sigil;
			set
			{
				_sigil = value;
				if (textField) textField.text = _sigil.ToString();
			}
		}
		private Sigil _sigil;

		/// <summary>
		/// The owner and user of this <see cref="Caster"/>.
		/// </summary>
		public Character owner;

		/// <summary>
		/// Divisor for <see cref="Spell.chargeRate"/>. Controls how fast the <see cref="Caster"/> works.
		/// </summary>
		public float timeEfficiency;

		/// <summary>
		/// Divisor for <see cref="Spell"/> costs. Controls how much mana is lost.
		/// </summary>
		public float costEfficiency;

		/// <summary>
		/// Describes the current <see cref="sigil"/>.
		/// </summary>
		public TextMeshProUGUI textField;

		/// <summary>
		/// Displays the current amount of <see cref="Character.mana"/>.
		/// </summary>
		public Slider manaBar;

		/// <summary>
		/// A <see cref="SpellInstance"/> that is charging from the <see cref="Caster"/>, if there is one.
		/// </summary>
		private SpellInstance spellInstance;

		/// <summary>
		/// Whether or not the <see cref="spellInstance"/> exists and is charging.
		/// </summary>
		public bool isCharging => spellInstance != null;

		/// <summary>
		/// Set up the UI elements.
		/// </summary>
		private void Start()
		{
			if (manaBar) manaBar.maxValue = owner.maxMana;
			if (textField) textField.text = sigil.ToString();
		}

		/// <summary>
		/// Charge the <see cref="sigil"/>.
		/// </summary>
		private void Update()
		{
			if (isCharging)
			{
				float cost = spellInstance.spell.ChargeCost(Time.deltaTime / timeEfficiency) / costEfficiency;
				if (cost > owner.mana)
				{
					Debug.Log($"Firing from lack of mana, need {cost}, have {owner.mana}");
					Fire();
				}
				else
				{
					owner.mana -= cost;
					spellInstance.Charge(cost);
				}
			}

			if (manaBar) manaBar.value = owner.mana;
		}

		/// <summary>
		/// Instantiate and begin charging the <see cref="sigil"/>.
		/// </summary>
		public void StartCharging()
		{
			spellInstance = sigil.CreateInstance(this);
			spellInstance.transform.parent = transform;
			spellInstance.caster = this;
		}

		/// <summary>
		/// Unbind and cast the <see cref="sigil"/>.
		/// </summary>
		public void Fire()
		{
			spellInstance.Fire();
			spellInstance = null;
		}

		/// <summary>
		/// <see cref="Hand"/> events to start/stop casting.
		/// </summary>
		/// <param name="hand"></param>
		/// <returns></returns>
		public SpellInstance.SpellState HandleHand(Hand hand)
		{
			if (hand.GetGrabStarting() != GrabTypes.None)
			{
				StartCharging();
				return SpellInstance.SpellState.Charging;
			}
			else if (hand.GetGrabEnding() != GrabTypes.None && isCharging)
			{
				Debug.Log("Firing from letting go");
				Fire();
				return SpellInstance.SpellState.Active;
			}
			return SpellInstance.SpellState.Dead;
		}
	}
}