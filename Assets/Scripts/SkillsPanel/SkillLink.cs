using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Utility;
public class SkillLink : MonoBehaviour
{
    private LineRenderer lineRenderer;
    private float counter;

    public Vector3 origin;
    public Vector3 destination;

    public float lineDrawSpeed = 6f;
    public float lineWidth = 5;
    System.Action onCompleted;

    IEnumerator Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.SetPosition(0, origin);
        lineRenderer.startWidth = transform.TransformVector(Vector3.one).magnitude * lineWidth;
        lineRenderer.endWidth = lineRenderer.startWidth;
        do
        {
            counter += lineDrawSpeed * Time.deltaTime;
            lineRenderer.SetPosition(1, Vector3.Lerp(origin, destination, counter));
            yield return null;
        } while (counter < 1);

        onCompleted?.Invoke();
    }

    public void Initialize(Vector3 a, Vector3 b, System.Action onCompleted)
    {
        origin = transform.InverseTransformPoint(a) + Vector3.forward * 10f;
        destination = transform.InverseTransformPoint(b) + Vector3.forward * 10f;
        this.onCompleted = onCompleted;
    }
}