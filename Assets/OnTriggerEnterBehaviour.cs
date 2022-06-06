using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class OnTriggerEnterBehaviour : MonoBehaviour
{
	private new Collider collider;
	private void OnEnable() {
		collider = GetComponent<Collider>();
	}
	public UnityEvent<(Collider @this, Collider other)> onTriggerEnter = new UnityEvent<(Collider @this, Collider other)>();
	private void OnTriggerEnter(Collider other) {
		Debug.Log("Starting to Forge");
		onTriggerEnter.Invoke((collider, other));

	}

}
