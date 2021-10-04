using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddItem : MonoBehaviour
{
	public Item itemPrefab;
	public ItemObject[] itemObjects;

	private void OnGUI() {
		if( GUI.Button(new Rect(20,20, 150,100), "AddItem")) {
			var newItem = Instantiate(itemPrefab, transform.position, Quaternion.identity);
			newItem.ItemObject = itemObjects[Random.Range(0, itemObjects.Length)];
			newItem.SetTexture();
		}
		
	}

}
