using System;
using UnityEngine;
using UnityEngine.Events;
using WrightWay.YellowVR.SpellEvents;

namespace WrightWay.YellowVR
{
	/// <summary>
	/// The physical manifestation of a <see cref="spell"/>.
	/// </summary>
	[RequireComponent(typeof(EnergyTransferer))]
	public class SpellInstance : MonoBehaviour
	{
		/// <summary>
		/// A list of <see cref="Transforms"/> to parent a <see cref="Element.particleSystem"/> to.
		/// </summary>
		public Transform[] particleParents;

		/// <summary>
		/// The time in seconds before the <see cref="SpellInstance"/> is forcefully destroyed.
		/// </summary>
		[NonSerialized]
		public float timeout = 2;

		/// <summary>
		/// The manifested <see cref="Spell"/>.
		/// </summary>
		[NonSerialized]
		public Spell spell;

		/// <summary>
		/// Contains standard <see cref="EnergyTransferer.ElementalInterface"/>s.
		/// </summary>
		[NonSerialized]
		public EnergyTransferer transferer;

		/// <summary>
		/// The <see cref="Caster"/> that "owns" this <see cref="SpellInstance"/>.
		/// </summary>
		public Caster caster
		{
			get => _caster;
			set
			{
				_caster = value;
				spell.SetParticleColor(gameObject, _caster.manaInterface.color);
			}
		}
		private Caster _caster;

		/// <summary>
		/// How long in seconds this <see cref="SpellInstance"/> has been alive.
		/// </summary>
		private float lifetime;

		/// <summary>
		/// If true, doesn't spawn the <see cref="Target.destructionPrefab"/>. True when the application is quit.
		/// </summary>
		private bool destroySilent;

		/// <summary>
		/// The amount of stored mana.
		/// </summary>
		public float mana { get; protected set; }
		/// <summary>
		/// The current state of the <see cref="SpellInstance"/>.
		/// </summary>
		public SpellState state { get; protected set; }
		/// <summary>
		/// Charging: Attached to a caster and recieving mana.
		/// Active: Out in the world.
		/// Inactive: Attached to a caster and not doing anything.
		/// Dead: Dead.
		/// </summary>
		public enum SpellState { Charging, Active, Inactive, Dead }

		/// <summary>
		/// An event that is raised when this <see cref="SpellInstance"/> collides with another <see cref="SpellInstance"/>.
		/// </summary>
		public CollideWithSpellEvent OnCollideWithSpell;

		/// <summary>
		/// Assign some values.
		/// </summary>
		private void Awake()
		{
			transferer = GetComponent<EnergyTransferer>();
		}

		/// <summary>
		/// Reset some values.
		/// </summary>
		private void Start()
		{
			Reattach();
			state = SpellState.Charging;

			OnCollideWithSpell.AddListener(spell.CollideWithSpell);
		}


		/// <summary>
		/// It's Update. Imagine that.
		/// </summary>
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

				case SpellState.Inactive:
					break;

				case SpellState.Dead:
					Destroy(gameObject);
					break;

				default:
					throw new ArgumentException($"Spell state \"{state}\" is invalid"); // sure let's use this one
			}
		}

		/// <summary>
		/// It's FixedUpdate. Guess what that's for.
		/// </summary>
		private void FixedUpdate()
		{
			if (state == SpellState.Active)
			{
				spell.UpdateInstance(this);
			}
		}

		/// <summary>
		/// Can I stop writing documentation now?
		/// </summary>
		/// <param name="collision">The collision we just entered.</param>
		private void OnCollisionEnter(Collision collision)
		{
			Debug.Log($"Colliding {gameObject} with {collision.collider}");
			if (state == SpellState.Charging)
			{
				Debug.Log("Firing from collision");
				caster.Fire();
			}

			SpellInstance collisionInstance = collision.gameObject.GetComponent<SpellInstance>();
			if (collisionInstance)
			{
				OnCollideWithSpell.Invoke(this, collisionInstance);
			}
		}

		/// <summary>
		/// Disables <see cref="Target.destructionPrefab"/> so things don't get instantiated on quit.
		/// </summary>
		private void OnApplicationQuit()
		{
			destroySilent = true;
		}

		/// <summary>
		/// Spawns (or doesn't) <see cref="Target.destructionPrefab"/>.
		/// </summary>
		private void OnDestroy()
		{
			if (!destroySilent)
				spell.DestroyInstance(this);
		}

		/// <summary>
		/// Adds <paramref name="mana"/> to <see cref="mana"/>.
		/// </summary>
		/// <param name="mana">How much mana to add.</param>
		public void Charge(float mana)
		{
			this.mana += mana;
		}

		/// <summary>
		/// Either fail the spell or cast it from the <see cref="caster"/>.
		/// </summary>
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

		/// <summary>
		/// Attach this instance to its current <see cref="caster"/>.
		/// </summary>
		public void Reattach()
		{
			state = SpellState.Inactive;
			transform.SetParent(caster.transform);
			transform.localPosition = Vector3.zero;
			transform.localRotation = Quaternion.identity;
			transform.localScale = Vector3.one;
			transferer.rigidbody.velocity = Vector3.zero;
			transferer.rigidbody.angularVelocity = Vector3.zero;
			lifetime = 0;
		}

		/// <summary>
		/// Parents new <paramref name="particleSystem"/>s to the <see cref="particleParents"/>.
		/// </summary>
		/// <param name="particleSystem">The <see cref="ParticleSystem"/> to add.</param>
		public void AddParticleSystem(ParticleSystem particleSystem)
		{
			foreach (Transform parent in particleParents)
			{
				Instantiate(particleSystem.gameObject, parent);
			}
		}

		/// <summary>
		/// Colors the <see cref="ParticleSystem"/>s the <see cref="caster"/>'s <see cref="Caster.owner"/>'s <see cref="Character.color"/>.
		/// </summary>
		/// <param name="parent">The root of the <see cref="ParticleSystem"/>s.</param>
		public void SetParticleColor(GameObject parent)
		{
			spell.SetParticleColor(parent, caster.manaInterface.color);
		}
	}
}