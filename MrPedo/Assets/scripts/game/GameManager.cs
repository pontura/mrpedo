using UnityEngine;
using UnityEngine.TextCore.Text;

namespace Game
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private float forwardSpeed = 4f;
        [SerializeField] Character character;
        [SerializeField] LevelsManager levelsManager;
        [SerializeField] ParallaxLayer[] parallaxLayers;
        void Start()
        {
            Loop();
        }
        float acceleration = 0.01f;
        private void Loop()
        {
            forwardSpeed += acceleration;

            character.SetSpeed(forwardSpeed);

            foreach (ParallaxLayer layer in parallaxLayers)
                layer.OnUpdate(character.rb.linearVelocity.x);

            Invoke("Loop", 0.5f);   
        }
    }
}
