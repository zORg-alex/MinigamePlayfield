using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(MinMaxAttribute))]
public class MinMaxDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        var minmaxAttr = (MinMaxAttribute)attribute;
        var min = property.vector2Value.x;
        var max = property.vector2Value.y;
        EditorGUI.BeginChangeCheck();
        EditorGUI.MinMaxSlider(position, ref min, ref max, minmaxAttr.minLimit, minmaxAttr.maxLimit);
        if (EditorGUI.EndChangeCheck())
        {
            property.vector2Value = new Vector2(min, max);
        }

    }
}