using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Valve.VR.InteractionSystem;

namespace WrightWay.YellowVR
{
	public class Caster : MonoBehaviour
	{
		public Sigil sigil;
		public TextMeshProUGUI textField;
		public Slider manaBar;
		public float maxMana = 100;
		public float manaRate = 5;
		public Character owner;

		public bool isCharging => spellInstance != null;

		private float _mana;
		private float mana
		{
			get => _mana;
			set
			{
				_mana = value;
				if (_mana > maxMana) _mana = maxMana;
				if (manaBar) manaBar.value = _mana;
			}
		}
		private SpellInstance spellInstance;

		private void Start()
		{
			mana = maxMana;
			if (manaBar) manaBar.maxValue = maxMana;
			if (textField) textField.text = sigil.ToString();
		}

		private void Update()
		{
			if (isCharging)
			{
				float cost = spellInstance.spell.ChargeCost(Time.deltaTime);
				if (cost > mana)
				{
					Debug.Log($"Firing from lack of mana, need {cost}, have {mana}");
					Fire();
				}
				else
				{
					mana -= cost;
					spellInstance.Charge(cost);
				}
			}
			else
			{
				mana += manaRate * Time.deltaTime;
			}
		}

		public void StartCharging()
		{
			spellInstance = sigil.CreateInstance(this);
			spellInstance.transform.parent = transform;
			spellInstance.caster = this;
		}

		public void Fire()
		{
			spellInstance.Fire();
			spellInstance = null;
		}

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