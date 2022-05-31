using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using Utility;

[ExecuteAlways, RequireComponent(typeof(RectTransform))]
public class GenerateSkills : MonoBehaviour
{
	public SkillCell CellPrefab;
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
		var nodes = nodeProvider.GetNodeList(rand, 200, rectTransform.rect.size);
		var linkedNodes = nodeLinker.GetLinkedNodes(nodes, rand, out var links, 250);
		Dictionary<LinkedPoint, SkillCell> skills = new Dictionary<LinkedPoint, SkillCell>();
		var sizeDelta = CellPrefab.GetComponent<RectTransform>().sizeDelta;
		bool firstElement = true;
		foreach (var node in linkedNodes) {
			var skillCell = Instantiate(CellPrefab);
			var skillCellTransform = skillCell.GetComponent<RectTransform>();
			skillCellTransform.SetParent(transform);
			skillCellTransform.anchoredPosition3D = Vector3.zero;
			skillCellTransform.localScale = Vector3.one;
			skillCellTransform.localRotation = Quaternion.identity;
			skillCellTransform.anchoredPosition = new Vector3(node.Point.x, node.Point.z);
			skillCellTransform.sizeDelta = sizeDelta;
			skillCell.gameObject.SetActive(false);
			skills.Add(node, skillCell);
			if (firstElement) {
				skillCell.gameObject.SetActive(true);
				firstElement = false;
			}
		}

        foreach (var link in links)
        {
			skills[link[0]].AddNeighbor(skills[link[1]]);
        }
        

	}

}
