using UnityEngine;

public class MainMode : BaseMode {
	public RectTransform UIPanel;

	public override void OnEndMode() {
		UIPanel.gameObject.SetActive(false);
	}

	public override void OnStartMode() {
		UIPanel.gameObject.SetActive(true);
	}
}