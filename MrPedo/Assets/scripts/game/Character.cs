using UnityEngine;
namespace Game
{

    [RequireComponent(typeof(Rigidbody2D))]
    public class Character : MonoBehaviour
    {
        [SerializeField] private Vector2 limits = new Vector2(-4.26f, 7.82f);
        [HideInInspector] public float forwardSpeed = 5f;

        [Header("Jetpack")]
        [SerializeField] private float jetpackForce = 15f;
        [SerializeField] private float maxFallSpeed = -20f;
        [SerializeField] private float maxRiseSpeed = 10f;
        [SerializeField] private float bounceFloor = 6f;
        [SerializeField] private float bounceCeil = 2f;

        private Rigidbody2D rb;
        [SerializeField] bool jetpackActive;

        void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            rb.gravityScale = 3f;
        }

        public void OnPedo( bool isOn)
        {
            jetpackActive = isOn;
        }

        void FixedUpdate()
        {
            HandleForwardMovement();
            bool hitted = CheckFloorOrCeil();
            if(!hitted)
            {
                HandleJetpack();
                ClampVelocity();
            }
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
                rb.AddForce(Vector2.up * jetpackForce, ForceMode2D.Force);
            }
        }
        public void Bounce(float force)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f);
            rb.AddForce(Vector2.up * force, ForceMode2D.Force);
        }
        private void ClampVelocity()
        {
            float clampedY = Mathf.Clamp(rb.linearVelocity.y, maxFallSpeed, maxRiseSpeed);
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, clampedY);
        }

        private bool CheckFloorOrCeil()
        {
            float force = 0;
            if (transform.position.y < limits.x)
            {
                force = bounceFloor * (rb.linearVelocityY * 10);
                print("choca floor: " + force);
                Bounce(-force);
            } else if (transform.position.y > limits.y)
            {
                force = bounceCeil * (rb.linearVelocityY * 10);
                print("choca bounceCeil: " + force);
                Bounce(-force);
            }
            return force != 0;
        }
    }

}