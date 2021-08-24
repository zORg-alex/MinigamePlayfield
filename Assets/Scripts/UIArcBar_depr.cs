using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIArcBar_depr : MaskableGraphic
{

    [Min(0)]
    public float InnerThickness = 10;
    [Range(0, 360)]
    public float ArcAngle = 180f;
    [Min(2)]
    public int quality = 18;

    [SerializeField]
    Texture m_Texture;
    [Min(0)]
    public float OuterRadius;

    // make it such that unity will trigger our ui element to redraw whenever we change the texture in the inspector
    public Texture texture
    {
        get
        {
            return m_Texture;
        }
        set
        {
            if (m_Texture == value)
                return;

            m_Texture = value;
            SetTransformStuff();
        }
    }
    public override Texture mainTexture
    {
        get
        {
            return m_Texture == null ? s_WhiteTexture : m_Texture;
        }
    }

    public float startAngle { get => -ArcAngle / 2f; }
    public float angleStep { get => ArcAngle / quality; }
    float innerRadiusCoef { get => (OuterRadius - InnerThickness) / OuterRadius; }
    float rightmostX { get => ArcAngle < 180 ? Mathf.Sin(Mathf.Deg2Rad * (-startAngle)) * OuterRadius : OuterRadius; }
    float bottommostY { get => Mathf.Cos(Mathf.Deg2Rad * startAngle) * (ArcAngle < 180 ? OuterRadius - InnerThickness : OuterRadius); }
    float arcFullHeight { get => OuterRadius - bottommostY; }
    float arcFullWidth { get => 2 * rightmostX; }

    // actually update our mesh
    protected override void OnPopulateMesh(VertexHelper vh)
    {
        //Check all input is correct
        if (quality < 2) quality = 2;


        // Clear vertex helper to reset vertices, indices etc.
        vh.Clear();

        var vert = new UIVertex();
        vert.color = color;

        for (int i = 0; i <= quality; i++)
        {
            Vector2 pos = new Vector2(Mathf.Sin(Mathf.Deg2Rad * (startAngle + i * angleStep)) * OuterRadius, Mathf.Cos(Mathf.Deg2Rad * (startAngle + i * angleStep)) * OuterRadius);
            vert.position = pos;
            vert.uv0 = new Vector2(0 + (float)i / quality, 1f);
            vh.AddVert(vert);

            vert.position = pos * innerRadiusCoef;
            vert.uv0 = new Vector2(0 + (float)i / quality, 0f);
            vh.AddVert(vert);

            if (i > 0)
            {
                vh.AddTriangle(i * 2, i * 2 - 1, i * 2 - 2);
                vh.AddTriangle(i * 2, i * 2 + 1, i * 2 - 1);
            }
        }
    }
    protected override void OnRectTransformDimensionsChange()
    {
        base.OnRectTransformDimensionsChange();
        SetTransformStuff();
    }
    protected override void OnValidate()
    {
        SetTransformStuff();
    }

    internal virtual void SetTransformStuff()
    {
        var ancDiff = rectTransform.anchorMax - rectTransform.anchorMin;
        rectTransform.sizeDelta = new Vector2(arcFullWidth * (1 + ancDiff.x), arcFullHeight * (1 + ancDiff.y));
        rectTransform.pivot = new Vector2(0.5f, -(OuterRadius - arcFullHeight)/arcFullHeight);

        SetVerticesDirty();
        SetMaterialDirty();
    }
}
