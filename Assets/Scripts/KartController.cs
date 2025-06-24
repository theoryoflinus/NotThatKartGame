using UnityEngine;
using UnityEngine.InputSystem; // Input System 사용

[RequireComponent(typeof(Rigidbody))]
public class SphereController : MonoBehaviour
{
    private PlayerInputActions.PlayerInputActions controls;
    private Vector2 moveInput;
    private Rigidbody rb;

    [SerializeField] private float moveForce = 10f;
    [SerializeField] private float maxSpeed = 5f;

    private void Awake()
    {
        controls = new PlayerInputActions.PlayerInputActions();

        // 이동 입력 처리
        controls.Player.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        controls.Player.Move.canceled += ctx => moveInput = Vector2.zero;

        rb = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }

    private void FixedUpdate()
    {
        Vector3 forceDirection = new Vector3(moveInput.x, 0f, moveInput.y);

        // 현재 속도가 최대 속도보다 낮을 때만 힘을 추가
        if (rb.linearVelocity.magnitude < maxSpeed)
        {
            rb.AddForce(forceDirection.normalized * moveForce, ForceMode.Force);
        }

        // 수평 속도 제한 (y축 제외)
        Vector3 horizontalVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);
        if (horizontalVelocity.magnitude > maxSpeed)
        {
            Vector3 limitedVelocity = horizontalVelocity.normalized * maxSpeed;
            rb.linearVelocity = new Vector3(limitedVelocity.x, rb.linearVelocity.y, limitedVelocity.z);
        }
    }
}
