using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Sirenix;
using Sirenix.OdinInspector;

public class InventoryController : MonoBehaviour
{
	[AssetsOnly]
	public ItemContainer ItemContainerPrefab;
	[ChildGameObjectsOnly]
	public Transform ItemRoot;
	public void Push(Item item) {
		var containers = GetComponentsInChildren<ItemContainer>();
		ItemContainer container = containers.FirstOrDefault(c => c.item.ItemObject == item.ItemObject);
		if (container is null) {
			container = Instantiate(ItemContainerPrefab);
			container.transform.parent = ItemRoot;
			container.transform.localScale = Vector3.one;
			container.transform.localPosition = Vector3.zero;
		}
		container.Push(item);
	}
}
