using UnityEngine;

public class Portal : MonoBehaviour
{
	public Portal otherPortal;
	public Camera renderCamera;
	public MeshRenderer meshRenderer;

	private RenderTexture renderTex;

	private void Awake()
	{
		renderTex = new RenderTexture(Screen.width, Screen.height, 0);
		renderCamera.targetTexture = renderTex;
		renderCamera.enabled = false;
		meshRenderer.material.SetTexture("_MainTex", renderTex);
	}

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
