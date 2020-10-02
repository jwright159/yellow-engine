using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
		foreach(Portal portal in portals)
		{
			portal.Render(camera);
		}
	}
}
