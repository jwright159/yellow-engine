using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WrightWay.YellowVR
{
	/// <summary>
	/// A <see cref="Camera"/> that wants <see cref="Portal"/>s rendered, usually the main camera.
	/// </summary>
	[RequireComponent(typeof(Camera))]
	public class PortalCamera : MonoBehaviour
	{
		public Portal[] portals;

		private new Camera camera;

		private void Awake()
		{
			camera = GetComponent<Camera>();
		}

		private void OnPreCull()
		{
			foreach (Portal portal in portals)
			{
				portal.Render(camera);
			}
		}
	}
}