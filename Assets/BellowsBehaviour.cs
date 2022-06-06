using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
#if UNITY_EDITOR
using UnityEditor;
#endif

[RequireComponent(typeof(Animator))]

public class BellowsBehaviour : MonoBehaviour
{
	public float maxSpeed = 1f;
	private Animator animator;
	[SerializeField, HideInInspector]
	private bool _isWorking;

	public bool isWorking { get => _isWorking; set => SetIsWorking(value); }

	private void SetIsWorking(bool value) {
		LeanTween.value(gameObject, animator.GetCurrentAnimatorStateInfo(0).speed, _isWorking ? 0 : maxSpeed, 0.1f);
	}

	private void OnEnable() {
		animator = GetComponent<Animator>();
	}

	[Button]    
    public static void PressMeshAnimation() {
        

        float time = 1f;
        float downAngle = 78f;
        float scaleMainFlameChange = mainFlame.transform.localScale.y * 1.7f;
        float scaleFlamesChange = flames.transform.localScale.y * 1.7f;
        LeanTween.rotateX(mesh, -downAngle, time).setLoopPingPong(1);
        LeanTween.scaleY(mainFlame, scaleMainFlameChange, time*3).setLoopPingPong(1);
        LeanTween.scaleY(flames, scaleFlamesChange, time*3).setLoopPingPong(1);
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(BellowsBehaviour))]
public class BellowsBehaviourInspector : Editor {
	public override void OnInspectorGUI() {
		base.OnInspectorGUI();
	}
} 
#endif
