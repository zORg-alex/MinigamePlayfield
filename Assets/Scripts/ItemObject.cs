using UnityEngine;

[CreateAssetMenu(fileName = "item", menuName = "ScriptableObjects/ItemObject", order = 1)]
public class ItemObject : ScriptableObject {
	public Mesh mesh;
	public Material material;
	public Sprite sprite;
	public string itemName;
	public string description;
	public float temperature;
	public string metal;
	[Range(0,1)]
	public float solidStructure;
}
