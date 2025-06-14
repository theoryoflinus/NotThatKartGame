using System.Runtime.CompilerServices;
using UnityEngine;

public class KartController : MonoBehaviour
{
    public float acceleration = 500f;
    public float steering = 300f;

    private Rigidbody rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        float moveInput = Input.GetAxis("Horizontal");
        float steerInput = Input.GetAxis("Vertical");

        rb.AddForce(transform.forward * moveInput * acceleration * Time.deltaTime);
        rb.MoveRotation(rb.rotation * Quaternion.Euler(0f, steerInput * steering * Time.deltaTime, 0f));
    }
}
