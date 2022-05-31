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
    internal ItemObject ItemObject { get => _itemObject; set { _itemObject = value; } }
    [SerializeField]
    private ItemObject _itemObject;
    //public Mesh mesh;
    //public Material material;
    private MeshFilter mf;
    private MeshCollider mc;
    private MeshRenderer mr;
    private SpriteRenderer sr;
    private int DrawOrder;

    private void OnEnable() {
       // sr = GetComponentInChildren<SpriteRenderer>();
        mf = GetComponentInChildren<MeshFilter>();
        mr = GetComponentInChildren<MeshRenderer>();
        // if (sr != null) 
        // DrawOrder = sr.sharedMaterial.renderQueue;
        if (mf != null)
            mf.sharedMesh = _itemObject.mesh;
        mc = GetComponentInChildren<MeshCollider>();
        mc.sharedMesh = _itemObject.mesh;
        mc.convex.Equals(true);
        mr.sharedMaterial = _itemObject.material;
	}

	/*[Button]
    public void SetTexture() {
        sr.sprite = ItemObject.sprite;
    }

    public void DrawOverUI() {
        sr.sharedMaterial.renderQueue++;
    }
    public void NormalDrawOrder() {
        sr.sharedMaterial.renderQueue = DrawOrder;
	}*/
}