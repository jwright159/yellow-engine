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
		/// The camera with which to raycast in non-VR.
		/// </summary>
		public Camera flatscreenCamera;
		/// <summary>
		/// The maximum distance to raycast at in non-VR.
		/// </summary>
		public float flatscreenRaycastDistance;
		/// <summary>
		/// A debug object showing where our raycast is coming from. Should be on the Ignore Raycast layer.
		/// </summary>
		public Transform flatscreenAim;
		/// <summary>
		/// The distance from the camera to the aim object.
		/// </summary>
		public float flatscreenAimDistance;
		/// <summary>
		/// The last distance that a raycast was hit at.
		/// </summary>
		private float flatscreenLastHitDistance;

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
			//else
				//useLayerMask &= ~(1 << gameObject.layer); // Don't even check to be usable with yourself
				// wait hold on I need the gun to be Hand

			// Start this with a value so we don't have the hand in our flatscreen face
			flatscreenLastHitDistance = flatscreenRaycastDistance;
		}

		private void Update()
		{
			if (flatscreenCamera != null)
				UpdateFlatscreenHand();
			UpdateUseState();
		}

		/// <summary>
		/// Fire use and unuse events to the nearest usable interactable.
		/// </summary>
		private void UpdateUseState()
		{
			bool used = pinchAction.GetStateDown(behaviourPose.inputSource) || (flatscreenCamera != null && Input.GetMouseButtonDown(0));
			bool unused = pinchAction.GetStateUp(behaviourPose.inputSource) || (flatscreenCamera != null && Input.GetMouseButtonUp(0));
			
			if (used || unused)
			{
				Usable usable = GetClosestUsable();
				Debug.Log($"Doing something to {usable} around {useCollisionPoint.position} within {useCollisionRadius}", this);
				if (usable)
				{
					if (used)
						usable.Use();
					if (unused)
						usable.Unuse();
				}
			}
		}

		/// <summary>
		/// Get the closest usable <see cref="Usable"/> to the <see cref="useCollisionPoint"/> within the <see cref="useCollisionRadius"/>.
		/// </summary>
		private Usable GetClosestUsable()
		{
			return GetClosestUsable(useCollisionPoint.position, useCollisionRadius);
		}

		/// <summary>
		/// Get the closest usable <see cref="Usable"/> to the <paramref name="position"/> within the <paramref name="radius"/>.
		/// </summary>
		/// <param name="position"></param>
		/// <param name="radius"></param>
		private Usable GetClosestUsable(Vector3 position, float radius)
		{
			int colliderAmount = Physics.OverlapSphereNonAlloc(position, radius, overlappingColliders, useLayerMask);

			if (colliderAmount >= MaxOverlappingColliders)
				Debug.LogWarning("Hand collider amount limit reached, might lose some results");

			Usable closestUsable = null;
			float closestDistance = float.MaxValue; // Can't just use radius bc it's touching by faces not by centers lmao

			for (int i = 0; i < colliderAmount; i++)
			{
				Collider collider = overlappingColliders[i];
				overlappingColliders[i] = null;

				Usable usable = collider.GetComponentInParent<Usable>();
				if (usable == null)
					continue;

				float distance = Vector3.Distance(position, collider.transform.position); // Something something sqrMagnitude

				if (distance < closestDistance)
				{
					closestUsable = usable;
					closestDistance = distance;
				}
			}

			return closestUsable;
		}

		/// <summary>
		/// Update a hand based on the mouse cursor on the screen.
		/// </summary>
		public void UpdateFlatscreenHand()
		{
			Ray ray = flatscreenCamera.ScreenPointToRay(Input.mousePosition);

			// Move the hand and aim so we don't hit them
			transform.position = flatscreenCamera.transform.TransformPoint(Vector3.back * 1000);
			// HACK: For some reason I can't move aim in order to not get hit, so it has been banished to the ignore raycast layer.

			RaycastHit hit;
			if (Physics.Raycast(ray, out hit, flatscreenRaycastDistance))
			{
				// Put the hand on the hit point so we might interact with it
				transform.position = hit.point;

				flatscreenLastHitDistance = hit.distance;
			}
			else
			{
				// Didn't hit anything, but move the hand around in the empty air
				transform.position = ray.origin + ray.direction * flatscreenLastHitDistance;
			}

			// Update the aim for debugging purposes (after the raycast)
			if (flatscreenAim != null)
			{
				flatscreenAim.position = ray.origin + flatscreenAimDistance * ray.direction;
				flatscreenAim.rotation = Quaternion.LookRotation(ray.direction, Vector3.up);
			}
		}
		// TODO: Make our own flatscreen camera controller
	}
}