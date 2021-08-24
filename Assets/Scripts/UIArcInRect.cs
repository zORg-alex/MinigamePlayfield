using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIArcInRect : MaskableGraphic
{

    public float InnerThickness = 10;
    [Range(0, 360)]
    public float ArcAngle = 180f;
    public int quality = 18;

    [SerializeField]
    Texture m_Texture;

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
            SetVerticesDirty();
            SetMaterialDirty();
        }
    }
    public override Texture mainTexture
    {
        get
        {
            return m_Texture == null ? s_WhiteTexture : m_Texture;
        }
    }

    // actually update our mesh
    protected override void OnPopulateMesh(VertexHelper vh)
    {
        //Check all input is correct


        Rect pixelAdjustedRect = GetPixelAdjustedRect();
        Vector4 rect = new Vector4(pixelAdjustedRect.x, pixelAdjustedRect.y, pixelAdjustedRect.x + pixelAdjustedRect.width, pixelAdjustedRect.y + pixelAdjustedRect.height);

        var startAngle = -ArcAngle / 2f;
        var angleStep = ArcAngle / quality;
        var OuterRadius = ArcAngle < 180 ? pixelAdjustedRect.width / 2 / Mathf.Cos(Mathf.Deg2Rad * (90 + startAngle)) : pixelAdjustedRect.width / 2;
        var innerRadiusCoef = (OuterRadius - InnerThickness) / OuterRadius;

        var rightmostX = ArcAngle < 180 ? Mathf.Sin(Mathf.Deg2Rad * (-startAngle)) : OuterRadius;
        var bottommostY = Mathf.Cos(Mathf.Deg2Rad * startAngle) * (ArcAngle < 180 ? OuterRadius - InnerThickness : OuterRadius);
        var arcFullHeight = OuterRadius - bottommostY;
        var arcFullWidth = 2 * rightmostX;
        var verticalOffset = -OuterRadius + arcFullHeight / 2 + (.5f -rectTransform.pivot.y) * pixelAdjustedRect.height;
        var horizontalOffset = (.5f - rectTransform.pivot.x) * pixelAdjustedRect.width;

        // Clear vertex helper to reset vertices, indices etc.
        vh.Clear();

        var vert = new UIVertex();
        vert.color = color;

        for (int i = 0; i <= quality; i++)
        {
            Vector2 pos = new Vector2(Mathf.Sin(Mathf.Deg2Rad * (startAngle + i * angleStep)) * OuterRadius, Mathf.Cos(Mathf.Deg2Rad * (startAngle + i * angleStep)) * OuterRadius);
            vert.position = pos + Vector2.up * verticalOffset + Vector2.right * horizontalOffset;
            vert.uv0 = new Vector2(0 + (float)i/quality, 1f);
            vh.AddVert(vert);

            vert.position = pos * innerRadiusCoef + Vector2.up * verticalOffset + Vector2.right * horizontalOffset;
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
        SetVerticesDirty();
        SetMaterialDirty();
    }
}
