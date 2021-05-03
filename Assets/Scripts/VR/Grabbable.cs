using UnityEngine;

namespace WrightWay.VR
{
	/// <summary>
	/// Something that can be grabbed.
	/// </summary>
	// TODO: Make it grab
	public class Grabbable : MonoBehaviour, IUsable
	{
		/// <summary>
		/// Event for starting grabbing the object.
		/// </summary>
		public GrabbableGrabEvent OnGrabbed;
		/// <summary>
		/// Event for stoping grabbing the object.
		/// </summary>
		public GrabbableGrabEvent OnUngrabbed;

		private void Start()
		{

		}

		private void Update()
		{

		}

		/// <summary>
		/// Start grabbing the object.
		/// </summary>
		public void Use()
		{
			OnGrabbed.Invoke();
		}

		/// <summary>
		/// Stop grabbing the object.
		/// </summary>
		public void Unuse()
		{
			OnUngrabbed.Invoke();
		}
	}
}