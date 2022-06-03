using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModeController : MonoBehaviour
{
	public static ModeController Instance { get; internal set; }

	public List<BaseMode> registeredModes = new List<BaseMode>();
	public BaseMode currentMode;
	private void OnEnable() {
		// If there is an instance, and it's not me, delete myself.
		if (Instance != null && Instance != this) {
			Destroy(this);
		}
		else {
			Instance = this;
			DontDestroyOnLoad(gameObject);
		}
	}

	private void Start() {
		//currentMode.StartMode(item);

	}
	public void StartMode(BaseMode mode, Item item) {
		if (currentMode && !currentMode.AllowSwitchMode()) return;
		currentMode?.EndMode();
		currentMode = mode;
		currentMode?.StartMode(item);
	}


	//internal void RegisterMode(BaseMode mode) {
	//	registeredModes.Add(mode);
	//}
}
