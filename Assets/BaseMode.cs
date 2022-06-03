using UnityEngine;

public abstract class BaseMode : MonoBehaviour {
	public abstract void OnStartMode(Item item);
	
	public abstract void OnEndMode();

	public void StartMode(Item item) {
		isCurrent = true;
		OnStartMode(item);
	}

	public void EndMode() {
		isCurrent = false;
		OnEndMode();
	}

	public virtual bool AllowSwitchMode() => true;

	public bool isCurrent;

	/*public void Initialize() {
		ModeController.Instance.RegisterMode(this);
	}*/
}
