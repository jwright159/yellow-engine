using System;
using UnityEngine.Events;

namespace WrightWay.YellowVR.SpellEvents
{
	public enum SpellEvent
	{
		CollideWithSpell
	}

	[Serializable]
	public class CollideWithSpellEvent : UnityEvent<SpellInstance, SpellInstance> { }
}