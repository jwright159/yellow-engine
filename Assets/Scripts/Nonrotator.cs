using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
