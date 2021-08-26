using UnityEngine;
using UnityEngine.UI;
using Utils;

/// <summary>
/// Thanks https://www.hallgrimgames.com/blog/2018/11/25/custom-unity-ui-meshes for reference
/// Also VS 2022 preview does show decompiled code that helped quite a bit
/// </summary>
public class UIArcBar : MaskableGraphic {
    [SerializeField]
    Texture m_Texture;
    // make it such that unity will trigger our ui element to redraw whenever we change the texture in the inspector
    public Texture texture {
        get {
            return m_Texture;
        }
        set {
            if (m_Texture == value)
                return;

            m_Texture = value;
            SetTransformStuff();
        }
    }
    public override Texture mainTexture {
        get {
            return m_Texture == null ? s_WhiteTexture : m_Texture;
        }
    }

    protected override void OnRectTransformDimensionsChange() {
        base.OnRectTransformDimensionsChange();
        SetTransformStuff();
    }
    protected override void OnValidate() {
        SetTransformStuff();
    }

    internal void SetTransformStuff() {

        SetVerticesDirty();
        SetMaterialDirty();
    }

    [Min(2)]
    public int quality = 18;
    [Range(0, 360)]
    public float ArcAngle = 180f;
    [Range(0f, 1f)]
    public float RelativeThickness;

	internal float FigureOutRadiusFromWidth(Rect AdjR) => ArcAngle < 180 ?
        AdjR.width / Mathf.Sin(Mathf.Deg2Rad * ArcAngle / 2) / 2 :
        AdjR.width / 2;

    internal float ArcRawHeight(float radius) =>
        ArcAngle < 180 ? Mathf.Sin(Mathf.Deg2Rad * halfAngle) * radius / Mathf.Tan(Mathf.Deg2Rad * (180 - halfAngle) / 2) :
        (1 + Mathf.Cos(Mathf.Deg2Rad * (180 - halfAngle))) * radius;

    internal float ExactFit(float radius) =>
        (GetPixelAdjustedRect().height - ArcRawHeight(radius)) / 2;

    internal float FigureOutRadiusFromHeight(Rect AdjR) => ArcAngle < 180 ?
        Mathf.Tan(Mathf.Deg2Rad * (180 - halfAngle) / 2) * AdjR.height / Mathf.Sin(Mathf.Deg2Rad * halfAngle) :
        AdjR.height / (1 + Mathf.Cos(Mathf.Deg2Rad * (180 - halfAngle)));

    private float halfAngle => ArcAngle / 2;
    private float IdealAspectRatio => ArcAngle <= 180 ? 2 * Mathf.Tan(Mathf.Deg2Rad * (180 - halfAngle) / 2) : 2 / (1 + Mathf.Cos(Mathf.Deg2Rad * (180 - halfAngle)));
    private bool RectIsTAller(float width, float height) => width / height < IdealAspectRatio;
    public float startAngle { get => -ArcAngle / 2f; }
    public float angleStep { get => ArcAngle / quality; }

    protected override void OnPopulateMesh(VertexHelper vh) {
		//Check all input is correct
		if (quality < 2) quality = 2;

		// Clear vertex helper to reset vertices, indices etc.
		vh.Clear();

		Rect adjR = GetPixelAdjustedRect();
		float radius, innerRadiusCoef;
		Vector2 origin;
		Prep(adjR, out radius, out origin, out innerRadiusCoef);

		var vert = new UIVertex();
		vert.color = color;

		for (int i = 0; i <= quality; i++) {
			Vector2 pos = new Vector2(Mathf.Sin(Mathf.Deg2Rad * (startAngle + i * angleStep)) * radius, Mathf.Cos(Mathf.Deg2Rad * (startAngle + i * angleStep)) * radius);
			vert.position = origin + pos;
			vert.uv0 = new Vector2(0 + (float)i / quality, 1f);
			vh.AddVert(vert);

			vert.position = origin + pos * innerRadiusCoef;
			vert.uv0 = new Vector2(0 + (float)i / quality, 0f);
			vh.AddVert(vert);

			if (i > 0) {
				vh.AddTriangle(i * 2, i * 2 - 1, i * 2 - 2);
				vh.AddTriangle(i * 2, i * 2 + 1, i * 2 - 1);
			}
		}
	}

	private void Prep(Rect adjR, out float radius, out Vector2 origin, out float innerRadiusCoef) {
		if (RectIsTAller(adjR.width, adjR.height)) {
			radius = FigureOutRadiusFromWidth(adjR);
		} else {
			radius = FigureOutRadiusFromHeight(adjR);
		}
		var rawHeight = ArcRawHeight(radius);
		origin = GetOrigin(adjR, rawHeight, radius);
		innerRadiusCoef = 1 - RelativeThickness;
	}

	private static Vector2 GetOrigin(Rect adjR, float rawHeight, float radius) => new Vector2(
        adjR.x + adjR.width / 2,
        adjR.y + (adjR.height + rawHeight) / 2 - radius);

    public Vector3 GetWorldSpaceOrigin() {
        Rect adjR = GetPixelAdjustedRect();
        float radius, innerRadiusCoef;
        Vector2 origin;
        Prep(adjR, out radius, out origin, out innerRadiusCoef);
        return transform.TransformPoint(origin);
    }

	//private void OnDrawGizmos() {

 //       Rect adjR = GetPixelAdjustedRect();
 //       float radius, innerRadiusCoef;
 //       Vector2 origin;
 //       Prep(adjR, out radius, out origin, out innerRadiusCoef);

 //       Gizmos.color = RectIsTAller(adjR.width, adjR.height) ? Color.red.MultiplyAlpha(.7f) : Color.cyan.MultiplyAlpha(.7f);
 //       Gizmos.DrawLine(transform.TransformPoint(origin), transform.TransformPoint(origin) + transform.TransformVector(transform.up * radius));
 //   }
}