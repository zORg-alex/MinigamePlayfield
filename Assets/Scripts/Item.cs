using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Item props: level, name, temperature...
[ExecuteInEditMode, SelectionBase]
public class Item : MonoBehaviour {
    [ShowInInspector]
    internal ItemObject ItemObject { get => _itemObject; set { _itemObject = value; SetTexture(); } }
    [HideInInspector,SerializeField]
    ItemObject _itemObject;
	private SpriteRenderer sr;
    private int DrawOrder;

	private void OnEnable() {
        sr = GetComponentInChildren<SpriteRenderer>();
        if (sr != null) 
            DrawOrder = sr.sharedMaterial.renderQueue;
	}

	[Button]
    public void SetTexture() {
        sr.sprite = ItemObject.sprite;
    }

    public void DrawOverUI() {
        sr.sharedMaterial.renderQueue++;
    }
    public void NormalDrawOrder() {
        sr.sharedMaterial.renderQueue = DrawOrder;
	}
}