using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InventoryController : Designs.Singleton<InventoryController>
{
	public void OnClick() {
		var ItemContainer = RayCaster.Instance.rectTransforms.First(rt => rt.GetComponent<ItemContainer>());
		if (ItemContainer == null) return;
		Debug.Log("Item Clicked");
	}
	//TODO take and drag (coroutings) item, 
}
