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
        mf = GetComponentInChildren<MeshFilter>();
        mr = GetComponentInChildren<MeshRenderer>();
        mc = GetComponentInChildren<MeshCollider>();
        if (mf != null && mr != null && mc != null ) {
            mf.sharedMesh = _itemObject.mesh;
            mc.sharedMesh = _itemObject.mesh;
            mc.convex.Equals(true);
            mr.sharedMaterial = _itemObject.material;
            DrawOrder = mr.sharedMaterial.renderQueue;
        }     
	}

	[Button]
    public void SetTexture() {
        mf = GetComponentInChildren<MeshFilter>();
        mr = GetComponentInChildren<MeshRenderer>();
        mc = GetComponentInChildren<MeshCollider>();
        if (mf != null && mr != null && mc != null) {
            mf.sharedMesh = _itemObject.mesh;
            mc.sharedMesh = _itemObject.mesh;
            mc.convex.Equals(true);
            mr.sharedMaterial = _itemObject.material;
        }
    }

    public void DrawOverUI() {
        mr.sharedMaterial.renderQueue++;
    }
    public void NormalDrawOrder() {
        mr.sharedMaterial.renderQueue = DrawOrder;
	}
}