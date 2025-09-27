using System.Collections.Generic;
using UnityEngine;

public class trackWaypoint : MonoBehaviour
{
    public Color lineColor = Color.green;
    [Range(0f, 1f)] public float sphereRadius = 0.5f;
    public List<Transform> nodes = new List<Transform>();

    private void Awake()
    {
        GetNodes();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = lineColor;
        GetNodes();

        for (int i = 0; i < nodes.Count; i++)
        {
            Vector3 currentWaypoint = nodes[i].position;
            Vector3 previousWaypoint = (i == 0) ? nodes[nodes.Count - 1].position : nodes[i - 1].position;

            Gizmos.DrawLine(previousWaypoint, currentWaypoint);
            Gizmos.DrawSphere(currentWaypoint, sphereRadius);
        }
    }

    private void GetNodes()
    {
        nodes = new List<Transform>();
        foreach (Transform child in transform)
        {
            if (child != transform) nodes.Add(child);
        }
    }
}
