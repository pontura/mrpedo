using UnityEngine;

public class ParallaxLayer : MonoBehaviour
{
    [SerializeField] private float parallaxMultiplier = 0.5f;

    [SerializeField] private float spriteWidth;
    [SerializeField] private Transform cam;
    float playerSpeedX;
    public void OnUpdate(float playerSpeedX)
    {
        this.playerSpeedX = playerSpeedX;
    }
    private void FixedUpdate()
    {        
        print(playerSpeedX);
        float movement = playerSpeedX * parallaxMultiplier * Time.deltaTime;

        transform.position += Vector3.right * movement;

        float cameraLeftEdge = cam.position.x -
            (Camera.main.orthographicSize * Camera.main.aspect);

        if (transform.position.x + spriteWidth < cameraLeftEdge)
        {
            transform.position += Vector3.right * spriteWidth;
        }
    }
}
