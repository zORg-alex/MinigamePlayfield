using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Utils;

public class InventoryController : Designs.Singleton<InventoryController>
{
	InputActions input;
	public Collider ItemDragPlane;
	private void OnEnable() {
		input = new InputActions();
		input.UI.Enable();
	}
	private void OnDisable() {
		input.UI.Disable();
	}


	public bool isDragging;
	public void OnUIClick() {
		var ItemContainer = RayCaster.Instance.rectTransforms
			.FirstOrDefault(rt => rt.GetComponent<ItemContainer>())
			.GetComponent<ItemContainer>();
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
		item.gameObject.SetActive(true);
		var rb = item.GetComponent<Rigidbody>();
		rb.isKinematic = true;
		rb.angularVelocity = Vector3.zero;
		rb.velocity = Vector3.zero;

		do {
			//Move over UI when it's there...
			ItemDragPlane.Raycast(Camera.main.ScreenPointToRay(input.UI.Point.ReadVector2()), out var hit, 10f);
			item.transform.position = hit.point;

			yield return null;
			isDragging = input.UI.Click.ReadValue<float>() > 0;
		} while (isDragging);
		//If over Inventory, push
		rb.isKinematic = false;
	}
}
