using UnityEngine;
using Valve.VR;

namespace WrightWay.VR
{
	/// <summary>
	/// One of the two hands used in VR.
	/// </summary>
	[RequireComponent(typeof(SteamVR_Behaviour_Pose))]
	public class Hand : MonoBehaviour
	{
		/// <summary>
		/// The center point with which to query usable <see cref="Usable"/>s.
		/// </summary>
		public Transform useCollisionPoint;
		/// <summary>
		/// The radius with which to query usable <see cref="Usable"/>s.
		/// </summary>
		public float useCollisionRadius;

		/// <summary>
		/// Typically the "use" action.
		/// </summary>
		public SteamVR_Action_Boolean pinchAction = SteamVR_Input.GetAction<SteamVR_Action_Boolean>("GrabPinch");
		/// <summary>
		/// Typically the "grab" action.
		/// </summary>
		public SteamVR_Action_Boolean gripAction = SteamVR_Input.GetAction<SteamVR_Action_Boolean>("GrabGrip");

		/// <summary>
		/// The mask from which to find usable <see cref="Usable"/>s.
		/// </summary>
		public LayerMask useLayerMask = -1;

		/// <summary>
		/// The actual SteamVR interface.
		/// </summary>
		private SteamVR_Behaviour_Pose behaviourPose;

		/// <summary>
		/// The maximum number of colliders to query for.
		/// </summary>
		private const int MaxOverlappingColliders = 32;
		/// <summary>
		/// A list of colliders colliding with this hand based on the <see cref="useCollisionPoint"/> and <see cref="useCollisionRadius"/>.
		/// </summary>
		private Collider[] overlappingColliders = new Collider[MaxOverlappingColliders];

		private void Awake()
		{
			behaviourPose = GetComponent<SteamVR_Behaviour_Pose>();
		}

		private void Start()
		{
			if (gameObject.layer == 0)
				Debug.LogWarning("Hand is on default layer, change it to the Hand layer lamo", this);
			else
				useLayerMask &= ~(1 << this.gameObject.layer); // Don't even check to be usable with yourself
		}

		private void Update()
		{
			UpdateUseState();
		}

		/// <summary>
		/// Fire use and unuse events to the nearest usable interactable.
		/// </summary>
		private void UpdateUseState()
		{
			bool used = pinchAction.GetStateDown(behaviourPose.inputSource);
			bool unused = pinchAction.GetStateUp(behaviourPose.inputSource);
			
			if (used || unused)
			{
				Usable interactable = GetClosestInteractable();
				if (interactable)
				{
					if (used)
						interactable.Use();
					if (unused)
						interactable.Unuse();
				}
			}
		}

		/// <summary>
		/// Get the closest usable <see cref="Usable"/> to the <see cref="useCollisionPoint"/> within the <see cref="useCollisionRadius"/>.
		/// </summary>
		private Usable GetClosestInteractable()
		{
			return GetClosestInteractable(useCollisionPoint.position, useCollisionRadius);
		}

		/// <summary>
		/// Get the closest usable <see cref="Usable"/> to the <paramref name="position"/> within the <paramref name="radius"/>.
		/// </summary>
		/// <param name="position"></param>
		/// <param name="radius"></param>
		private Usable GetClosestInteractable(Vector3 position, float radius)
		{
			int colliderAmount = Physics.OverlapSphereNonAlloc(position, radius, overlappingColliders, useLayerMask);

			if (colliderAmount >= MaxOverlappingColliders)
				Debug.LogWarning("Hand collider amount limit reached, might lose some results");

			Usable closestInteractable = null;
			float closestDistance = radius; // Probably. Might use float.MaxValue

			for (int i = 0; i < colliderAmount; i++)
			{
				Collider collider = overlappingColliders[i];
				overlappingColliders[i] = null;

				Usable interactable = collider.GetComponent<Usable>();
				if (interactable == null)
					continue;

				float distance = Vector3.Distance(position, collider.transform.position); // Something something sqrMagnitude

				if (distance < closestDistance)
				{
					closestInteractable = interactable;
					closestDistance = distance;
				}
			}

			return closestInteractable;
		}
	}
}