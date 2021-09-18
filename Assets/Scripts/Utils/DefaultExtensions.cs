using UnityEngine;

public static class DefaultExtensions {
	public static float Rad2Deg(this float value) => value * Mathf.Rad2Deg;
	public static float Deg2Rad(this float value) => value * Mathf.Deg2Rad;

}