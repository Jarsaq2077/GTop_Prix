using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CarController : MonoBehaviour
{
    public float speed = 10f;
    public float turnSpeed = 50f;

    private Rigidbody rb;
    private InputManager input;

    [HideInInspector] public bool canMove = false;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        input = GetComponent<InputManager>();
    }

    void FixedUpdate()
    {
        if (canMove)
        {
            // Avanzar
            Vector3 forwardMove = transform.forward * input.vertical * speed * Time.fixedDeltaTime;

            rb.MovePosition(rb.position + forwardMove);

            // Girar
            float turn = input.horizontal * turnSpeed * Time.fixedDeltaTime;
            Quaternion turnRotation = Quaternion.Euler(0f, turn, 0f);
            rb.MoveRotation(rb.rotation * turnRotation);
        }
    }
}
