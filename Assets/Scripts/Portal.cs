using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

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
		/// The <see cref="Camera"/> to render onto the <see cref="renderTex"/>.
		/// </summary>
		public Camera renderCamera;

		/// <summary>
		/// The <see cref="MeshRenderer"/> containing the <see cref="Material"/> to be overriden with the <see cref="renderTex"/>.
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

		private void Start()
		{
			RenderPipelineManager.beginCameraRendering += RenderPortal;
		}

		private void OnDestroy()
		{
			RenderPipelineManager.beginCameraRendering -= RenderPortal;
		}

		/// <summary>
		/// Renders the view on the other side of the portal from the perspective of a given <see cref="Camera"/>.
		/// </summary>
		/// <param name="frameCamera">The <see cref="Camera"/> that is rendering the frame.</param>
		public void RenderPortal(ScriptableRenderContext context, Camera frameCamera)
		{
			bool onFrontSide = transform.InverseTransformPoint(frameCamera.transform.position).y >= 0;

			// maincam_world -> get local to thisportal -> transform to local to otherportal (but all of this is backwards bc matrices) (not flipping bc convenicence of normals)
			Matrix4x4 transformMatrix = otherPortal.transform.localToWorldMatrix * transform.worldToLocalMatrix * frameCamera.transform.localToWorldMatrix;
			renderCamera.transform.SetPositionAndRotation(transformMatrix.GetPosition(), transformMatrix.rotation);
			renderCamera.transform.RotateAround(otherPortal.transform.position, otherPortal.transform.forward, 180);

			// up instead of forward bc I'm dumb
			Vector3 otherPortalNormal = otherPortal.transform.up * (onFrontSide ? 1 : -1);
			Plane clipPlanePlane = new Plane(otherPortalNormal, otherPortal.transform.position - otherPortalNormal * 0.01f);
			Vector4 clipPlane = new Vector4(clipPlanePlane.normal.x, clipPlanePlane.normal.y, clipPlanePlane.normal.z, clipPlanePlane.distance);
			Vector4 clipPlaneCameraSpace = Matrix4x4.Transpose(Matrix4x4.Inverse(renderCamera.worldToCameraMatrix)) * clipPlane;
			renderCamera.projectionMatrix = frameCamera.CalculateObliqueMatrix(clipPlaneCameraSpace);

			otherPortal.meshRenderer.enabled = false;
			UniversalRenderPipeline.RenderSingleCamera(context, renderCamera);
			otherPortal.meshRenderer.enabled = true;
		}
	}
}