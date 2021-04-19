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
		[SerializeField]
		private Sigil _sigil;

		/// <summary>
		/// The interface to draw mana from.
		/// </summary>
		public ManaInterface manaInterface;

		/// <summary>
		/// Multiplier for <see cref="Spell.chargeRate"/>. Controls how fast the <see cref="Caster"/> works.
		/// </summary>
		public float timeEfficiency = 1;

		/// <summary>
		/// Divisor for <see cref="Spell"/> costs. Controls how much mana is lost.
		/// </summary>
		public float costEfficiency = 1;

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
		private SpellInstance spellInstance
		{
			get => _spellInstance;
			set
			{
				if (hasSpellInstance)
				{
					spellInstance.Fire(); // This is probably wanted behavior
				}
				_spellInstance = value;
				if (hasSpellInstance)
				{
					spellInstance.transform.parent = transform;
					spellInstance.caster = this;
					spellInstance.Reattach();
				}
			}
		}
		private SpellInstance _spellInstance;

		/// <summary>
		/// Whether the <see cref="spellInstance"/> exists.
		/// </summary>
		public bool hasSpellInstance => spellInstance != null;

		/// <summary>
		/// Whether or not the <see cref="spellInstance"/> exists and is charging.
		/// </summary>
		public bool isCharging => hasSpellInstance && spellInstance.state == SpellInstance.SpellState.Charging;

		/// <summary>
		/// Set up the UI elements.
		/// </summary>
		private void Start()
		{
			if (manaBar) manaBar.maxValue = manaInterface.maxMana;
			if (textField) textField.text = sigil.ToString();
		}

		/// <summary>
		/// Charge the <see cref="sigil"/>.
		/// </summary>
		private void Update()
		{
			if (hasSpellInstance)
			{
				// If holding a thing at all, remove your mana, but only charge it if it's charging
				float cost = spellInstance.spell.ChargeCost(Time.deltaTime * timeEfficiency) / costEfficiency;
				if (cost > manaInterface.mana)
				{
					Debug.Log($"Firing from lack of mana, need {cost}, have {manaInterface.mana}");
					Fire();
				}
				else
				{
					manaInterface.mana -= cost;
					if (isCharging)
					{
						spellInstance.Charge(cost);
					}
				}
			}

			if (manaBar) manaBar.value = manaInterface.mana;
		}

		/// <summary>
		/// Instantiate and begin charging the <see cref="sigil"/>.
		/// </summary>
		public void StartCharging()
		{
			Attach(sigil.CreateInstance(this));
		}

		/// <summary>
		/// Attach a <see cref="SpellInstance"/> to the caster.
		/// </summary>
		/// <param name="newInstance">The new instance to attach.</param>
		/// <returns>Whether this caused a spell to fire.</returns>
		public bool Attach(SpellInstance newInstance)
		{
			bool rtn = hasSpellInstance;
			spellInstance = newInstance;
			return rtn;
		}

		/// <summary>
		/// Unbind and cast the <see cref="sigil"/>.
		/// </summary>
		public void Fire()
		{
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
			else if (hand.GetGrabEnding() != GrabTypes.None && hasSpellInstance)
			{
				Debug.Log("Firing from letting go");
				Fire();
				return SpellInstance.SpellState.Active;
			}
			return SpellInstance.SpellState.Dead;
		}
	}
}