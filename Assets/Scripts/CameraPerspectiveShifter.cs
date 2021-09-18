
using UnityEngine;
using System.Collections;
using Sirenix.OdinInspector;
using UnityEngine.Events;

[RequireComponent(typeof(Camera))]
[ExecuteInEditMode]
public class CameraPerspectiveShifter : MonoBehaviour {
    Camera cam;
    public float a02;
    private float _oldA02;
    public GameObject Viewport;
    public UnityEvent MatrixChanged;

    void OnEnable() {
        cam = GetComponent<Camera>();
        cam.ResetProjectionMatrix();
        Matrix4x4 p = cam.projectionMatrix;
        p.m02 = a02;
        cam.projectionMatrix = p;
        ViewportSizeChanged();
    }
    public Vector3 zzz;
	void Update() {
        if (_oldA02 != a02) {
			UpdateProjection();
        }
        var canv = Viewport.GetComponentInParent<Canvas>();
        var vrt = Viewport.GetComponent<RectTransform>();
	}

	private void UpdateProjection() {
		cam.ResetProjectionMatrix();
		Matrix4x4 p = cam.projectionMatrix;
		p.m02 = a02;
		cam.projectionMatrix = p;
		MatrixChanged?.Invoke();
        _oldA02 = a02;
	}

	[Button]
    public void ViewportSizeChanged() {
        a02 = cam.WorldToViewportPoint(Viewport.transform.position).x / 2;
        UpdateProjection();
    }
}