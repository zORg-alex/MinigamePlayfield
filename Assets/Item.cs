using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Item props: level, name, temperature...
public class Item : MonoBehaviour {
    [ShowInInspector]
    internal ItemObject ItemObject { get => _itemObject; set { _itemObject = value; SetTexture(); } }
    ItemObject _itemObject;
	private SpriteRenderer sr;
    private int DrawOrder;

	private void OnEnable() {
        sr = GetComponentInChildren<SpriteRenderer>();
        DrawOrder = sr.material.renderQueue;
	}

	[Button]
    private void SetTexture() {
        sr.sprite = ItemObject.sprite;
    }

    public void DrawOverUI() {
        sr.material.renderQueue++;
    }
    public void NormalDrawOrder() {
        sr.material.renderQueue = DrawOrder;
	}
}