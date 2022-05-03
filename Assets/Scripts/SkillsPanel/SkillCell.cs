using System;
using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

public class SkillCell : MonoBehaviour
{
    public float radius = 20f;
    public List<SkillCell> neighbors;
    public SkillLink skillLinkPrefab;
    public void SkillGained()
    {
        //TODO check if already connected
        foreach (var n in neighbors)
        {
            var captureNeighbor = n;
            Instantiate(skillLinkPrefab, transform.parent).Initialize(transform.position, n.transform.position, () => ShowCell(captureNeighbor));
        }
    }

    public static void ShowCell(SkillCell cell)
    {
        cell.gameObject.SetActive(true);
    }

    internal void AddNeighbor(SkillCell skillCell) => neighbors.Add(skillCell);

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Handles.matrix = transform.localToWorldMatrix;
        Handles.DrawWireDisc(Vector3.zero, -Camera.current.transform.forward, radius);
    }

#endif
}
