using UnityEngine;

namespace Utils {
	public static class Extensions {
		public static Vector2 Divide(this Vector2 t, Vector2 other) => new Vector2(t.x / other.x, t.y / other.y);
	}
}