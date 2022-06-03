using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionCollection : MonoBehaviour
{
	public Transform trToAdd;
	public List<Vector3> positions = new List<Vector3>();
	public List<Quaternion> rotations = new List<Quaternion>();
	public List<string> names = new List<string>();

	[Button]
	public void AddCurrentPosition(string name) {
		if (name ==  null || name == "") return;
		if (names.Contains(name)) {
			int index = names.IndexOf(name);
			positions[index] = transform.position;
			rotations[index] = transform.rotation;
			return;
		}
		positions.Add(transform.position);
		rotations.Add(transform.rotation);
		names.Add(name);
	}


    public Vector3 GetPoint(string name, out Quaternion rotation) {
		int index = names.IndexOf(name);
		rotation = rotations[index];
		return positions[index];
	}
}
