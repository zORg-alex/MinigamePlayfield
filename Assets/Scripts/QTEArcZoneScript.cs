using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

[ExecuteInEditMode]
public class QTEArcZoneScript : MonoBehaviour {

	[SerializeField]
	Texture2D m_Texture;

	public Texture2D texture {
		get {
			return m_Texture;
		}
		set {
			if (m_Texture == value)
				return;

			m_Texture = value;
			RegenTexture();
		}
	}

	private UIArcBar gr;
	//[MinMax(0f, 1f)]
	public Vector2 MinMax = new Vector2(.4f, .6f);
	[Min(0)]
	public float Falloff = .05f;
	public Color color = Color.red;

	private Vector2 oldMinMax;
	private float oldFalloff;
	private Color oldColor;
	private void OnEnable() {
		gr = GetComponent<UIArcBar>();
		RegenTexture();
		controller = transform.parent.GetComponent<QTEArcController>();
		activationAction.Enable();
		activationAction.performed += ActivationAction_performed;
	}

	private void OnDisable() {
		activationAction.Disable();
		activationAction.performed -= ActivationAction_performed;
	}
	private void Update() {
		if (Falloff != oldFalloff || MinMax != oldMinMax || color != oldColor) {
			var maxFalloff = MinMax.y - MinMax.x;
			if (Falloff / 2 > maxFalloff) Falloff = maxFalloff;
			oldFalloff = Falloff;
			oldColor = color;
			oldMinMax = MinMax;
			RegenTexture();
		}
	}

	private void RegenTexture() {
		var nt = new Texture2D(texture.width, texture.height);
		var pixels = texture.GetPixels();
		for (int x = 0; x < texture.width; x++) {
			var grad = new Color(color.r, color.g, color.b, color.a * GetGradAlpha((float)x / texture.width));
			for (int y = 0; y < texture.height; y++) {
				pixels[x + y * texture.width] *= grad;
			}
		}
		nt.SetPixels(pixels);
		nt.Apply();
		gr.texture = nt;
	}

	private float GetGradAlpha(float pos) =>
	pos < MinMax.x - Falloff / 2 ? 0f :
	pos < MinMax.x + Falloff / 2 ? (pos - MinMax.x + Falloff / 2) / Falloff :
	pos < MinMax.y - Falloff / 2 ? 1f :
	pos < MinMax.y + Falloff / 2 ? 1 - (pos - MinMax.y + Falloff / 2) / Falloff : 0f;

	public bool IsOver(float pos) =>
		pos < MinMax.x - Falloff / 2 ? false :
		pos < MinMax.y + Falloff / 2 ? true : false;
	public bool IsPerfect(float pos) =>
		pos < MinMax.x + Falloff / 2 ? false :
		pos < MinMax.y - Falloff / 2 ? true : false;

	public bool isOver;
	public float lastPosition;
	public void SetCurrentPos(float pos) {
		lastPosition = pos;
		isOver = IsOver(pos);
	}


	public delegate void ZoneHitHandler(QTEArcZoneScript sender, float precision);
	public event ZoneHitHandler OnHit;
	public delegate void ZoneInteractionHandler(QTEArcZoneScript sender);
	public event ZoneInteractionHandler OnZoneEnter;
	public event ZoneInteractionHandler OnZoneExit;

	public InputAction activationAction;
	public QTEArcController controller;
	private void ActivationAction_performed(InputAction.CallbackContext ctx) {
		OnHit.Invoke(this, GetGradAlpha(controller.curPosition));
	}

}
