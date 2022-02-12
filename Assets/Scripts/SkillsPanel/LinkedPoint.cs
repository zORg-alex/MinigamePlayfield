using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinkedPoint {
	public Vector3 Point;
	public float Weight = 1f;

	[SerializeReference]
	public List<LinkedPoint> LinkedPoints = new List<LinkedPoint>();

	public LinkedPoint(Vector3 point) {
		Point = point;
	}

	public LinkedPoint(Vector3 point, float weight) {
		Point = point;
		Weight = weight;
	}

	public void Link(LinkedPoint Node, bool recurse = true) {
		if (Node == this || Node is null) return;
		LinkedPoints.Add(Node);
		if (recurse) Node.Link(this, false);
	}

	public void UnLink(LinkedPoint point, bool recurse = true) {
		if (point == this || point is null) return;
		LinkedPoints.Remove(point);
		if (recurse) point.UnLink(this, false);
	}

	public bool IsLinked(LinkedPoint Node) {
		return Node != null ? LinkedPoints.Contains(Node) : false;
	}

	public override bool Equals(object obj) {
		return obj is LinkedPoint point &&
			   Point.Equals(point.Point) &&
			   Weight == point.Weight &&
			   EqualityComparer<List<LinkedPoint>>.Default.Equals(LinkedPoints, point.LinkedPoints);
	}

	public override int GetHashCode() {
		int hashCode = 166848289;
		hashCode = hashCode * -1521134295 + Point.GetHashCode();
		hashCode = hashCode * -1521134295 + Weight.GetHashCode();
		hashCode = hashCode * -1521134295 + EqualityComparer<List<LinkedPoint>>.Default.GetHashCode(LinkedPoints);
		return hashCode;
	}

	public static implicit operator Vector3(LinkedPoint n) => n.Point;

	public override string ToString() {
		return $"{Point} : {Weight} (liked to {LinkedPoints})";
	}
}
