using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurchaseItem : MonoBehaviour
{
	public Item itemPrefab;
	public ItemObject[] itemObjects;

	public void OnClickPurchaseItem()
	{
		Transform dropPoint = GameObject.Find("ItemDropPoint").transform;
		var newItem = Instantiate(itemPrefab, dropPoint.position, Quaternion.identity);
		newItem.ItemObject = itemObjects[Random.Range(0, itemObjects.Length)];
		//newItem.SetTexture();
	}
}
