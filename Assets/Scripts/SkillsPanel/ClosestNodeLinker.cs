using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine;
using System;

public class ClosestNodeLinker : MonoBehaviour, INodeLinker
{
	public float DefaultDistance;
	public float WeightCof;
	[Range(0, 1)]
	public float DistanceCof;

	public LinkedPoint[] GetLinkedNodes(Vector3[] nodes, System.Random rand, out List<LinkedPoint[]> links, float minDistance)
	{
		if (rand == null) rand = new System.Random();
		var LinkedNodes = nodes.Select(n => new LinkedPoint(n)).ToArray();
		links = new List<LinkedPoint[]>();

		foreach (var node in LinkedNodes)
		{
			foreach (var node2 in LinkedNodes)
			{
				if (node != node2 && !node.IsLinked(node2) && node.Point.DistanceTo(node2) <= RequiredDistance(minDistance))
				{
					node.Link(node2);
					links.Add(new LinkedPoint[2] { node, node2 });
				}
			}

		}

		return LinkedNodes;
	}

	public LinkedPoint[] GetLinkedNodes((Vector3 point, float weight)[] nodes, System.Random rand, out List<LinkedPoint[]> links, float minDistance)
	{
		if (rand == null) rand = new System.Random();
		var LinkedNodes = nodes.Select(n => new LinkedPoint(n.point, n.weight)).ToArray();
		links = new List<LinkedPoint[]>();

		foreach (var node in LinkedNodes)
		{
			foreach (var node2 in LinkedNodes)
			{
				if (node != node2 && !node.IsLinked(node2) && node.Point.DistanceTo(node2) <= RequiredDistance(minDistance))
				{
					node.Link(node2);
					links.Add(new LinkedPoint[2] { node, node2 });
				}
			}

		}

		return LinkedNodes;
	}

	private float RequiredDistance(float minDistance)
	{
		float maxDistance = (float)Math.Sqrt(2) * minDistance;
		return minDistance + (maxDistance - minDistance) * DistanceCof;
	}
}
