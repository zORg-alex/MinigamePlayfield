using System.Collections;
using System.Linq;
using UnityEngine;

public class AnvilMode : BaseMode {
	public PositionCollection cameraPC;
	public PositionCollection itemPC;
	public float time = 2f;
	public ItemDragController dragController;
	public UseCalipers calipers;
	public GameObject hammer;
	public Transform caliperRestPoint;
	public Transform caliperWorkPoint;
	public Transform caliperTopBucket;
	public Transform caliperBottomBucket; 
	public Transform hammerRestPoint;
	public Transform hammerWorkTopPoint; 
	public Transform hammerWorkBottomPoint;
	public RectTransform UIPanel;
	public InventoryController ic;
	public Item itemPrefab;
	public ItemObject[] blades;
	public int hitCount = 10;
	private float hitTime = 0.5f;

	private Item currentItem;

	public override void OnEndMode() {
		UIPanel.gameObject.SetActive(false);
		StopAllCoroutines();
		//moving hammer back
		LeanTween.move(hammer, hammerRestPoint, time);
		LeanTween.rotate(hammer, hammerRestPoint.rotation.eulerAngles, time);
		//moving camera in position
		LeanTween.rotate(Camera.main.gameObject, new Vector3(4.92f, 16.44f, 0), time);
		//moving calipers to the bucket
		LeanTween.move(calipers.gameObject, caliperTopBucket, time);		
		LeanTween.rotate(calipers.gameObject, caliperTopBucket.rotation.eulerAngles, time).setOnComplete(() => {
			currentItem.ItemObject.temperature = 25;
			LeanTween.move(calipers.gameObject, caliperBottomBucket, time).setLoopPingPong(1);
			LeanTween.rotate(calipers.gameObject, caliperBottomBucket.rotation.eulerAngles, time).setLoopPingPong(1).setOnComplete(() => {
				LeanTween.move(currentItem.gameObject, new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y - .15f, Camera.main.transform.position.z), time + 1.1f)
				.setOnComplete(() => {
					ic.Push(currentItem);
					currentItem = null;
				});
			
			
			LeanTween.move(calipers.gameObject, caliperRestPoint, time).setDelay(time + .1f);
			LeanTween.rotate(calipers.gameObject, caliperRestPoint.rotation.eulerAngles, time).setDelay(time + .1f);
			});
		});


	}

	public override void OnStartMode() {
		hitCount = 10;
		currentItem = GetCurrentItem();
		//moving to camera forge
		MoveCamera.MoveToPoint(cameraPC.GetPoint("AnvilModeMain", out var lookAtMeshRotation), lookAtMeshRotation, time);
		//moving calipers into work place
		LeanTween.move(calipers.gameObject, caliperWorkPoint, time);
		LeanTween.rotate(calipers.gameObject, caliperWorkPoint.rotation.eulerAngles, time)
			.setOnComplete(() => {
				calipers.OpenAndTightenCalipers();
			});

		LeanTween.move(hammer, hammerWorkTopPoint, time);
		LeanTween.rotate(hammer, hammerWorkTopPoint.rotation.eulerAngles, time);

		//moving item into work place
		LeanTween.move(currentItem.gameObject, calipers.itemPosition, time + .1f)
			.setOnComplete(() => {
				currentItem.transform.SetParent(calipers.itemPosition);
				currentItem.gameObject.GetComponent<Rigidbody>().isKinematic = true;
				StartCoroutine(ForgingItem());
				StartCoroutine(HammerHitting());
			}); 
		LeanTween.rotate(currentItem.gameObject, Vector3.zero, time + .1f);

		//turning on QTE

		UIPanel.gameObject.SetActive(true);
	}


	IEnumerator ForgingItem() {
		yield return new WaitForSeconds(1.3f);
		Debug.Log("Starting to heat the item");
		Vector3 calipersPosition = calipers.transform.position;
		float startPoint = calipersPosition.z;
		float endPoint = startPoint + 0.08f;
		float movingCalipersSpeed = 0.0001f;
		


		while (hitCount > 0) {
			if (calipers.transform.position.z > endPoint || calipers.transform.position.z < startPoint)
				movingCalipersSpeed = -movingCalipersSpeed;
			calipers.transform.position = new Vector3(calipersPosition.x, calipersPosition.y, calipers.transform.position.z + movingCalipersSpeed);

			
			yield return null;
		}
		if (hitCount == 0 && currentItem.temperature >800) {

			float currentTemperature = currentItem.temperature;
			currentItem.temperature =  10000;
				currentItem.ItemObject = blades[Random.Range(0, blades.Length)];
				currentItem.transform.eulerAngles = new Vector3(currentItem.transform.rotation.x - 90, currentItem.transform.rotation.y, currentItem.transform.rotation.z);
				currentItem.SetTexture();
			currentItem.temperature = currentTemperature;

		}

	}

	IEnumerator HammerHitting() {
		yield return new WaitForSeconds(1.5f);
		
		while (hitCount > 0) {
			LeanTween.move(hammer, hammerWorkBottomPoint, hitTime).setLoopPingPong(1);
			LeanTween.rotate(hammer, hammerWorkBottomPoint.rotation.eulerAngles, hitTime).setLoopPingPong(1);
			hitCount--;
			yield return new WaitForSeconds(hitTime * 3 );
		}
		ModeController.Instance.StopMode();

	}

	public Item GetCurrentItem() {
		if (dragController.isDragging) {
			var item = dragController.currentItem;
			return item;
		}
		else return null;
	}

	private void Update() {
		if (Input.GetKeyUp(KeyCode.Escape)) {
			Debug.Log("Escape key was released");
			ModeController.Instance.StopMode();
		}
	}

	public void OnTriggerEnterAnvilMode((Collider @this, Collider other) e) {
		if (!GetCurrentItem()) return;
		ModeController.Instance.StartMode(this);
	}
}