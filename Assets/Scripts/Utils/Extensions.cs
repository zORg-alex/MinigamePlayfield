using UnityEngine;

namespace Utils {
	public static class Extensions {
		public static Vector2 Divide(this Vector2 t, Vector2 other) => new Vector2(t.x / other.x, t.y / other.y);

		public static Color MultiplyAlpha(this Color col, float multiplier) => new Color(col.r, col.g, col.b, col.a * multiplier);
		public static Color MultiplyAlpha(this Color col, double multiplier) => new Color(col.r, col.g, col.b, col.a * (float)multiplier);
	}
}