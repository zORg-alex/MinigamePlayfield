using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Utility;

[ExecuteAlways, RequireComponent(typeof(RectTransform))]
public class GenerateSkills : MonoBehaviour
{
	public SkillCell CellPrefab;
	public SkillLink skillLinkPrefab;
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
		var nodes = nodeProvider.GetNodeList(rand, 250, rectTransform.rect.size);
		var linkedNodes = nodeLinker.GetLinkedNodes(nodes, rand, out var links, 10f);
		//List<LinkedPoint> linkedSkills = 
		var sizeDelta = CellPrefab.GetComponent<RectTransform>().sizeDelta;
		foreach (var node in linkedNodes) {
			var skill = Instantiate(CellPrefab);
			var skillTransform = skill.GetComponent<RectTransform>();
			skillTransform.SetParent(transform);
			skillTransform.anchoredPosition3D = Vector3.zero;
			skillTransform.localScale = Vector3.one;
			skillTransform.anchoredPosition = new Vector3(node.Point.x, node.Point.z);
			skillTransform.sizeDelta = sizeDelta;
			
		}

        foreach (var link in links)
        {
			var line = Instantiate(skillLinkPrefab);
			line.Initialize(link[0], link[1]);
        }

	}

}
