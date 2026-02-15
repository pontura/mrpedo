using UnityEngine;
using UnityEngine.U2D;

namespace Game
{
    public class AreaData : MonoBehaviour
    {
        public int width = 10;
        [SerializeField] SceneObject[] sceneObjects;
        public SpriteShapeController[] grabbables;

        public void Init()
        {
            print("tengo: " + sceneObjects.Length + " pos: " + transform.position + " NAME " + gameObject.name);
        }
        public SceneObject[] GetSceneObjects()
        {
            return sceneObjects;
        }
        public SpriteShapeController[] GetGrabbablesLines()
        {
            return grabbables;
        }
    }
}