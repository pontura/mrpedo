using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Character : MonoBehaviour
{
    [Header("Forward Movement")]
    [SerializeField] private float forwardSpeed = 5f;

    [Header("Jetpack")]
    [SerializeField] private float jetpackForce = 15f;
    [SerializeField] private float maxFallSpeed = -20f;
    [SerializeField] private float maxRiseSpeed = 10f;

    [Header("Ground Check (optional)")]
    [SerializeField] private bool useGroundClamp = false;
    [SerializeField] private float groundY = -4f;

    private Rigidbody2D rb;
    private bool jetpackActive;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 3f; // gravedad base
    }

    void Update()
    {
        // Input básico (espacio o click)
        jetpackActive = Input.GetKey(KeyCode.Space) || Input.GetMouseButton(0);
    }

    void FixedUpdate()
    {
        HandleForwardMovement();
        HandleJetpack();
        ClampVelocity();
        ClampGround();
    }

    private void HandleForwardMovement()
    {
        // Mantiene velocidad constante en X
        rb.linearVelocity = new Vector2(forwardSpeed, rb.linearVelocity.y);
    }

    private void HandleJetpack()
    {
        if (jetpackActive)
        {
            // Fuerza continua hacia arriba
            rb.AddForce(Vector2.up * jetpackForce, ForceMode2D.Force);
        }
    }

    private void ClampVelocity()
    {
        float clampedY = Mathf.Clamp(rb.linearVelocity.y, maxFallSpeed, maxRiseSpeed);
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, clampedY);
    }

    private void ClampGround()
    {
        if (!useGroundClamp) return;

        if (transform.position.y < groundY)
        {
            transform.position = new Vector3(transform.position.x, groundY, transform.position.z);
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f);
        }
    }
}
