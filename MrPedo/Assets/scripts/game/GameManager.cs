using UnityEngine;

namespace Game
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private float forwardSpeed = 5f;
        [SerializeField] Character character;
        [SerializeField] ParallaxLayer[] parallaxLayers;
        void Start()
        {
            character.forwardSpeed = forwardSpeed;
            foreach (var layer in parallaxLayers)
            {
                layer.scrollSpeed = forwardSpeed;
            }
        }
        void Update()
        {
            bool pedo = Input.GetKey(KeyCode.Space);
            character.OnPedo(pedo);
        }
    }
}
