using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Utils;

[SelectionBase]
[ExecuteInEditMode]
public class QTEArcController : MonoBehaviour
{
	public enum Modes { StaticZones }
	public Modes mode = Modes.StaticZones;

	public GameObject cursor;
	public UIArcBar[] bars;
	public QTEArcZoneScript[] zones;

	public float cursorSpeed = 1f;

	public float realArcAngle = 90f;
	public float visibleArcAngle = 80f;
	public float curInitialPosition = 0f;

	private float _curPosition;
	public float curPosition {
		get => _curPosition;
		set {
			_curPosition = value;
			cursor.transform.eulerAngles = Vector3.forward * (value - visibleArcAngle / 2);
		}
	}
	public bool curInitialDirectionPositive;
	public bool curDirectionPositive;
	public bool active;

	private InputActions input;

	private void OnEnable() {
		input = new InputActions();
		input.QTE.Enable();
		if (Application.isPlaying) {
			cursor.SetActive(active);
		}
		if (bars[0] != null) {
			cursor.transform.position = (bars[0].GetWorldSpaceOrigin());
		}
		curPosition = curInitialPosition;
		curDirectionPositive = curInitialDirectionPositive;
	}
	private void OnDisable() {
		input.QTE.Disable();
	}

	private void Update() {
#if UNITY_EDITOR
		if (!Application.isPlaying && curPosition != curInitialPosition) {
			curPosition = curInitialPosition;
		}
#endif
		if (active && Application.isPlaying) {
			//Move cursor
			curPosition += cursorSpeed * Time.deltaTime * (curDirectionPositive ? 1 : -1);
			//Fix overshoot and redirect
			if (curPosition < 0) {
				curPosition = -curPosition;
				curDirectionPositive = !curDirectionPositive;
			}
			 else if (curPosition > visibleArcAngle) {
				curPosition = visibleArcAngle * 2 - curPosition;
				curDirectionPositive = !curDirectionPositive;
			}
			foreach (var z in zones) {
				z.SetCurrentPos(curPosition);
			}
		}
		if (cursor.activeSelf != active && Application.isPlaying) {
			curPosition = curInitialPosition;
			curDirectionPositive = curInitialDirectionPositive;
		}
	}

	//private void OnDrawGizmos() {
	//	Gizmos.color = Color.red.MultiplyAlpha(.3);
	//	Gizmos.DrawLine(transform.TransformPoint(Vector3.right * 10f), transform.TransformPoint(Vector3.right * -10f));
	//	Gizmos.DrawLine(transform.TransformPoint(Vector3.up * 10f), transform.TransformPoint(Vector3.up * - 10f));
	//	Gizmos.color = Color.green.MultiplyAlpha(.3);
	//	Gizmos.DrawLine(cursor.transform.TransformPoint(Vector3.right * 10f), cursor.transform.TransformPoint(Vector3.right * -10f));
	//	Gizmos.DrawLine(cursor.transform.TransformPoint(Vector3.up * 10f), cursor.transform.TransformPoint(Vector3.up * -10f));
	//	var pos = (Vector3)bars[0].GetWorldSpaceOrigin();
	//	Gizmos.color = Color.magenta.MultiplyAlpha(.3);
	//	Gizmos.DrawLine(pos + transform.TransformVector(Vector3.right * 10f), pos + transform.TransformVector(Vector3.right * -10f));
	//	Gizmos.DrawLine(pos + transform.TransformVector(Vector3.up * 10f), pos + transform.TransformVector(Vector3.up * -10f));
	//}
}
