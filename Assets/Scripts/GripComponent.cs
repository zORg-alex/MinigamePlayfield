using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using Utils;

[ExecuteInEditMode]
public class GripComponent : MonoBehaviour {
	[HideInInspector]
	public Camera UICamera;
	private Vector2 oldAnchorMin;
	private Vector2 oldAnchorMax;
	[HideInInspector]
	public List<RectTransform> LeftBuddies = new List<RectTransform>();
	[HideInInspector]
	public List<RectTransform> RightBuddies = new List<RectTransform>();
	[HideInInspector]
	public List<RectTransform> TopBuddies = new List<RectTransform>();
	[HideInInspector]
	public List<RectTransform> BottomBuddies = new List<RectTransform>();

	public enum GripMode { Vertical = 0, Horizontal = 1 }
	[HideInInspector]
	public GripMode Mode;
	private InputActions input;
	private bool clickedInside;
	private bool inside;
	[HideInInspector]
	public bool Initiated;

	RectTransform rectTransform => (RectTransform)transform;

	public UnityEvent GripClicked = new UnityEvent();

	public void OnEnable() {
		input = new InputActions();
		input.UI.Enable();
		input.UI.Click.performed += Click_performed;
		var canv = FindObjectOfType<Canvas>();
		UICamera = canv.worldCamera;
	}

	public void OnDisable() {
		input.UI.Click.performed -= Click_performed;
	}

	private void Click_performed(InputAction.CallbackContext ctx) {
		clickedInside = inside && ctx.ReadValue<float>() > .5f;
	}

	private void Update() {
		if (rectTransform.anchorMin != oldAnchorMin || rectTransform.anchorMax != oldAnchorMax) {
			if (!Initiated || Application.isEditor)
				Reinit();
		}

		var mousePos = input.UI.Point.ReadValue<Vector2>();
		var relPos = mousePos.Divide(new Vector2(Screen.width, Screen.height));
		if (clickedInside || RectTransformUtility.RectangleContainsScreenPoint(rectTransform, mousePos, UICamera)) {
			if (!inside) {
				CursorUtils.SetCursor(Mode == GripMode.Horizontal ? CursorUtils.CursorType.ArrowsWE : CursorUtils.CursorType.ArrowsNS);
				inside = true;
			}
		} else {
			if (inside) {
				CursorUtils.ResetCursor();
				inside = false;
			}
		}
		if (clickedInside) {
			if (Mode == GripMode.Horizontal) {
				foreach (var left in LeftBuddies) {
					left.anchorMax = new Vector2(relPos.x, left.anchorMax.y);
				}
				foreach (var right in RightBuddies) {
					right.anchorMin = new Vector2(relPos.x, right.anchorMin.y);
				}
				//LeftBuddy.anchorMax = new Vector2(relPos.x, LeftBuddy.anchorMax.y);
				//RightBuddy.anchorMin = new Vector2(relPos.x, RightBuddy.anchorMin.y);
				rectTransform.anchorMin = new Vector2(relPos.x, rectTransform.anchorMin.y);
				rectTransform.anchorMax = new Vector2(relPos.x, rectTransform.anchorMax.y);
			} else {
				foreach (var top in TopBuddies) {
					top.anchorMin = new Vector2(top.anchorMin.x, relPos.y);
				}
				foreach (var bottom in BottomBuddies) {
					bottom.anchorMax = new Vector2(bottom.anchorMax.x, relPos.y);
				}
				//TopBuddy.anchorMin = new Vector2(TopBuddy.anchorMin.x, relPos.y);
				//BottomBuddy.anchorMax = new Vector2(BottomBuddy.anchorMax.x, relPos.y);
				rectTransform.anchorMin = new Vector2(rectTransform.anchorMin.x, relPos.y);
				rectTransform.anchorMax = new Vector2(rectTransform.anchorMax.x, relPos.y);
			}
			GripClicked?.Invoke();
		}
	}

	private void Reinit() {
		var anchorArea = rectTransform.anchorMax - rectTransform.anchorMin;
		if (anchorArea.x > anchorArea.y) {
			LookForVerticalNeighbours();
			Mode = GripMode.Vertical;
		} else {
			LookForHorizontalNeighbours();
			Mode = GripMode.Horizontal;
		}

		oldAnchorMin = rectTransform.anchorMin;
		oldAnchorMax = rectTransform.anchorMax;

		Initiated = true;
	}

	private void LookForHorizontalNeighbours() {
		ClearAll();
		foreach (var rt in transform.parent.GetComponentsInChildren<RectTransform>()) {
			if (rt != rectTransform) {
				if (rt.anchorMax.x == rectTransform.anchorMin.x && VerticalAnchorBetween(rt))
					LeftBuddies.Add(rt);
				if (rt.anchorMin.x == rectTransform.anchorMax.x && VerticalAnchorBetween(rt))
					RightBuddies.Add(rt);
			}
		}
	}

	private void LookForVerticalNeighbours() {
		ClearAll();
		foreach (var rt in transform.parent.GetComponentsInChildren<RectTransform>()) {
			if (rt != rectTransform) {
				if (rt.anchorMax.y == rectTransform.anchorMin.y && HorizontalAnchorBetween(rt))
					BottomBuddies.Add(rt);
				if (rt.anchorMin.y == rectTransform.anchorMax.y && HorizontalAnchorBetween(rt))
					TopBuddies.Add(rt);
			}
		}
	}

	private void ClearAll() {
		LeftBuddies.Clear();
		RightBuddies.Clear();
		BottomBuddies.Clear();
		TopBuddies.Clear();
	}

	private bool VerticalAnchorBetween(RectTransform rt) =>
		rt.anchorMax.y <= rectTransform.anchorMax.y && rt.anchorMin.y >= rectTransform.anchorMin.y;

	private bool HorizontalAnchorBetween(RectTransform rt) =>
		rt.anchorMax.x <= rectTransform.anchorMax.x && rt.anchorMin.x >= rectTransform.anchorMin.x;
}
