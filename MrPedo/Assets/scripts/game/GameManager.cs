using UnityEngine;

namespace Game
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

        float acceleration = 0.025f;
        [SerializeField] private float forwardSpeed = 4f;
        [SerializeField] Character character;
        [SerializeField] LevelsManager levelsManager;
        [SerializeField] ParallaxLayer[] parallaxLayers;
        public ObjectPool pool;

        private void Awake()
        {
            Instance = this;
        }
        void Start()
        {
            Loop();
        }
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
