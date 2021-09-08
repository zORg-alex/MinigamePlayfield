using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Item props: level, name, temperature...
public class Item : MonoBehaviour {

    [ShowInInspector]
    internal ItemObject ItemObject { get => _itemObject; set { _itemObject = value; SetTexture(); } }
    ItemObject _itemObject;

    private void SetTexture() {
        var sr = GetComponent<SpriteRenderer>();
        sr.sprite = ItemObject.sprite;
    }
}