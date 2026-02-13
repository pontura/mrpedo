using UnityEngine;
namespace Game
{

    [RequireComponent(typeof(Rigidbody2D))]
    public class Character : MonoBehaviour
    {
        [SerializeField] private Vector2 limits = new Vector2(-4.26f, 7.82f);
        [SerializeField] float forwardSpeed = 5f;

        [Header("Jetpack")]
        [SerializeField] private float gravityScale = 1;
        [SerializeField] private float jetpackForce = 15f;
        [SerializeField] private float maxFallSpeed = -20f;
        [SerializeField] private float maxRiseSpeed = 10f;
        [SerializeField] private float bounceFloor = 6f;
        [SerializeField] private float bounceCeil = 2f;

        public Rigidbody2D rb;
        [SerializeField] float upForce;

        void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            rb.gravityScale = gravityScale;
        }
        public void SetSpeed(float _forwardSpeed)
        {
            forwardSpeed = _forwardSpeed;
        }
        public void OnPedo( float upForce)
        {
            this.upForce = upForce;
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
            if (upForce > 0)
            {
                print("sube: " + jetpackForce * (1 + upForce));
                rb.AddForce(Vector2.up * jetpackForce * (1+upForce), ForceMode2D.Force);
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
                Bounce(-force);
            } else if (transform.position.y > limits.y)
            {
                force = bounceCeil * (rb.linearVelocityY * 10);
                Bounce(-force);
            }
            return force != 0;
        }
    }

}