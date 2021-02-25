using System;
using UnityEngine;
using WrightWay.YellowVR.SpellEvents;

namespace WrightWay.YellowVR
{
	[RequireComponent(typeof(EnergyTransferer))]
	public class SpellInstance : MonoBehaviour
	{
		public enum SpellState { Charging, Active, Dead }

		public Transform[] particleParents;

		public float timeout = 2;

		[NonSerialized]
		public Spell spell;

		[NonSerialized]
		public EnergyTransferer transferer;

		private Caster _caster;
		public Caster caster
		{
			get => _caster;
			set
			{
				_caster = value;
				spell.SetParticleColor(gameObject, _caster.owner.color);
			}
		}

		private float lifetime;

		private bool destroySilent;

		public float mana { get; protected set; }
		public SpellState state { get; protected set; }

		public event EventHandler<CollideWithSpellEventArgs> CollidedWithSpell;

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
			spell.SetParticleColor(gameObject, caster.owner.color);

			CollidedWithSpell += spell.CollideWithSpell;
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

		private void OnCollisionEnter(Collision collision)
		{
			Debug.Log($"Colliding with {collision.collider}");
			if (state == SpellState.Charging)
			{
				Debug.Log("Firing from collision");
				caster.Fire();
			}

			SpellInstance collisionInstance = collision.gameObject.GetComponent<SpellInstance>();
			if (collisionInstance)
			{
				CollidedWithSpell?.Invoke(this, new CollideWithSpellEventArgs(this, collisionInstance));
			}
		}

		private void OnApplicationQuit()
		{
			destroySilent = true;
		}

		private void OnDestroy()
		{
			if (!destroySilent)
				spell.DestroyInstance(this);
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

		public void AddParticleSystem(ParticleSystem particleSystem)
		{
			foreach (Transform parent in particleParents)
			{
				Instantiate(particleSystem.gameObject, parent);
			}
		}

		public void SetParticleColor(GameObject parent)
		{
			spell.SetParticleColor(parent, caster.owner.color);
		}
	}
}