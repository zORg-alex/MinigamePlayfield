using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ChangeEventScript : MonoBehaviour
{
	public UnityEvent<GameObject> OnChange;
	public void Changed() {
		OnChange.Invoke(gameObject);
	}
}