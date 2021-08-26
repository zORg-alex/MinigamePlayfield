using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using Utils;

public class RayCaster : Designs.Singleton<RayCaster> {

	public InputActions input;
	public bool RayCastAll() {
		PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
		eventDataCurrentPosition.position = input.UI.Point.ReadVector2();
		List<RaycastResult> results = new List<RaycastResult>();
		EventSystem.current.RaycastAll(eventDataCurrentPosition, results);

		var rRTs = results.Select(r => r.gameObject.GetComponent<RectTransform>()).Where(r=>r != null);
		rectTransforms = rRTs.ToList();
		MouseOverUI = rRTs.Count() > 0 && !rRTs.Where(go=>go.GetComponent<UIIgnore>() != null).Any();

		var rSIs = results.Select(r => r.gameObject.GetComponent<SceneInteractable>()).Where(r => r != null);
		SceneInteractables = rSIs.ToList();
		MouseOverSI = !MouseOverUI && rSIs.Count() > 0;

		return results.Count > 0;
	}
	private void OnEnable() {
		input = new InputActions();
		input.UI.Enable();
	}
	public bool MouseOverUI;
	public bool MouseOverSI;
	public List<RectTransform> rectTransforms;
	public List<SceneInteractable> SceneInteractables;

	private void Update() {
		RayCastAll();
	}
}