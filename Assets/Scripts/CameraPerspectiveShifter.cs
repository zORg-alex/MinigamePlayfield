
using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class CameraPerspectiveShifter : MonoBehaviour {
    Camera cam;
    public float a02;
    public GameObject Viewport;

    void OnEnable() {
        ViewportSizeChanged();
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

    public void ViewportSizeChanged() {
        var canv = Viewport.GetComponentInParent<Canvas>();
        var horCenter = (Screen.width / 2 - RectTransformUtility.WorldToScreenPoint(canv != null && canv.renderMode == RenderMode.ScreenSpaceCamera ? canv.worldCamera : null, Viewport.transform.position).x) / Screen.width * 2;
        a02 = horCenter;
        Debug.Log(horCenter);
	}
}