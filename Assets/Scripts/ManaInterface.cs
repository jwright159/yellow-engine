using UnityEngine;

namespace WrightWay.YellowVR
{
	/// <summary>
	/// An interface to add and subract mana from.
	/// </summary>
	[CreateAssetMenu()]
	public class ManaInterface : ScriptableObject
	{
		/// <summary>
		/// The color of the interface's mana.
		/// </summary>
		public Color color = Color.white;

		/// <summary>
		/// The maximum amount of mana an interface has.
		/// </summary>
		public float maxMana = 100;

		/// <summary>
		/// How much mana the interface has.
		/// </summary>
		public float mana
		{
			get => _mana;
			set
			{
				_mana = value;
				if (_mana > maxMana) _mana = maxMana;
			}
		}
		private float _mana;

		/// <summary>
		/// Reset the mana.
		/// </summary>
		private void Awake()
		{
			mana = maxMana;
		}
	}
}