using UnityEngine;

public abstract class BaseMode : MonoBehaviour {
	public abstract void OnStartMode();
	
	public abstract void OnEndMode();

	public void StartMode() {
		isCurrent = true;
		OnStartMode();
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
