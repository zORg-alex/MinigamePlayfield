using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ForgeMode : BaseMode {
	public PositionCollection cameraPC;
	public PositionCollection itemPC;
	public float time = 1f;
	public ItemDragController dragController;
	public UseCalipers calipers;
	public Camera mainCamera;
	public Transform caliperRestPoint;
	public Transform caliperWorkPoint;
	public BellowsBehaviour bellow;

	public override void OnEndMode() {
		throw new System.NotImplementedException();
	}

	public override void OnStartMode() {
		float time = 1f;
		var currentItem = GetCurrentItem().gameObject;
		Quaternion mainCameraItemViewRotation = mainCamera.transform.rotation * Quaternion.Euler(0, 170, 90);
		//moving to forge
		MoveCamera.MoveToPoint(cameraPC.GetPoint("ForgeModeMain", out var lookAtMeshRotation), lookAtMeshRotation);
		LeanTween.move(calipers.gameObject, caliperWorkPoint, time);
		calipers.OpenAndTightenCalipers();
		LeanTween.move(currentItem, calipers.itemPosition, time + .1f)
			.setOnComplete( () => currentItem.transform.SetParent(calipers.itemPosition) );
		
		//look at bellows
		//MoveCamera.MoveToPoint(cameraPC.GetPoint("LookAtBellowsPoint", out var mainRotation), mainRotation, time + .3f);
		bellow.IsWorking = true;

		//look at forge 
		//MoveCamera.MoveToPoint(cameraPC.GetPoint("ForgeModeMain", out var lookAtMeshRotation), lookAtMeshRotation, time + time);
		//turning on QTE
		

		
	}

	public Item GetCurrentItem() {
		if (dragController.isDragging) {
			var item = dragController.currentItem;
			return item;
		}
		else return null;
	}

	public Vector3 PositionFromCameraspace(Camera camera, Vector2 screenPosition, float distFromCamera) =>
		camera.ScreenToWorldPoint((Vector3)screenPosition + Vector3.forward * distFromCamera);

	public void OnTriggerEnter((Collider @this, Collider other) e) {
		//TODO check if other is expected item
		if (!GetCurrentItem()) return;
		StartMode();
	}
}
