using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;
using Utility;

public class PoissonNodeGenerator : SerializedMonoBehaviour, INodeProvider {
	public List<(Vector3 point, float weight)> WeightedNodeList;
	public Vector2 Size = Vector2.one * 300;
	public float Radius = 50;

	[Button]
	public void Generate() => GeneratePoints(null, Radius, Size);
	public void GeneratePoints(System.Random rand = null, float radius = 5f, Vector2 panelSize = default, int numSamplesBeforeRejection = 100) {
		if (panelSize == default) panelSize = Vector2.one * 300;
		if (rand == null) rand = new System.Random();

		float cellSize = radius / Mathf.Sqrt(2);

		int[,] grid = new int[Mathf.CeilToInt(panelSize.x / cellSize), Mathf.CeilToInt(panelSize.y / cellSize)];
		List<Vector2> points = new List<Vector2>();
		List<Vector2> spawnPoints = new List<Vector2>();

		//adding central point
		spawnPoints.Add(panelSize / 2);
		points.Add(panelSize / 2);
		grid[(int)(panelSize.x / 2 / cellSize), (int)(panelSize.y / 2 / cellSize)] = points.Count;
		while (spawnPoints.Count > 0) {
			int spawnIndex = rand.Range(0, spawnPoints.Count);
			Vector2 spawnCenter = spawnPoints[spawnIndex];
			bool candidateAccepted = false;

			for (int i = 0; i < numSamplesBeforeRejection; i++) {
				float angle = (float)rand.NextDouble() * Mathf.PI * 2;
				Vector2 dir = new Vector2(Mathf.Sin(angle), Mathf.Cos(angle));
				Vector2 candidate = spawnCenter + dir * rand.Range(radius, 2 * radius);
				if (IsValid(candidate, panelSize, cellSize, radius, points, grid)) {
					points.Add(candidate);
					spawnPoints.Add(candidate);
					grid[(int)(candidate.x / cellSize), (int)(candidate.y / cellSize)] = points.Count;
					candidateAccepted = true;
					break;
				}
			}
			if (!candidateAccepted) {
				spawnPoints.RemoveAt(spawnIndex);
			}

		}

		WeightedNodeList = points.Select(v => (point: new Vector3(v.x /*- panelSize.x / 2*/, 0, v.y /*- panelSize.y / 2*/), weight: rand.Range(0f, 1f))).ToList();
	}

	static bool IsValid(Vector2 candidate, Vector2 RegionSize, float cellSize, float radius, List<Vector2> points, int[,] grid) {
		var offset = 70;
		if (candidate.x >= 0 + offset && candidate.x < RegionSize.x - offset && candidate.y >= 0 + offset && candidate.y < RegionSize.y - offset) {
			int cellX = (int)(candidate.x / cellSize);
			int cellY = (int)(candidate.y / cellSize);
			int searchStartX = Mathf.Max(0, cellX - 2);
			int searchEndX = Mathf.Min(cellX + 2, grid.GetLength(0) - 1);
			int searchStartY = Mathf.Max(0, cellY - 2);
			int searchEndY = Mathf.Min(cellY + 2, grid.GetLength(1) - 1);

			for (int x = searchStartX; x <= searchEndX; x++) {
				for (int y = searchStartY; y <= searchEndY; y++) {
					int pointIndex = grid[x, y] - 1;
					if (pointIndex != -1) {
						float sqrDst = (candidate - points[pointIndex]).sqrMagnitude;
						if (sqrDst < radius * radius) {
							return false;
						}
					}
				}
			}
			return true;
		}
		return false;
	}

	public Vector3[] GetNodeList(System.Random rand, float radius, Vector2 panelSize) {
		GeneratePoints(rand, radius, panelSize);
		return WeightedNodeList.Select(p => p.point).ToArray();
	}

	public (Vector3 point, float weight)[] GetNodeListWithWeights() {
		return WeightedNodeList.ToArray();
	}
}
