using UnityEngine;
namespace Game
{
    public class ParallaxLayer : MonoBehaviour
    {
        [Header("Scroll")]
        [HideInInspector] public float scrollSpeed = 2f;
        [SerializeField] private float parallaxMultiplier = 0.5f;

        [SerializeField] private float spriteWidth;
        [SerializeField] private Transform cam;


        void Update()
        {
            float movement = scrollSpeed * parallaxMultiplier * Time.deltaTime;
            transform.position += Vector3.left * movement;

            float cameraLeftEdge = cam.position.x - (Camera.main.orthographicSize * Camera.main.aspect);

            // Si el sprite salió completamente por la izquierda de la cámara
            if (transform.position.x + spriteWidth < cameraLeftEdge)
            {
                transform.position += Vector3.right * spriteWidth;
            }
        }
    }
}
