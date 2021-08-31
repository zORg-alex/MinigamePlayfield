using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(GripComponent))]
public class GripComponentEditor : Editor {
	private SerializedProperty modeProp;
	private SerializedProperty leftBuddiesProp;
	private SerializedProperty rightBuddiesProp;
	private SerializedProperty topBuddiesProp;
	private SerializedProperty bottomBuddiesProp;

	private void OnEnable() {
		modeProp = serializedObject.FindProperty("Mode");
		leftBuddiesProp = serializedObject.FindProperty("LeftBuddies");
		rightBuddiesProp = serializedObject.FindProperty("RightBuddies");
		topBuddiesProp = serializedObject.FindProperty("TopBuddies");
		bottomBuddiesProp = serializedObject.FindProperty("BottomBuddies");
	}

	public override void OnInspectorGUI() {
		base.OnInspectorGUI();
		var mode = (GripComponent.GripMode)modeProp.enumValueIndex;
		GUILayout.Label("Current mode: " + mode.ToString());

		if (mode == GripComponent.GripMode.Vertical) {
			EditorGUILayout.PropertyField(topBuddiesProp);
			EditorGUILayout.PropertyField(bottomBuddiesProp);
		} else {
			EditorGUILayout.PropertyField(leftBuddiesProp);
			EditorGUILayout.PropertyField(rightBuddiesProp);
		}
	}
}