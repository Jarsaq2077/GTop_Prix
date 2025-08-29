using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CameraController : MonoBehaviour
{
    public GameObject Player;
    public GameObject FixPoint;
    public float smoothSpeed;
    public float heightDistance;
    public float minDistance;
    public float moveThreshold = 0.1f;
    private Rigidbody playerRb;
    void Start()
    {
        playerRb = Player.GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (playerRb.velocity.magnitude > moveThreshold)
        {
            Follow();
        }
    }

    private void Follow()
    {
        Vector3 desiredPos = FixPoint.transform.position + Vector3.up * heightDistance;
        Vector3 direction = (desiredPos - Player.transform.position).normalized;
        float distance = Vector3.Distance(desiredPos, Player.transform.position);

        if (distance < smoothSpeed)
        {
            desiredPos = Player.transform.position + direction * minDistance + Vector3.up * heightDistance;
            
           
        }
        transform.position = Vector3.Lerp(transform.position, desiredPos, Time.deltaTime*smoothSpeed);
        gameObject.transform.LookAt(Player.transform.position + Vector3.up*0.5f);
    }
}
