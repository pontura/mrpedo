using UnityEngine;
using UnityEngine.U2D;

namespace Game
{
    public class LevelData : MonoBehaviour
    {
        public int width = 10;
        [SerializeField] SceneObject[] sceneObjects;
        public SpriteShapeController[] grabbables;

        public void Init()
        {
            if(sceneObjects.Length == 0)
            sceneObjects = GetComponentsInChildren<SceneObject>();
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