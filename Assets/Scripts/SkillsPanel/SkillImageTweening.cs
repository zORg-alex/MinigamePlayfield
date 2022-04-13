using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillImageTweening : MonoBehaviour
{
    public Image image;

    public float tweenTime;

    public void Tween() {
        LeanTween.value(gameObject, 0.1f, 1, tweenTime)
            .setOnUpdate((value) => { image.fillAmount = value; } );
	}

}
