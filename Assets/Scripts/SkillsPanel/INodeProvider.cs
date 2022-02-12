using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface INodeProvider {
	Vector3[] GetNodeList(System.Random rand, float radius, Vector2 panelSize);
}
