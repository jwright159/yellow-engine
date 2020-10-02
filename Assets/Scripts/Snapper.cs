using UnityEngine;
using Valve.VR.InteractionSystem;

namespace WrightWay.YellowVR
{
	public class Snapper : InteractableImplementation
	{
		public Hand.AttachmentFlags attachmentFlags = Hand.AttachmentFlags.ParentToHand | Hand.AttachmentFlags.DetachFromOtherHand | Hand.AttachmentFlags.TurnOnKinematic;

		private Interactable interactable;

		private void Awake()
		{
			interactable = GetComponent<Interactable>();
		}

		private void Start()
		{

		}

		private void Update()
		{

		}

		protected override void OnHandHoverBegin(Hand hand)
		{

		}

		protected override void HandHoverUpdate(Hand hand)
		{
			GrabTypes startingGrabType = hand.GetGrabStarting();
			if (interactable.attachedToHand == null && startingGrabType != GrabTypes.None) // Was grabbed this update
			{
				hand.HoverLock(interactable); // Make hover messages exclusive to this object
				hand.AttachObject(gameObject, startingGrabType, attachmentFlags); // Attach this object to the hand
			}
			else if (hand.IsGrabEnding(gameObject)) // Was ungrabbed this frame
			{
				hand.DetachObject(gameObject); // Detach this object from the hand
				hand.HoverUnlock(interactable); // Allow other objects to recieve hover messages again
			}
		}

		protected override void OnHandHoverEnd(Hand hand)
		{

		}

		protected override void OnAttachedToHand(Hand hand)
		{

		}

		protected override void HandAttachedUpdate(Hand hand)
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
	}
}