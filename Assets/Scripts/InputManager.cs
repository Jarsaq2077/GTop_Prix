using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    internal enum Driver
    {
        AI,
        Keyboard,
        Mobile
    }

    [SerializeField] Driver driveController;
    [HideInInspector] public float vertical;
    [HideInInspector] public float horizontal;
    [HideInInspector] public bool handbrake;
    [HideInInspector] public bool boosting;

    public trackWaypoint waypoints;
    public Transform currentWaypoint;
    public List<Transform> nodes = new List<Transform>();
    [Range(0, 10)] public int distanceOffset;
    [Range(0, 10)] public float steerForce = 5f;

    private void Awake()
    {
        waypoints = GameObject.FindGameObjectWithTag("path").GetComponent<trackWaypoint>();
    }

    private void Start()
    {
        nodes = waypoints.nodes;  // <<< CARGA LOS NODES AQUÍ
        calculateDistanceOfWaypoints();
    }

    private void FixedUpdate()
    {
        switch (driveController)
        {
            case Driver.AI:
                AIDrive();
                break;

            case Driver.Keyboard:
                keyboardDrive();
                break;

            case Driver.Mobile:
                mobileDrive();
                break;
        }

        calculateDistanceOfWaypoints();
    }

    private void AIDrive()
    {
        AISteer();
        vertical = 1f; // acelerar siempre hacia adelante
    }

    private void keyboardDrive()
    {
        vertical = Input.GetAxis("Vertical");
        horizontal = Input.GetAxis("Horizontal");
        handbrake = (Input.GetAxis("Jump") != 0);
        boosting = Input.GetKey(KeyCode.LeftShift);
    }

    private void mobileDrive()
    {
        // Para controles en móvil
    }

    private void calculateDistanceOfWaypoints()
    {
        if (nodes == null || nodes.Count == 0) return;

        Vector3 position = transform.position;
        float distance = Mathf.Infinity;

        for (int i = 0; i < nodes.Count; i++)
        {
            float currentDistance = Vector3.Distance(nodes[i].position, position);

            if (currentDistance < distance)
            {
                int targetIndex = i + distanceOffset;
                if (targetIndex >= nodes.Count)
                    targetIndex = 0; // volver al inicio

                currentWaypoint = nodes[targetIndex];
                distance = currentDistance;
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (currentWaypoint != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(currentWaypoint.position, 1f);
        }
    }

    private void AISteer()
    {
        if (currentWaypoint == null) return;

        // Calcula hacia dónde girar
        Vector3 relative = transform.InverseTransformPoint(currentWaypoint.position);
        relative.Normalize();

        // horizontal determina giro
        horizontal = (relative.x / relative.magnitude) * steerForce;
    }
}
