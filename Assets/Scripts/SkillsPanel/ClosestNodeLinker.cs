using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class ClosestNodeLinker : MonoBehaviour, INodeLinker
{
	public float DefaultDistance;
	public float WeightCof;
	[Range(0, 1)]
	public float DistanceCof;
	public int NumberOfConnections = 4;
	public int MinNumberOfConnections = 2;

	public LinkedPoint[] GetLinkedNodes(Vector3[] nodes, System.Random rand, out List<LinkedPoint[]> links, float minDistance)
	{
		if (rand == null) rand = new System.Random();
		var LinkedNodes = nodes.Select(n => new LinkedPoint(n)).ToArray();
		links = new List<LinkedPoint[]>();
		List<LinkedPoint> notProcessed = LinkedNodes.ToList();
		List<LinkedPoint> toProcess = new List<LinkedPoint>() { LinkedNodes[0] };
		var connectionCount = NumberOfConnections;

		while (toProcess.Count > 0) {
			
			var currentNode = toProcess.Last();
			toProcess.Remove(currentNode);
			notProcessed.Remove(currentNode);
			var orderedNodes = notProcessed.OrderBy((n) => Vector3.Distance(currentNode,n) );
			var nodesToAdd = orderedNodes.Take(connectionCount);
			toProcess.AddRange(nodesToAdd);
            foreach (var node in nodesToAdd)
            {
				if (!currentNode.IsLinked(node) && node.LinkedPoints.Count == 0 && !isIntersecting(currentNode, node, links))
                {
					currentNode.Link(node);
					links.Add(new LinkedPoint[2] { currentNode, node });
				}
				
			}

			connectionCount = Math.Max(MinNumberOfConnections, connectionCount - 1);
		}

		return LinkedNodes;
	}

    private bool isIntersecting(LinkedPoint currentNode, LinkedPoint node, List<LinkedPoint[]> links)
    {
		bool intersecting = false;
		float distance = currentNode.Point.DistanceTo(node.Point);
		Vector3 currentLine = node.Point - currentNode.Point;
        foreach (var link in links)
        {
			Vector3 testLine = link[0].Point - link[1].Point;
			Vector3 crossedPoint = Vector3.Cross(currentLine, testLine);
 			if (crossedPoint.x != 0 && crossedPoint.z != 0)
            {
				intersecting = true;
				break;
            }
        }
		

		return intersecting;
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
				if (node.LinkedPoints.Count == 3) break;

				if (node != node2 && !node.IsLinked(node2) && node.Point.DistanceTo(node2) <= RequiredDistance(minDistance) && node2.LinkedPoints.Count == 0)
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
