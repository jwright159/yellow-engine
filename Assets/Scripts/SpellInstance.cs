using System;
using UnityEngine;
using Valve.VR.InteractionSystem;
using WrightWay.YellowVR.Spells;

[RequireComponent(typeof(EnergyTransferer))]
public class SpellInstance : MonoBehaviour
{
	public enum SpellState { Charging, Active, Dead }

	public float timeout = 2;

	[NonSerialized]
	public Spell spell;

	[NonSerialized]
	public EnergyTransferer transferer;

	private Caster _owner;
	public Caster owner {
		get => _owner;
		set {
			_owner = value;
			spell.target.SetSpellColor(this, _owner.owner.color);
		}
	}

	private float lifetime;

	public float mana { get; protected set; }
	public SpellState state { get; protected set; }

	private void Awake()
	{
		transferer = GetComponent<EnergyTransferer>();
	}

	private void Start()
	{
		state = SpellState.Charging;
		transform.localPosition = Vector3.zero;
		transform.localRotation = Quaternion.identity;
		transform.localScale = Vector3.zero;
		spell.target.SetSpellColor(this, owner.owner.color);
	}

	private void Update()
	{
		if (lifetime >= timeout)
			state = SpellState.Dead;

		switch (state)
		{
			case SpellState.Charging:
				//transferer.rigidbody.mass = Mathf.Pow(mana / spell.minimumManaCost, 0.333f);
				transform.localScale = Vector3.one * Mathf.Pow(mana / spell.minimumManaCost, 0.333f);
				break;

			case SpellState.Active:
				lifetime += Time.deltaTime;
				//spell.UpdateInstance(this); // Use Fixed for physics
				break;

			case SpellState.Dead:
				Destroy(gameObject);
				break;

			default:
				throw new ArgumentException($"Spell state \"{state}\" is invalid"); // sure let's use this one
		}
	}

	private void FixedUpdate()
	{
		if (state == SpellState.Active)
		{
			spell.UpdateInstance(this);
		}
	}

	public void OnCollisionEnter(Collision collision)
	{
		Debug.Log($"Colliding with {collision.collider}");
		if (state == SpellState.Charging)
		{
			Debug.Log("Firing from collision");
			owner.Fire();
		}
		spell.Collide(this, collision);
	}

	public void Charge(float mana)
	{
		this.mana += mana;
	}

	// Only call this from Caster.Fire(). Perhaps just move this there?
	public void Fire()
	{
		if (mana < spell.minimumManaCost)
		{
			state = SpellState.Dead;
		}
		else
		{
			state = SpellState.Active;
			transform.SetParent(null);
			spell.Fire(this);
		}
	}
}
