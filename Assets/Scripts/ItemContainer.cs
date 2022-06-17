using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Item number, add, remove, animation 
public class ItemContainer : MonoBehaviour {

	public Item item;

	[SerializeField]
	int count;
	public int Count => count;

	public Image ItemIcon;
	public Text counter;
	private float temperature;

	public void Push(Item item) {
		if (count == 0) {
			item.gameObject.SetActive(false);
			item.transform.parent = transform;
			item.transform.localPosition = Vector3.zero;
			item.transform.localRotation = Quaternion.identity;
			this.item = item;
			temperature = item.temperature;
			SetIconTexture();
		} else {
			Destroy(item.gameObject);

		}
		count++;
		counter.text = count.ToString();
	}

	public Item Pop() {
		count--;
		counter.text = count.ToString();
		Item newItem;
		if (count == 0) {
			newItem = item;
			item.transform.parent = null;
			item.gameObject.SetActive(true);
			item.temperature = temperature;
			Destroy(gameObject);//Or move to Update, if it's not working
		} else {
			newItem = Instantiate(item);
			newItem.transform.localScale = Vector3.one;
			newItem.gameObject.SetActive(true);
			newItem.temperature = temperature;
		}
		return newItem;
	}
	private void SetIconTexture() {
		ItemIcon.sprite = item.ItemObject.sprite;
	}

}