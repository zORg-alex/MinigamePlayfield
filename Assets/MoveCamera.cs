using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
	InputActions input;
	public bool isRotating;
	static public void MoveToPoint(Transform destination) {
		float time = 1f;
		GameObject camera = GameObject.Find("Main Camera");
		LeanTween.move(camera, destination, time);
		LeanTween.rotate(camera, destination.eulerAngles, time);
	}

	private void OnEnable() {
		input = new InputActions();
		input.UI.Enable();
		input.UI.Click.performed += ClickReleaseed;
	}

	private void ClickReleaseed(UnityEngine.InputSystem.InputAction.CallbackContext obj) => isRotating = false;

	private void OnDisable() {
		input.UI.Disable();
		input.UI.Click.performed -= ClickReleaseed;
	}

	IEnumerator Drag() {


		do {
			transform.eulerAngles = PositionFromCameraspace(Camera.main, input.UI.Point.ReadVector2(), WorldDropDistanceFromCamera);

			yield return null;
		} while (isRotating);
		
	}

}
