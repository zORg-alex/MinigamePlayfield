using UnityEngine;

namespace Utils {
	public static class CursorUtils {
		public enum CursorType { ArrowsNS, ArrowsWE }

		static Texture2D NS;
		static Texture2D WE;

		public static void SetCursor(CursorType cursor) {
			switch (cursor) {
				case CursorType.ArrowsNS:
					//if (NS == null)
						var NS = Resources.Load<Texture2D>("Aero_NS");
					Cursor.SetCursor(NS, Vector2.one * 64, CursorMode.Auto);
					break;
				case CursorType.ArrowsWE:
					//if (WE == null)
						 var WE = Resources.Load<Texture2D>("Aero_WE");
					Cursor.SetCursor(WE, Vector2.one * 64, CursorMode.Auto);
					break;
				default:
					ResetCursor();
					break;
			}
		}
		public static void ResetCursor() {
			Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
		}

	}
}