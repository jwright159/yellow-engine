using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Valve.VR.InteractionSystem;
using WrightWay.YellowVR.Spells;

[RequireComponent(typeof(Caster))]
public class CAD : InteractableImplementation
{
	private Caster caster;

	private void Awake()
	{
		caster = GetComponent<Caster>();
	}

	protected override void HandAttachedUpdate(Hand hand)
	{
		caster.HandleHand(hand);
	}

	protected override void HandHoverUpdate(Hand hand)
	{
		
	}

	protected override void OnAttachedToHand(Hand hand)
	{
		Debug.Log("Attaching CAD");
	}

	protected override void OnDetachedFromHand(Hand hand)
	{
		Debug.Log("Detaching CAD");
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
		
	}
}
