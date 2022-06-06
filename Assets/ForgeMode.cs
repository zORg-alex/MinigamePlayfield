using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ForgeMode : BaseMode {
	public PositionCollection cameraPC;
	public PositionCollection itemPC;
	public float time = 1f;
	public ItemDragController dragController;
	public GameObject caliperPrefab;
	public Camera mainCamera;

	public override void OnEndMode() {
		throw new System.NotImplementedException();
	}

	public override void OnStartMode() {
		float time = 1f;
		var currentItem = GetCurrentItem().gameObject;
		Quaternion mainCameraItemViewRotation = mainCamera.transform.rotation * Quaternion.Euler(0, 170, 90);
		//moving to look at the mesh
		MoveCamera.MoveToPoint(cameraPC.GetPoint("LookAtMeshPoint", out var lookAtMeshRotation), lookAtMeshRotation);
		LeanTween.move(currentItem, PositionFromCameraspace(mainCamera, new Vector2(Screen.width/2,Screen.height/2), 0.5f), time);
		LeanTween.rotate(currentItem, mainCameraItemViewRotation.eulerAngles, time);
		BellowsBehaviour.PressMeshAnimation();

		//moving to the forge
		MoveCamera.MoveToPoint(cameraPC.GetPoint("ForgeModeMain", out var mainRotation), mainRotation);

		Vector3 caliperSpawnPosition = mainCamera.transform.position;
		caliperSpawnPosition.y -= 0.2f ;
		var caliper = Instantiate(caliperPrefab, caliperSpawnPosition, mainCameraItemViewRotation);
		LeanTween.move(caliper, currentItem.transform.GetChild(0).transform.position, time);

		LeanTween.move(currentItem, itemPC.GetPoint("ForgeItemModePoint", out var itemRotation), time);
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

	public Vector3 PositionFromCameraspace(Camera camera, Vector2 screenPosition, float distFromCamera) =>
		camera.ScreenToWorldPoint((Vector3)screenPosition + Vector3.forward * distFromCamera);
}
