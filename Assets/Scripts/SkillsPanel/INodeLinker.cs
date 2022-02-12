using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface INodeLinker {
	LinkedPoint[] GetLinkedNodes((Vector3 point, float weight)[] nodes, System.Random rand, out List<LinkedPoint[]> links, float minDistance);
	LinkedPoint[] GetLinkedNodes(Vector3[] nodes, System.Random rand, out List<LinkedPoint[]> links, float minDistance);
}
