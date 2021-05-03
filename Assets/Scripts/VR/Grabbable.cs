using System;
using UnityEngine;

namespace WrightWay.VR
{
	/// <summary>
	/// Something that can be grabbed.
	/// </summary>
	// TODO: Make it grab
	public class Grabbable : MonoBehaviour, IUsable
	{
		/// <summary>
		/// Event fired after grabbing the object.
		/// </summary>
		public GrabbableGrabEvent OnGrabbed;
		/// <summary>
		/// Event fired after releasing the object.
		/// </summary>
		public GrabbableGrabEvent OnReleased;

		/// <summary>
		/// Whether the object is currently being held.
		/// </summary>
		[NonSerialized]
		public bool isGrabbed;

		/// <summary>
		/// Whether we've initialized the grab layers below yet.
		/// </summary>
		private static bool initializedGrabLayers;
		/// <summary>
		/// The layer for unheld Grabbables.
		/// </summary>
		private static int grabbableLayer;
		/// <summary>
		/// The layer for held Grabbables.
		/// </summary>
		private static int grabbedLayer;

		private void Awake()
		{
			if (!initializedGrabLayers)
			{
				initializedGrabLayers = true;
				grabbableLayer = LayerMask.NameToLayer("Usable");
				grabbedLayer = LayerMask.NameToLayer("Grabbed Usable");
			}
		}

		private void Start()
		{
			
		}

		private void Update()
		{

		}

		public void Use(Hand hand)
		{
			if (!isGrabbed)
				Grab(hand);
			else
				Release();
		}

		public void Unuse(Hand hand)
		{
			
		}

		private void Grab(Hand hand)
		{
			isGrabbed = true;

			transform.parent = hand.transform;
			transform.localPosition = Vector3.zero;
			transform.localRotation = Quaternion.identity;

			if (gameObject.layer != grabbableLayer)
				Debug.LogWarning($"Grabbable {this} is not on the layer {LayerMask.LayerToName(grabbableLayer)}, will be after releasing", this);
			SetChildrenLayer(grabbedLayer);

			OnGrabbed.Invoke();
		}

		private void Release()
		{
			isGrabbed = false;

			transform.parent = null;

			SetChildrenLayer(grabbableLayer);

			OnReleased.Invoke();
		}

		private void SetChildrenLayer(int layer)
		{
			foreach (Transform transform in GetComponentsInChildren<Transform>())
				transform.gameObject.layer = layer;
		}
	}
}