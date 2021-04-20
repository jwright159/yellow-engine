using System;
using UnityEngine;
using WrightWay.YellowVR.SpellEvents;

namespace WrightWay.YellowVR
{
	/// <summary>
	/// Triggers another <see cref="spell"/> event on the event <see cref="trigger"/>.
	/// </summary>
	public class Retrigger : MonoBehaviour
	{
		/// <summary>
		/// The <see cref="SpellEvent"/> on which to cast the <see cref="spell"/>, as well as which event on the spell gets fired.
		/// </summary>
		public SpellEvent trigger;

		/// <summary>
		/// The <see cref="Spell"/> to cast.
		/// </summary>
		public Spell spell;

		/// <summary>
		/// Link this <see cref="Retrigger"/> to the <paramref name="instance"/>.
		/// </summary>
		/// <param name="instance">The <see cref="SpellInstance"/> to link to.</param>
		public void LinkInstance(SpellInstance instance)
		{
			// Hopefully all these subscribbles get discarded when SpellInstance gets destroyed.
			switch (trigger)
			{
				case SpellEvent.CollideWithSpell:
					instance.OnCollideWithSpell.AddListener(spell.CollideWithSpell);
					break;
				default:
					throw new NullReferenceException("Retrigger trigger cannot be null.");
			}
		}
	}
}
