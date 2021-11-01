using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class ViewportPositionProvider : MonoBehaviour
{
	private Canvas canvas;

	public Vector3 positionInPixels { get; private set; }

	public float horizontalRelative => positionInPixels.x / canvas.pixelRect.width * 2;
	public float verticalRelative => positionInPixels.y / canvas.pixelRect.height * 2;
	private void OnEnable() {
		canvas = FindObjectOfType<Canvas>();
	}
	// Update is called once per frame
	void Update()
    {
		positionInPixels = -RectTransformUtility.CalculateRelativeRectTransformBounds(transform, canvas.transform).center;
    }
}