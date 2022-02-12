using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine;
using System;

public class ClosestNodeLinker : MonoBehaviour, INodeLinker {
	public float DefaultDistance;
	public float WeightCof;
	[Range(0, 1)]
	public float DistanceCof;

	public LinkedPoint[] GetLinkedNodes(Vector3[] nodes, System.Random rand, out List<LinkedPoint[]> links, float minDistance) {
		if (rand == null) rand = new System.Random();
		var LinkedNodes = nodes.Select(n => new LinkedPoint(n)).ToArray();
		links = new List<LinkedPoint[]>();

		foreach (var node in LinkedNodes) {
			foreach (var node2 in LinkedNodes) {
				if (node != node2 && !node.IsLinked(node2) && node.Point.DistanceTo(node2) <= RequiredDistance(minDistance)) {
					node.Link(node2);
					links.Add(new LinkedPoint[2] { node, node2 });
				}
			}

		}

		return LinkedNodes;
	}

	public LinkedPoint[] GetLinkedNodes((Vector3 point, float weight)[] nodes, System.Random rand, out List<LinkedPoint[]> links, float minDistance) {
		if (rand == null) rand = new System.Random();
		var LinkedNodes = nodes.Select(n => new LinkedPoint(n.point, n.weight)).ToArray();
		links = new List<LinkedPoint[]>();

		foreach (var node in LinkedNodes) {
			foreach (var node2 in LinkedNodes) {
				if (node != node2 && !node.IsLinked(node2) && node.Point.DistanceTo(node2) <= RequiredDistance(minDistance)) {
					node.Link(node2);
					links.Add(new LinkedPoint[2] { node, node2 });
				}
			}

		}

		return LinkedNodes;
	}

	private float RequiredDistance(float minDistance) {
		float maxDistance = (float)Math.Sqrt(2) * minDistance;
		return minDistance + (maxDistance - minDistance) * DistanceCof;
	}

	public LinkedPoint[] GetSpawnPoint(int count, LinkedPoint[] linkedPoints, ref int pathLength) {
		if (linkedPoints.Count() <= count) return null;
		var points = GetPathsDictionary(linkedPoints);

		var pathGroups = points.Values.GroupBy(p => p.path).ToArray();
		foreach (var g in pathGroups.Reverse()) {
			if (g.Count() >= count) {
				pathLength = g.Key;
				return g.Select(g => g.obj).Take(count).ToArray();

			}
		}
		return new LinkedPoint[0];
	}

	private static Dictionary<LinkedPoint, AstarNode<LinkedPoint>> GetPathsDictionary(LinkedPoint[] linkedPoints) {
		var points = linkedPoints.ToDictionary(p => p, p => new AstarNode<LinkedPoint>(p));
		var current = points[linkedPoints.First()];
		var openPoints = new List<AstarNode<LinkedPoint>>();

		current.path = 0;

		do {
			var neighbors = current.obj.LinkedPoints.Select(p => points[p]).Where(n => n.path > current.path + 1).ToList();
			neighbors.ForEach(n => {
				n.parent = current.obj;
				n.path = current.path + 1;
			});
			openPoints.AddRange(neighbors);

			current = openPoints.FirstOrDefault();
			openPoints.Remove(current);
		} while (openPoints.Count > 0);
		return points;
	}


}
public class AstarNode<T> {
	public int path = int.MaxValue;
	public T obj;
	public T parent;

	public AstarNode(T obj) {
		this.obj = obj;
	}
}
