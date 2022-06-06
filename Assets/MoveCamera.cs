using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
	InputActions input;
	public bool isRotating;
	[SerializeField]
	public float sensetivity = 0.1f;

	static public void MoveToPoint(Vector3 position, Quaternion rotation, float delay = 0f) {
		float time = 1f;
		GameObject camera = GameObject.Find("Main Camera");
		LeanTween.move(camera, position, time).setDelay(delay);
		LeanTween.rotate(camera, rotation.eulerAngles, time).setDelay(delay);
	}

	private void OnEnable() {
		input = new InputActions();
		input.Enable();
		input.UI.RightClick.performed += ClickReleaseed;
		input.UI.RightClick.started += ClickPressed;
	}

	private void OnDisable()
	{
		input.Disable();
		input.UI.RightClick.performed -= ClickReleaseed;
		input.UI.RightClick.started -= ClickPressed;
	}

	private void ClickPressed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
		isRotating = true;
		StartCoroutine(Rotate());
	}
	private void ClickReleaseed(UnityEngine.InputSystem.InputAction.CallbackContext obj) => isRotating = false;

	IEnumerator Rotate() {
		Quaternion rotation = transform.rotation;
		Vector3 euler = transform.eulerAngles;
		do {
            Vector2 mouseDelta = input.Player.Look.ReadValue<Vector2>() * sensetivity;
			euler += new Vector3(-mouseDelta.y, Mathf.Clamp(mouseDelta.x, -90f, 90f)); 
			rotation *= Quaternion.Euler(Mathf.Clamp(-mouseDelta.x, -90f, 90f), -mouseDelta.y, 0);

			transform.eulerAngles = euler;

			yield return new WaitForEndOfFrame();
		} while (isRotating);
		
	}

}
