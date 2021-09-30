using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

//Item number, add, remove, animation 
public class ItemContainer : MonoBehaviour {

	public Item item;

	[SerializeField]
	int count;
	public int Count => count;

	public Image ItemIcon;

	public void Push(Item item) {
		if (count == 0) {
			item.gameObject.SetActive(false);
			item.transform.parent = transform;
			item.transform.localPosition = Vector3.zero;
			item.transform.localRotation = Quaternion.identity;
			this.item = item;
			SetIconTexture();
		} else {
			Destroy(item.gameObject);
		}
		count++;
	}

	public Item Pop() {
		count--;
		Item newItem;
		if (count == 0) {
			newItem = item;
			Destroy(gameObject);//Or move to Update, if it's not working
		} else {
			newItem = Instantiate(item);
			newItem.gameObject.SetActive(true);
		}
		return new Item();
	}
	private void SetIconTexture() {

	}

}