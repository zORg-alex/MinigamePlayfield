using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Utility {
	public static partial class Utils {

		//Mimic Unity's Random behaviour. But with working seed
		public static int Range(this System.Random rand, int minInclusive, int maxExclusive) {
			return rand.Next(minInclusive, maxExclusive);
		}

		public static int Range(this System.Random rand, int maxExclusive) {
			return Range(rand, 0, maxExclusive);
		}

		public static float Range(this System.Random rand, float minInclusive, float maxExclusive) {
			return minInclusive + (float)(rand.NextDouble() * (maxExclusive - minInclusive));
		}

		public static float Range(this System.Random rand, float maxExclusive) {
			return Range(rand, 0f, maxExclusive);
		}
		public static bool NextBool(this System.Random r, int probability = 50) {
			return r.NextDouble() < probability;
		}

		//* Used for Getting and setting System.Random state *//
		private static System.Reflection.FieldInfo[] randomFields;
		private static System.Reflection.FieldInfo[] RandomFields {
			get {
				if (randomFields == null) {
					randomFields = new System.Reflection.FieldInfo[3];
					var t = typeof(System.Random);
					randomFields[0] = t.GetField("SeedArray", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
					randomFields[1] = t.GetField("inext", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
					randomFields[2] = t.GetField("inextp", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
				}
				return randomFields;
			}
		}
		/// <summary>
		/// Gets <see cref="System.Random"/> current state array and indexes with Reflection.
		/// </summary>
		/// <param name="rand"></param>
		/// <returns></returns>
		public static int[] GetSeedArray(this System.Random rand) {
			var state = new int[58];
			((int[])RandomFields[0].GetValue(rand)).CopyTo(state, 0);
			state[56] = (int)RandomFields[1].GetValue(rand);
			state[57] = (int)RandomFields[2].GetValue(rand);
			return state;
		}

		/// <summary>
		/// Restores saved <see cref="System.Random"/> state and indexes with Reflection. Use with caution.
		/// </summary>
		/// <param name="rand"></param>
		/// <param name="seedArray"></param>
		public static void SetSeedArray(this System.Random rand, int[] seedArray) {
			if (seedArray.Length != 56 + 2) return;

			Array.Copy(seedArray, ((int[])RandomFields[0].GetValue(rand)), 56);
			RandomFields[1].SetValue(rand, seedArray[56]);
			RandomFields[2].SetValue(rand, seedArray[57]);
		}

		public static Vector3 Scaled(this Vector3 t, Vector3 scale) {
			return new Vector3(t.x * scale.x, t.y * scale.y, t.z * scale.z);
		}
		public static void Destroy(UnityEngine.Object o) {
			try {
#if UNITY_EDITOR
				if (Application.isEditor)
					GameObject.DestroyImmediate(o);
				else
					GameObject.Destroy(o);
				return;
#endif
#pragma warning disable CS0162 // Unreachable code detected
				GameObject.Destroy(o);
#pragma warning restore CS0162 // Unreachable code detected

			}
			catch (Exception ex) {
				Debug.LogError($"Exception while deleting GameObject in Utils.Destroy: {ex.Message}");
			}
		}
		public static void Destroy(IEnumerable<Component> Component) {
			foreach (var comp in Component) {
				Destroy(comp.gameObject);
			}
		}
		public static void Destroy(IEnumerable<GameObject> GameObjects) {
			foreach (var go in GameObjects) {
				Destroy(go);
			}
		}
		public static void DestroyGameObjects(IEnumerable<Component> Components) {
			Component[] cs = Components.ToArray();
			foreach (var c in cs) {
				Destroy(c.gameObject);
			}
		}


		public static Transform[] GetChildren(this Transform transform) {
			var arr = new Transform[transform.childCount];
			for (int i = 0; i < transform.childCount; i++) {
				arr[i] = transform.GetChild(i);
			}
			return arr;
		}
		public static void DestroyChildren(this Transform t) {
			List<GameObject> destr = new List<GameObject>();
			foreach (Transform obj in t) {
				destr.Add(obj.gameObject);
			}
			foreach (var obj in destr) {
				Destroy(obj);
			}
		}

		/// <summary>
		/// Depth first recursive child search. Returns first child with <paramref name="name"/>
		/// </summary>
		/// <param name="transform"></param>
		/// <param name="name"></param>
		/// <returns></returns>
		public static Transform FindRecursively(this Transform transform, string name) {
			return Recurse(transform, name, 0);

			//This will hide unnecessary internal arument depth
			Transform Recurse(Transform transform_, string name_, int depth) {
				if (transform_.name.Equals(name_)) return transform_;
				depth++;
				foreach (Transform child in transform_) {
					var r = Recurse(child, name_, depth);
					if (r != null) return r;
				}
				return null;
			}

		}

	}
}