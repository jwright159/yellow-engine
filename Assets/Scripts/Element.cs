using UnityEngine;

[CreateAssetMenu()]
public class Element : ScriptableObject
{
	public string displayName;
	public ParticleSystem particleSystem;

	public override string ToString() => displayName;
}