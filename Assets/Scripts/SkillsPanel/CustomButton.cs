using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class CustomButton : Selectable {
	public UnityEngine.UI.Image image;
	[Range(0f,1f)] public float changeFilling = 0.01f;
	//public CoroutingsAndProps coroutineScript;


	public override void OnPointerEnter(PointerEventData eventData) {
		base.OnPointerEnter(eventData);
		Debug.Log("OnPointerEnter");
	}

	public override void OnPointerExit(PointerEventData eventData) {
		base.OnPointerExit(eventData);
		if (image.fillAmount != 1) {
			StopCoroutine("ImageFillingUp");
			StartCoroutine("ImageFillingDown", image);
		}
		Debug.Log("OnPointerExit");
	}

	public override void OnPointerDown(PointerEventData eventData) {
		base.OnPointerDown(eventData);
		StartCoroutine("ImageFillingUp", image);

		Debug.Log("OnPointerDown");
	}

	public override void OnPointerUp(PointerEventData eventData) {
		base.OnPointerUp(eventData);
		if (image.fillAmount != 1) {
			StopCoroutine("ImageFillingUp");
			StartCoroutine("ImageFillingDown", image);
		}
		Debug.Log("OnPointerUp");
	}

	IEnumerator ImageFillingUp(UnityEngine.UI.Image imageFill) {
		while (imageFill.fillAmount < 1f) {
			imageFill.fillAmount += changeFilling;
			Debug.Log("Filling UP");
			yield return null;
		}
	}

	IEnumerator ImageFillingDown(UnityEngine.UI.Image imageFill) {
		while (imageFill.fillAmount > 0f) {
			imageFill.fillAmount -= changeFilling;
			Debug.Log("Filling DOWN");
			yield return null;
		}
	}
}
