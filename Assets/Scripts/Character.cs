using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WrightWay.YellowVR
{
	/// <summary>
	/// Someone that can probably cast spells.
	/// </summary>
	public class Character : MonoBehaviour
	{
		/// <summary>
		/// The interface to exchange mana with.
		/// </summary>
		public ManaInterface manaInterface;

		/// <summary>
		/// The mana regeneration rate in arbitrary units per second. Each character contributes to this.
		/// </summary>
		public float manaRegen = 1;

		/// <summary>
		/// Recharge some mana.
		/// </summary>
		private void Update()
		{
			manaInterface.mana += manaRegen * Time.deltaTime;
		}
	}
}