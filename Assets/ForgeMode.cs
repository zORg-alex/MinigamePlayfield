using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForgeMode : BaseMode {
	public PositionCollection cameraPC;
	public PositionCollection itemPC;
	public float time = 1f;

	private ForgeMode mode;
	public override void OnEndMode() {
		throw new System.NotImplementedException();
	}

	public override void OnStartMode(Item item) {
		float time = 1f;
		MoveCamera.MoveToPoint(cameraPC.GetPoint("ForgeModeMain", out var rotation), rotation);

		LeanTween.move(item.gameObject, itemPC.GetPoint("ForgeItemModePoint", out var itemRotation), time);
		LeanTween.rotate(item.gameObject, itemRotation.eulerAngles, time);
		
		//turning on QTE
		GameObject.Find("QTE Arc").gameObject.SetActive(true);

		PressMesh.PressMeshAnimation();
	}
}
