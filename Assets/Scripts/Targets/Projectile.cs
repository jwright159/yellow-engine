using UnityEngine;

namespace WrightWay.YellowVR.Targets
{
	/// <summary>
	/// A <see cref="Target"/> that fires a projectile that has an effect on impact.
	/// </summary>
	public class Projectile : Target
	{
		/// <summary>
		/// Multiplier for how fast it launches.
		/// </summary>
		public float fireFactor = 1f;

		/// <summary>
		/// Multiplier for how fast it accelerates after being fired.
		/// </summary>
		public float forceFactor = 1f;

		public override float minimumManaCost => 10;

		public override void Fire(SpellInstance instance)
		{
			float weight = 1f / (instance.mana / instance.spell.minimumManaCost);
			instance.transferer.rigidbody.AddForce(instance.transform.rotation * Vector3.forward * 2000f * Mathf.Pow(weight, 2) * fireFactor);
			//Debug.Log($"mana: {instance.mana}, minMana: {instance.spell.minimumManaCost}, unweight: {instance.mana / instance.spell.minimumManaCost}, weight: {weight}, square: {weight * weight}");
		}

		public override void UpdateInstance(SpellInstance instance)
		{
			Transform transform = instance.transform;

			// Add a little natural(?) variance
			//transform.rotation *= Quaternion.Euler(Gaussian(), Gaussian(), Gaussian()); // this is hecckin hilarious

			// TODO: change speed of bullets based on normal velocity of CAD
			// TODO: curve the bullets based on non-normal velocity of CAD
			// TODO: make the bullets accelerate instead of constant rate
			// TODO: just use physics

			// Make it go forward
			instance.transferer.rigidbody.AddForce(instance.transferer.rigidbody.velocity * 5f * Mathf.Pow(instance.mana / instance.spell.minimumManaCost, 2) * forceFactor);
		}

		private float Gaussian(float stddev = 1, float mean = 0)
		{
			float x, y, sum;
			do
			{
				x = Random.value * 2 - 1; // remapped from [0,1] to [-1,1]
				y = Random.value * 2 - 1; // remapped from [0,1] to [-1,1]
				sum = x * x + y * y;
			} while (sum >= 1);
			return x * Mathf.Sqrt(-2 * Mathf.Log(sum) / sum);
		}

		public override string ToString() => "Projecting";
	}
}