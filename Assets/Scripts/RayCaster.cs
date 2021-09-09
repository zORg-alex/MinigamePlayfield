using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
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
		MouseOverUI = rRTs.Count() > 0 && !rRTs.All(go => go.GetComponent<UIIgnore>() != null);

		var hits = Physics.RaycastAll(Camera.main.ScreenPointToRay(input.UI.Point.ReadVector2()));
		if (hits.Count() > 0) {
			SceneTransforms = hits.Select(h=>h.transform).ToList();
			MouseOverSceneTransform = !MouseOverUI;
		} else
			MouseOverSceneTransform = false;

		return results.Count > 0;
	}
	private void OnEnable() {
		input = new InputActions();
		input.UI.Enable();
		input.UI.Click.started += OnClick; 
	}

	public UnityEvent OnUIClick = new UnityEvent(); 
	public UnityEvent OnSTClick = new UnityEvent();

	private void OnClick(UnityEngine.InputSystem.InputAction.CallbackContext obj) {
		if (RayCastAll()) {
			if (MouseOverUI) OnUIClick.Invoke();
			else if (MouseOverSceneTransform) OnSTClick.Invoke();
		}
	}


	public bool MouseOverUI;
	public bool MouseOverSceneTransform;
	public List<RectTransform> rectTransforms;
	public List<Transform> SceneTransforms;

}