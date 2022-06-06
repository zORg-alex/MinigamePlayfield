using System.Linq;
using UnityEngine;

public class AnvilMode : BaseMode {
	public PositionCollection cameraPC;
	public PositionCollection itemPC;
	public float time = 1f;
	public ItemDragController dragController;

	public override void OnEndMode() {
		throw new System.NotImplementedException();
	}

	public override void OnStartMode() {
		float time = 1f;
		MoveCamera.MoveToPoint(cameraPC.GetPoint("AnvilModeMain", out var rotation), rotation);

		var currentItem = GetCurrentItem().gameObject;
		LeanTween.move(currentItem, itemPC.GetPoint("AnvilItemModePoint", out var itemRotation), time);
		LeanTween.rotate(currentItem, itemRotation.eulerAngles, time);

		//turning on QTE
		GameObject.Find("QTE Arc").gameObject.SetActive(true);
	}

	public Item GetCurrentItem() {
		if (dragController.isDragging) {
			var item = dragController.currentItem;
			return item;
		}
		else return null;
	}
}