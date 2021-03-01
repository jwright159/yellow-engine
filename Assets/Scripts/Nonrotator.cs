using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WrightWay.YellowVR
{
	/// <summary>
	/// A constraint that just prevents a <see cref="Transform"/> from rotating after starting.
	/// </summary>
	public class Nonrotator : MonoBehaviour
	{
		private Vector3 angles;

		private void Start()
		{
			angles = transform.localEulerAngles;
		}

		private void Update()
		{
			transform.eulerAngles = angles;
		}
	}
}