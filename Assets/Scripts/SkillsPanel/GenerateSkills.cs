using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Utility;

[ExecuteAlways, RequireComponent(typeof(RectTransform))]
public class GenerateSkills : MonoBehaviour
{
	public RectTransform CellPrefab;
	private RectTransform rectTransform;
	private INodeProvider nodeProvider;
	private INodeLinker nodeLinker;

	private void OnEnable() {
		nodeProvider = GetComponent<INodeProvider>();
		nodeLinker = GetComponent<INodeLinker>();
		rectTransform = GetComponent<RectTransform>();

		if (nodeLinker == null || nodeProvider == null)
			Debug.LogError("nodeLinker or nodeProvider or kek missing");
	}

	private void Start() {
		Generate();
	}

	[Button]
	public void Generate() {
		var children = transform.GetChildren();
		Utility.Utils.DestroyGameObjects(children);

		var rand = new System.Random();
		var nodes = nodeProvider.GetNodeList(rand, 70, rectTransform.rect.size);
		var linkedNodes = nodeLinker.GetLinkedNodes(nodes, rand, out var links, 10f);

		foreach (var node in linkedNodes) {
			var skill = Instantiate(CellPrefab);
			skill.transform.parent = transform;
			skill.localScale = Vector3.one;
			skill.offsetMin = new Vector3(node.Point.x, node.Point.z);
			skill.sizeDelta = CellPrefab.sizeDelta;
		}

	}

}
