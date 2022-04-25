using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

public class SkillCell : MonoBehaviour
{
    public float radius = 20f;
    public void SkillGained() { }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Handles.matrix = transform.localToWorldMatrix;
        Handles.DrawWireDisc(Vector3.zero, -Camera.current.transform.forward, radius);
    }
#endif
}
