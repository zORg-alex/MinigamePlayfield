public class ForgeQTEController : QTEArcController {

	public QTEArcZoneScript RightZone;
	public QTEArcZoneScript LeftZone;
	private new void OnEnable() {
		base.OnEnable();
		input.QTE.Click.started += RightZoneInput;
		input.QTE.ClickAlt.started += LeftZoneInput;
	}

	private void LeftZoneInput(UnityEngine.InputSystem.InputAction.CallbackContext obj) {
		if (LeftZone.isOver) {
			curDirectionPositive = !curDirectionPositive;
		}
	}

	private void RightZoneInput(UnityEngine.InputSystem.InputAction.CallbackContext obj) {
		if (RightZone.isOver) {
			curDirectionPositive = !curDirectionPositive;
		}
	}

	private new void OnDisable() {
		base.OnDisable();
	}
}
