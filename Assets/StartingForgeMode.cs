using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StartingForgeMode : MonoBehaviour
{
	public ForgeMode mode;
	private void OnTriggerEnter(Collider other) {
		Debug.Log("Starting to Forge");
		ModeController.Instance.StartMode(mode);

	}

}
