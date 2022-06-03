using UnityEngine;

public class AnvilMode : BaseMode {

	public PositionCollection cameraPC;
	public PositionCollection itemPC;
	public float time = 1f;
	public override void OnEndMode() {
		throw new System.NotImplementedException();
	}

	public override void OnStartMode(Item item) {
		float time = 1f;
		MoveCamera.MoveToPoint(cameraPC.GetPoint("AnvilModeMain", out var rotation), rotation);

		LeanTween.move(item.gameObject, itemPC.GetPoint("AnvilItemModePoint", out var itemRotation), time);
		LeanTween.rotate(item.gameObject, itemRotation.eulerAngles, time);

		//turning on QTE
		GameObject.Find("QTE Arc").gameObject.SetActive(true);
	}
}