using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CustomButton : Selectable {
	public UnityEngine.UI.Image buttonImage;
	public float fillingSpeed = 1f;
    private IEnumerator imageFillingUpCoroutine;
    private IEnumerator imageFillingDownCoroutine;
	public UnityEvent onFilled = new UnityEvent();


    public override void OnPointerEnter(PointerEventData eventData) {
		base.OnPointerEnter(eventData);
	}

	public override void OnPointerExit(PointerEventData eventData) {
		base.OnPointerExit(eventData);
		if (buttonImage.fillAmount != 1 && buttonImage.IsActive()) {
			if (imageFillingUpCoroutine != null)
				StopCoroutine(imageFillingUpCoroutine);
			imageFillingDownCoroutine = ImageFillingDown(buttonImage);
			StartCoroutine(imageFillingDownCoroutine);
		}
	}

	public override void OnPointerDown(PointerEventData eventData) {
		base.OnPointerDown(eventData);
		if (buttonImage.IsActive())
        {
			imageFillingUpCoroutine = ImageFillingUp(buttonImage);
			StartCoroutine(imageFillingUpCoroutine);
		}
	}

	public override void OnPointerUp(PointerEventData eventData) {
		base.OnPointerUp(eventData);
		if (buttonImage.fillAmount != 1) {
			if (imageFillingUpCoroutine != null)
				StopCoroutine(imageFillingUpCoroutine);
			imageFillingDownCoroutine = ImageFillingDown(buttonImage);
			StartCoroutine(imageFillingDownCoroutine);
		}
	}

	IEnumerator ImageFillingUp(UnityEngine.UI.Image imageFill) {
		while (imageFill.fillAmount < 1f) {
			imageFill.fillAmount += fillingSpeed * Time.deltaTime;
			yield return null;
		}
		imageFillingUpCoroutine = null;
		onFilled.Invoke();
	}

	IEnumerator ImageFillingDown(UnityEngine.UI.Image imageFill) {
		while (imageFill.fillAmount > 0f) {
			imageFill.fillAmount -= fillingSpeed * Time.deltaTime;
			yield return null;
		}
		imageFillingDownCoroutine = null;
	}
}
