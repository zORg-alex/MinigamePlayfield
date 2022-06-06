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
	public float transitionTime = 0.5f;

	public Transform scaleOnWorking;
	public float minScaleY = 1f;
	public float maxScaleY = 1.7f;

	public bool IsWorking { get => _isWorking; set => SetIsWorking(value); }

	private void SetIsWorking(bool value) {
		if (value == IsWorking) return;
		var state = animator.GetCurrentAnimatorStateInfo(0);
		LeanTween.value(gameObject, animator.speed, _isWorking ? 0 : 1, transitionTime)
			.setEaseInSine()
			.setOnUpdate( v => {
				animator.speed = v * maxSpeed;
				if (scaleOnWorking)
					scaleOnWorking.localScale = new Vector3(scaleOnWorking.localScale.x, Mathf.Lerp(minScaleY, maxScaleY, v), scaleOnWorking.localScale.z);
			});
		_isWorking = value;
	}

	private void OnEnable() {
		animator = GetComponent<Animator>();
	}

	private void Start() {
		animator.speed = 0;
	}

	//[Button]
	//public void ToggleBellows() {
	//	IsWorking = !IsWorking;
	//}
}

#if UNITY_EDITOR
[CustomEditor(typeof(BellowsBehaviour))]
public class BellowsBehaviourInspector : Editor {
	private BellowsBehaviour script;

	private void OnEnable() {
		script = (BellowsBehaviour)target;
	}
	public override void OnInspectorGUI() {
		base.OnInspectorGUI();
		script.IsWorking = GUILayout.Toggle(script.IsWorking, "IsWorking");
	}
} 
#endif
