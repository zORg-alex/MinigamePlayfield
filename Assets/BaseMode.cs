using UnityEngine;

public abstract class BaseMode : MonoBehaviour {
	/// <summary>
	/// Define initializer
	/// </summary>
	public abstract void OnStartMode();
	/// <summary>
	/// Define deinitializer
	/// </summary>
	public abstract void OnEndMode();
	/// <summary>
	/// Use this to start new mode
	/// </summary>
	public void StartMode() {
		isCurrent = true;
		OnStartMode();
	}
	/// <summary>
	/// Used by controller to end mode
	/// </summary>
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
