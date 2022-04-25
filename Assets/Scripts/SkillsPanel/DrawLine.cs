using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawLine : MonoBehaviour
{
    private LineRenderer lineRenderer;
    private float counter;
    private float dist;

    public Transform origin;
    public Transform destination;

    public float lineDrawSpeed = 6f;
    public float lineWidth = 0.45f;
    
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.SetPosition(0, origin.position);

        dist = Vector3.Distance(origin.position, destination.position);
    }

    
    void Update()
    {
        counter += 0.1f / lineDrawSpeed;
        lineRenderer.SetPosition(1, Vector3.Lerp(origin.position, destination.position, counter));
    }
}
