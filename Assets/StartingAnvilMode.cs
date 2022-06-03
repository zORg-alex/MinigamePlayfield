using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StartingAnvilMode : MonoBehaviour
{
	public AnvilMode mode;
	private void OnTriggerEnter(Collider other) {
		Debug.Log("Starting to use anvil");
		ModeController.Instance.StartMode(mode, GetCurrentItem());

	}

	public Item GetCurrentItem() {
		var item = RayCaster.Instance.SceneTransforms
			.FirstOrDefault(i => i.GetComponent<Item>())?
			.GetComponent<Item>();
		if (item != null) {
			return item;
		}
		return null;
	}
}
