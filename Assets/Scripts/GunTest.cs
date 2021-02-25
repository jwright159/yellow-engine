using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Valve.VR.InteractionSystem;
using WrightWay.YellowVR.Spells;

namespace WrightWay.YellowVR
{
	[RequireComponent(typeof(Caster))]
	public class GunTest : InteractableImplementation
	{
		private Caster caster;

		private void Awake()
		{
			caster = GetComponent<Caster>();
		}

		protected override void HandAttachedUpdate(Hand hand)
		{

		}

		protected override void HandHoverUpdate(Hand hand)
		{
			caster.HandleHand(hand);
		}

		protected override void OnAttachedToHand(Hand hand)
		{

		}

		protected override void OnDetachedFromHand(Hand hand)
		{

		}

		protected override void OnHandFocusAcquired(Hand hand)
		{

		}

		protected override void OnHandFocusLost(Hand hand)
		{

		}

		protected override void OnHandHoverBegin(Hand hand)
		{

		}

		protected override void OnHandHoverEnd(Hand hand)
		{
			if (caster.isCharging)
			{
				Debug.Log("Firing from letting go");
				caster.Fire();
			}
		}
	}
}