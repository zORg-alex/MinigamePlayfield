using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode, SelectionBase]
public class Item : MonoBehaviour {
    [ShowInInspector]
    internal ItemObject ItemObject { get => _itemObject; set { _itemObject = value; } }
	[ShowInInspector]
    public bool isHeating { get; set; }
    [ShowInInspector]
    public float temperature { get; set; }

    public Material material;
    [SerializeField]
    public float temperatureDelta = 10f;
    private ItemObject _itemObject;
    
    private MeshFilter mf;
    private MeshCollider mc;
    private MeshRenderer mr;
    private SpriteRenderer sr;
    private int DrawOrder;
    private float heatingCoef = 0.00032685f;

    private void OnEnable() {
        isHeating = false;
        temperature = 25f;
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

	private void Update() {
		if (isHeating) {
            temperature += temperatureDelta;
        }
        MaterialPropertyBlock materialPropertyBlock = new MaterialPropertyBlock();
        mr.GetPropertyBlock(materialPropertyBlock);
        materialPropertyBlock.SetFloat("_GlowMaxAmount", temperature > 500 ? temperature * heatingCoef : 0);
        mr.SetPropertyBlock(materialPropertyBlock);
    }
}