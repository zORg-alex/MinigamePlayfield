using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Utils;

public class InventoryController : Designs.Singleton<InventoryController>
{
	InputActions input;
	public Canvas canvas;
	public float WorldDropDistanceFromCamera = .7f;
	//public float UIDistanceFromCamera = .5f;
	private void OnEnable() {
		input = new InputActions();
		input.UI.Enable();
		input.UI.Click.performed += ClickReleaseed;
		canvas = FindObjectOfType<Canvas>();
	}

	private void ClickReleaseed(UnityEngine.InputSystem.InputAction.CallbackContext obj) => isDragging = false;

	private void OnDisable() {
		input.UI.Disable();
		input.UI.Click.performed -= ClickReleaseed;
	}


	public bool isDragging;
	public void OnUIClick() {
		var ItemContainer = RayCaster.Instance.rectTransforms
			.FirstOrDefault(rt => rt.GetComponent<ItemContainer>())
			?.GetComponent<ItemContainer>();
		if (ItemContainer != null) {
			isDragging = true;
			var item = ItemContainer.Pop();
			StartCoroutine(Drag(item));
			Debug.Log("ItemContainer Clicked");
			return;
		} 
	}
	public void OnItemClick() {
		var item = RayCaster.Instance.SceneTransforms
			.FirstOrDefault(i=>i.GetComponent<Item>())
			.GetComponent<Item>();
		if (item != null) {
			isDragging = true;
			StartCoroutine(Drag(item));
			Debug.Log("Item Clicked");
			return;
		}
	}
	//TODO take and drag (coroutings) item, 
	IEnumerator Drag(Item item) {
		var rb = item.GetComponent<Rigidbody>();
		rb.isKinematic = true;
		rb.angularVelocity = Vector3.zero;
		rb.velocity = Vector3.zero;
		item.DrawOverUI();

		do {
			item.transform.position = PositionFromCameraspace(Camera.main, input.UI.Point.ReadVector2(), WorldDropDistanceFromCamera);

			yield return null;
		} while (isDragging);
		item.NormalDrawOrder();

		//item.transform.position = PositionFromCameraspace(Camera.main, input.UI.Point.ReadVector2(), WorldDropDistanceFromCamera);
		rb.isKinematic = false;
	}

	public Vector3 PositionFromCameraspace(Camera camera, Vector2 screenPosition, float distFromCamera) =>
		camera.ScreenToWorldPoint((Vector3)screenPosition + Vector3.forward * distFromCamera);
}
