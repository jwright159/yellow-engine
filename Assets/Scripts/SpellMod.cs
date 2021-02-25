using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WrightWay.YellowVR
{
	// Essentially this class is full of "hooks" (or whatever) that happen at certain events.
	// Consider using events?

	// Events are stored in SpellInstance, but spell mods can subscribe to those

	public abstract class SpellMod : MonoBehaviour
	{
		public abstract void LinkInstance(SpellInstance instance);
	}
}
