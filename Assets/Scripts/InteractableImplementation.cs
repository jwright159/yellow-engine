using UnityEngine;
using Valve.VR.InteractionSystem;

namespace WrightWay.YellowVR
{
	/// <summary>
	/// Implementation of <see cref="Interactable"/>.
	/// </summary>
	[RequireComponent(typeof(Interactable))]
	public abstract class InteractableImplementation : MonoBehaviour
	{
		/// <summary>
		/// Called when a Hand starts hovering over this object.
		/// </summary>
		protected abstract void OnHandHoverBegin(Hand hand);

		/// <summary>
		/// Called every Update() while a Hand is hovering over this object (including when attached).
		/// </summary>
		protected abstract void HandHoverUpdate(Hand hand);

		/// <sumamry>
		/// Called when a Hand stops hovering over this object.
		/// </sumamry>
		protected abstract void OnHandHoverEnd(Hand hand);

		/// <summary>
		/// Called when this GameObject becomes attached to the hand.
		/// </summary>
		protected abstract void OnAttachedToHand(Hand hand);

		/// <summary>
		/// Called every Update() while this GameObject is attached to the hand.
		/// </summary>
		protected abstract void HandAttachedUpdate(Hand hand);

		/// <summary>
		/// Called when this GameObject is detached from the hand.
		/// </summary>
		protected abstract void OnDetachedFromHand(Hand hand);

		/// <summary>
		/// Called when this attached GameObject becomes the primary attached object.
		/// </summary>
		protected abstract void OnHandFocusAcquired(Hand hand);

		/// <summary>
		/// Called when another attached GameObject becomes the primary attached object.
		/// </summary>
		protected abstract void OnHandFocusLost(Hand hand);
	}
}