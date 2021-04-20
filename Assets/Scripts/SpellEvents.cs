using System;
using UnityEngine.Events;

namespace WrightWay.YellowVR.SpellEvents
{
	public enum SpellEvent
	{
		CollideWithSpell
	}

	/// <summary>
	/// <see cref="EventArgs"/> for <see cref="SpellInstance.OnCollideWithSpell"/>.
	/// </summary>
	public class CollideWithSpellEventArgs : EventArgs
	{
		public CollideWithSpellEventArgs(SpellInstance senderInstance, SpellInstance collisionInstance)
		{
			this.senderInstance = senderInstance;
			this.collisionInstance = collisionInstance;
		}
		public SpellInstance senderInstance { get; }
		public SpellInstance collisionInstance { get; }
	}

	[Serializable]
	public class CollideWithSpellEvent : UnityEvent<SpellInstance, SpellInstance> { }
}