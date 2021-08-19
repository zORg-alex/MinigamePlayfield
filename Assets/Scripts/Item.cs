using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Item : MonoBehaviour {
	public ScriptedItem scriptable;
	protected List<Item> StackedItems = new List<Item>();
	public bool IsRoot;
	public int StackSize => StackedItems.Count + 1;
	public bool StackFull => StackedItems.Count < scriptable.MaxStack - 1;
	public void AddToStack(Item item) {
		StackedItems.Add(item);
	}
	public void AddToStack(IEnumerable<Item> items) {
		StackedItems.AddRange(items);
		IsRoot = true;
	}


	public IEnumerable<Item> GetAllItems() {
		yield return this;
		foreach (var item in StackedItems) {
			yield return item;
		}
	}

	public Item GetAmount(int count) {
		if (count <= 0 || count > StackSize) return null;
		if (count == StackedItems.Count) {
			var r = StackedItems.First();
			r.StackedItems = StackedItems.Skip(1).ToList();
			StackedItems.Clear();
			IsRoot = false;
			return r;
		}
		var items = StackedItems.Skip(StackSize - count).ToList();
		StackedItems.RemoveRange(StackSize - count, count);
		var s = items.First();
		items.Remove(s);
		s.AddToStack(items);
		return s;
	}
	//public Item GetSpecific(Func<Item, bool> selector) {
	//	var f = GetAllItems().FirstOrDefault(selector);
	//	Remove()
	//		///Жопа. Так нельзя
	//}
}

public class ScriptedItem : ScriptableObject {
	public Sprite Sprite;
	public string Name;
	public string Description;
	public int MaxStack;
}