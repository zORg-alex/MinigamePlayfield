using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(Canvas))]
public class CameraMatrixOffsetFix : MonoBehaviour
{
	private Canvas canvas;
	private RectTransform crt;

	public float Distance = 0.5f;

	private void OnEnable() {
		canvas = GetComponent<Canvas>();
		crt = canvas.GetComponent<RectTransform>();
		FitCamera();
	}

	[Button]
	public void FitCamera() {
		var camFoV = canvas.worldCamera.fieldOfView;
		var vertSize = Mathf.Tan((camFoV / 2).Deg2Rad()) * 2 * Distance;
		canvas.transform.position = canvas.worldCamera.transform.position + canvas.worldCamera.transform.forward * Distance;
		canvas.transform.rotation = canvas.worldCamera.transform.rotation;
		canvas.transform.localScale = Vector3.one * vertSize / Screen.height;
		var offset = canvas.worldCamera.projectionMatrix[0, 2];
		crt.pivot = new Vector2(.5f - offset / 2, .5f);
		crt.sizeDelta = new Vector3(Screen.width, Screen.height, 1);
	}
}
