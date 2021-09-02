using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[ExecuteInEditMode]
public class ScreeSizeChangedEvent : MonoBehaviour
{
	public delegate void ScreeSizeChangedHandler(Vector2 oldResolution);
	/// <summary>
	/// Passes old size for computations
	/// </summary>
	public event ScreeSizeChangedHandler ScreenSizeChanged;
	/// <summary>
	/// Passes old size for computations
	/// </summary>
	public UnityEvent<Vector2> ScreenSizeChangedWithOldSize_;
	public UnityEvent ScreenSizeChanged_;
	public Vector2Int oldSize;
	public void OnEnable() {
		SaveOldSize();
	}

	private void SaveOldSize() {
		oldSize = new Vector2Int((int)Screen.width, (int)Screen.height);
	}

	// Update is called once per frame
	void Update()
    {
		if (Screen.width != oldSize.x || Screen.height != oldSize.y) {
			ScreenSizeChanged?.Invoke(oldSize);
			ScreenSizeChangedWithOldSize_?.Invoke(oldSize);
			ScreenSizeChanged_?.Invoke();
			SaveOldSize();
		}
    }
}
