using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Utils {
	public static class Extensions {
		public static Vector2 Divide(this Vector2 t, Vector2 other) => new Vector2(t.x / other.x, t.y / other.y);

		public static Color MultiplyAlpha(this Color col, float multiplier) => new Color(col.r, col.g, col.b, col.a * multiplier);
		public static Color MultiplyAlpha(this Color col, double multiplier) => new Color(col.r, col.g, col.b, col.a * (float)multiplier);

		public static Vector2 ReadVector2(this InputAction action) => action.ReadValue<Vector2>();

		public static float HFoV(this Camera cam) {
			var radAngle = cam.fieldOfView * Mathf.Deg2Rad;
			var radHFOV = 2 * Math.Atan(Mathf.Tan(radAngle / 2) * cam.aspect);
			return (float)(Mathf.Rad2Deg * radHFOV);
		}
	}
}
