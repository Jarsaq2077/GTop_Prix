using UnityEngine;

public class ProgresoAuto : MonoBehaviour
{
    public Transform waypointsList;
    public Transform[] waypoints;
    public int lastCheckpointIndex = 0;
    public float progressToNext = 0f;

    public int lapCount = 0;
    public int totalProgress = 0;

    void Start()
    {
        waypoints = new Transform[waypointsList.childCount];
        for (int i = 0; i < waypointsList.childCount; i++)
            waypoints[i] = waypointsList.GetChild(i);
    }

    void Update()
    {
        AvanzarProgreso();
    }

    void AvanzarProgreso()
    {
        int next = (lastCheckpointIndex + 1) % waypoints.Length;

        Vector3 pos = transform.position;
        Vector3 a = waypoints[lastCheckpointIndex].position;
        Vector3 b = waypoints[next].position;

        float t = ProyeccionEnSegmento(pos, a, b);
        progressToNext = t;

        if (t >= 1f)
        {
            lastCheckpointIndex = next;

            if (next == 0)
                lapCount++;

            totalProgress = lapCount * waypoints.Length + lastCheckpointIndex;
            progressToNext = 0f;
        }
    }

    float ProyeccionEnSegmento(Vector3 p, Vector3 a, Vector3 b)
    {
        Vector3 AP = p - a;
        Vector3 AB = b - a;
        float ab2 = AB.sqrMagnitude;

        if (ab2 <= 0.01f) return 0f;

        float t = Vector3.Dot(AP, AB) / ab2;
        return Mathf.Clamp01(t);
    }
}
