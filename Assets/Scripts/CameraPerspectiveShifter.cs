
using UnityEngine;
using System.Collections;
using Sirenix.OdinInspector;
using UnityEngine.Events;

[RequireComponent(typeof(Camera))]
[ExecuteInEditMode]
public class CameraPerspectiveShifter : SerializedMonoBehaviour {
    Camera cam;
    public float a02;
    private float _oldA02;
	private Canvas canvas;
    public ViewportPositionProvider viewport;

    public UnityEvent MatrixChanged;
	public float viewportScreenPosition = 0f;
	[SerializeField]
	public Vector2 screenResolution;
	public bool blockUpdate;

	void OnEnable() {
		cam = GetComponent<Camera>();
		cam.ResetProjectionMatrix();
		Matrix4x4 p = cam.projectionMatrix;
		p.m02 = a02;
		cam.projectionMatrix = p;
		viewportScreenPosition = GetViewportScreenPosition();
		ViewportSizeChanged();
		canvas = FindObjectOfType<Canvas>();
		if (!canvas || canvas == null) Debug.LogError("Canvas is not found");
	}

	void Update() {
		var curViewportScreenPosition = GetViewportScreenPosition();

		if (!blockUpdate && (curViewportScreenPosition != viewportScreenPosition
			|| screenResolution.x != Screen.width || screenResolution.y != Screen.height)) {
			viewportScreenPosition = curViewportScreenPosition;
			ViewportSizeChanged();
		}
        if (_oldA02 != a02) {
			UpdateProjection();
        }
		screenResolution = new Vector2(Screen.width, Screen.height);
    }

	[Button]
	private void UpdateProjection() {
		cam.ResetProjectionMatrix();
		Matrix4x4 p = cam.projectionMatrix;
		p.m02 = a02;
		cam.projectionMatrix = p;
		MatrixChanged?.Invoke();
        _oldA02 = a02;
	}

	private float GetViewportScreenPosition() {
		MatrixChanged?.Invoke();
		return Mathf.Round(1000 * viewport?.horizontalRelative ?? 0f) / 1000;
	}

    public void ViewportSizeChanged() {
        a02 = -viewportScreenPosition;
        UpdateProjection();
    }
}
