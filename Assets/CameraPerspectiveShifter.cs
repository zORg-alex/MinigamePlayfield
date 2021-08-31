
using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class CameraPerspectiveShifter : MonoBehaviour {
    Camera cam;
    public float a02;

	void OnEnable() {
        cam = GetComponent<Camera>();
        cam.ResetProjectionMatrix();
        Matrix4x4 p = cam.projectionMatrix;
        p.m02 = a02;
        cam.projectionMatrix = p;
    }

	void Update() {
		cam.ResetProjectionMatrix();
		Matrix4x4 p = cam.projectionMatrix;
		p.m02 = a02;
		cam.projectionMatrix = p;
	}

    public void ViewportSizeChanged(GameObject viewport) {
        var rt = viewport.GetComponent<RectTransform>();
        var canv = viewport.GetComponentInParent<Canvas>();
        var horCenter = (Screen.width / 2 - RectTransformUtility.WorldToScreenPoint(canv != null && canv.renderMode == RenderMode.ScreenSpaceCamera ? canv.worldCamera : null, viewport.transform.position).x) / Screen.width * 2;
        a02 = horCenter;
        Debug.Log(horCenter);
	}
}