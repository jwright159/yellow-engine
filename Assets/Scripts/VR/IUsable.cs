using UnityEngine;

namespace WrightWay.VR
{
	/// <summary>
	/// Something that can be used.
	/// </summary>
	public interface IUsable
	{
		/// <summary>
		/// Start using the object.
		/// </summary>
		public void Use();

		/// <summary>
		/// Stop using the object.
		/// </summary>
		public void Unuse();
	}
}