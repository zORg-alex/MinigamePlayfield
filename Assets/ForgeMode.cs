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
	public Transform caliperRestPoint;
	public Transform caliperWorkPoint;
	public BellowsBehaviour bellow;
	public RectTransform UIPanel;
	public InventoryController ic;

	private Item currentItem;

	public override void OnEndMode() {
		bellow.IsWorking = false;
		UIPanel.gameObject.SetActive(false);
		StopAllCoroutines();

		LeanTween.move(calipers.gameObject, caliperRestPoint, time);
		LeanTween.rotate(calipers.gameObject, caliperRestPoint.rotation.eulerAngles, time);
		//currentItem.transform.SetParent();
		LeanTween.move(currentItem.gameObject, new Vector3( Camera.main.transform.position.x , Camera.main.transform.position.y - .15f, Camera.main.transform.position.z), time + 1.1f)
			.setOnComplete(() => {
				ic.Push(currentItem);
				currentItem = null;
			});
	}

	public override void OnStartMode() {
		float time = 1.5f;
		 currentItem = GetCurrentItem();
		//moving to camera forge
		MoveCamera.MoveToPoint(cameraPC.GetPoint("ForgeModeMain", out var lookAtMeshRotation), lookAtMeshRotation, time);
		//moving calipers into work place
		LeanTween.move(calipers.gameObject, caliperWorkPoint, time);
		LeanTween.rotate(calipers.gameObject, caliperWorkPoint.rotation.eulerAngles, time)
			.setOnComplete(() => {
				calipers.OpenAndTightenCalipers();
				bellow.IsWorking = true;
			});

		//moving item into work place
		LeanTween.move(currentItem.gameObject, calipers.itemPosition , time + .1f)
			.setOnComplete(() => {
				currentItem.transform.SetParent(calipers.itemPosition);
				currentItem.gameObject.GetComponent<Rigidbody>().isKinematic = true;
				currentItem.isHeating = true;
				StartCoroutine(ItemHeating());
			});

		//turning on QTE

		UIPanel.gameObject.SetActive(true);
	}

	IEnumerator ItemHeating() {
		yield return new WaitForSeconds(1.5f);
		Debug.Log("Starting to heat the item");
		Vector3 calipersPosition = calipers.transform.position;
		float startPoint = calipersPosition.z;
		float endPoint = startPoint + 0.22f;
		float movingCalipersSpeed = 0.001f;
		

		
		while (currentItem.temperature < 1500) {
			if (calipers.transform.position.z > endPoint || calipers.transform.position.z < startPoint)
				movingCalipersSpeed = -movingCalipersSpeed;
			calipers.transform.position = new Vector3(calipersPosition.x, calipersPosition.y, calipers.transform.position.z + movingCalipersSpeed);

			yield return null;
		}
		ModeController.Instance.StopMode();

	}

	private void Update() {
		if (Input.GetKeyUp(KeyCode.Escape)) {
			Debug.Log("Escape key was released");
			ModeController.Instance.StopMode();
		}
	}


	public Item GetCurrentItem() {
		if (dragController.isDragging) {
			var item = dragController.currentItem;
			return item;
		}
		else return null;
	}

	public void OnTriggerEnterForgeMode((Collider @this, Collider other) e) {
		if (!GetCurrentItem() || GetCurrentItem().temperature > 1500) return;
		ModeController.Instance.StartMode(this);
	}
}
