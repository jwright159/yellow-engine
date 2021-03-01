using UnityEngine;

namespace WrightWay.YellowVR
{
	/// <summary>
	/// A very cool visual portal that doesn't do anything and breaks in VR.
	/// </summary>
	public class Portal : MonoBehaviour
	{
		/// <summary>
		/// The paired <see cref="Portal"/>.
		/// </summary>
		public Portal otherPortal;

		/// <summary>
		/// The <see cref="Camera"/> to render the outside of this <see cref="Portal"/> from.
		/// </summary>
		public Camera renderCamera;

		/// <summary>
		/// The <see cref="MeshRenderer"/> containing the <see cref="Material"/> to be overriden with the <see cref="RenderTexture"/>.
		/// </summary>
		public MeshRenderer meshRenderer;

		private RenderTexture renderTex;

		private void Awake()
		{
			renderTex = new RenderTexture(Screen.width, Screen.height, 0);
			renderCamera.targetTexture = renderTex;
			renderCamera.enabled = false;
			meshRenderer.material.SetTexture("_MainTex", renderTex);
		}

		/// <summary>
		/// Renders the view on the other side of the portal from the perspective of a given <see cref="Camera"/>.
		/// </summary>
		/// <param name="camera">The <see cref="Camera"/> on (this? the other? TODO) side of the <see cref="Portal"/>.</param>
		public void Render(Camera camera)
		{
			bool onFrontSide = transform.InverseTransformPoint(camera.transform.position).y >= 0;

			// maincam_world -> get local to thisportal -> transform to local to otherportal (but all of this is backwards bc matrices) (not flipping bc convenicence of normals)
			Matrix4x4 transformMatrix = otherPortal.transform.localToWorldMatrix * transform.worldToLocalMatrix * camera.transform.localToWorldMatrix;
			renderCamera.transform.SetPositionAndRotation(transformMatrix.GetPosition(), transformMatrix.rotation);

			// up instead of forward bc I'm dumb
			Vector3 otherPortalNormal = otherPortal.transform.up * (onFrontSide ? -1 : 1);
			Plane clipPlanePlane = new Plane(otherPortalNormal, otherPortal.transform.position - otherPortalNormal * 0.01f);
			Vector4 clipPlane = new Vector4(clipPlanePlane.normal.x, clipPlanePlane.normal.y, clipPlanePlane.normal.z, clipPlanePlane.distance);
			Vector4 clipPlaneCameraSpace = Matrix4x4.Transpose(Matrix4x4.Inverse(renderCamera.worldToCameraMatrix)) * clipPlane;
			renderCamera.projectionMatrix = camera.CalculateObliqueMatrix(clipPlaneCameraSpace);

			otherPortal.meshRenderer.enabled = false;
			renderCamera.Render();
			otherPortal.meshRenderer.enabled = true;
		}
	}
}