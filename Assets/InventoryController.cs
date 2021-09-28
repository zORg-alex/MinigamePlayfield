using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class InventoryController : MonoBehaviour
{
	public void Push(Item item) {
		var containers = GetComponentsInChildren<ItemContainer>();
		if (containers.FirstOrDefault(c => c.item.ItemObject == item.ItemObject) is ItemContainer container)
			container.Push(item);
		else {
			//create new one
		}
	}
}
