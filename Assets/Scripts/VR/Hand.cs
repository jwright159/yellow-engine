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

		protected virtual void Awake()
		{
			behaviourPose = GetComponent<SteamVR_Behaviour_Pose>();
		}

		protected virtual void Start()
		{
			if (gameObject.layer == 0)
				Debug.LogWarning("Hand is on default layer, change it to the Hand layer lamo", this);
			//else
				//useLayerMask &= ~(1 << gameObject.layer); // Don't even check to be usable with yourself
				// wait hold on I need the gun to be Hand
		}

		protected virtual void Update()
		{
			UpdateUseState();
		}

		/// <summary>
		/// Fire use and unuse events to the nearest usable interactable.
		/// </summary>
		private void UpdateUseState()
		{
			bool used = GetUse();
			bool unused = GetUnuse();
			
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

		protected virtual bool GetUse()
		{
			return pinchAction.GetStateDown(behaviourPose.inputSource);
		}

		protected virtual bool GetUnuse()
		{
			return pinchAction.GetStateUp(behaviourPose.inputSource);
		}
	}
}