using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UsableTestCube : MonoBehaviour
{
	private Material material;

	private void Start()
	{
		material = GetComponent<MeshRenderer>().material;
	}

	public void ChangeColor()
	{
		material.color = Random.ColorHSV(0, 1, 1, 1, 1, 1);
	}
}
