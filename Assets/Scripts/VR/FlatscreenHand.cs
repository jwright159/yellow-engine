using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WrightWay.VR
{
	public class FlatscreenHand : Hand
	{
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

		protected override void Start()
		{
			base.Start();

			// Start this with a value so we don't have the hand in our flatscreen face
			flatscreenLastHitDistance = flatscreenRaycastDistance;
		}

		protected override void Update()
		{
			UpdateFlatscreenHand();

			base.Update();
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

		protected override bool GetUse()
		{
			return base.GetUse() || Input.GetMouseButtonDown(0);
		}

		protected override bool GetUnuse()
		{
			return base.GetUnuse() || Input.GetMouseButtonUp(0);
		}
	}
}
