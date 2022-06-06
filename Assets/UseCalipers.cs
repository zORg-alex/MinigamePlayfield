using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseCalipers : MonoBehaviour
{
    public GameObject bone1;
    public Transform itemPosition;

	[Button]
    public  void OpenAndTightenCalipers() {

        float time = 0.5f;
        float downAngle = 240f;
        float upAngle = 245f;
        LeanTween.rotateZ(bone1, -downAngle, time).setOnComplete(() => { LeanTween.rotateZ(bone1, -upAngle, time); });
    }
}
