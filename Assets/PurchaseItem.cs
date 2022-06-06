using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurchaseItem : MonoBehaviour
{
	public Item itemPrefab;
	public ItemObject[] itemObjects;
	private Vector3 gripPointPosition;

	public void OnClickPurchaseItem()
	{
		Transform dropPoint = GameObject.Find("ItemDropPoint").transform;
		var newItem = Instantiate(itemPrefab, dropPoint.position, Quaternion.identity);
		newItem.ItemObject = itemObjects[Random.Range(0, itemObjects.Length)];
		newItem.SetTexture();
		newItem.gameObject.transform.localScale = new Vector3(1, 1, Random.Range(0.75f, 1.25f));

		gripPointPosition = newItem.transform.GetChild(0).position;
		gripPointPosition = new Vector3(gripPointPosition.x, gripPointPosition.y,
			newItem.gameObject.transform.position.z  * newItem.gameObject.transform.localScale.z);

	}
}
