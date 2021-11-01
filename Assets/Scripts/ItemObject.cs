using UnityEngine;

[CreateAssetMenu(fileName = "item", menuName = "ScriptableObjects/ItemObject", order = 1)]
public class ItemObject : ScriptableObject {
	public Texture2D texture;
	public Sprite sprite;
	public string name;
	public string description;
}
