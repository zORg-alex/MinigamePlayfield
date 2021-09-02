using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class CameraTextureResizer : MonoBehaviour
{
	public Camera MainCamera;
	public RawImage TargetImage;
	private void OnEnable() {
		SetTextureSize();
	}

	public void SetTextureSize() {
		if (MainCamera != null && TargetImage != null) {
			if (MainCamera.targetTexture != null) {
				MainCamera.targetTexture.Release();
			}

			MainCamera.targetTexture = new RenderTexture((int)TargetImage.rectTransform.rect.width, (int)TargetImage.rectTransform.rect.height, 24);
			TargetImage.texture = MainCamera.targetTexture;
		}
	}
}
