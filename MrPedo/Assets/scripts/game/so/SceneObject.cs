using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class SceneObject : MonoBehaviour
    {
        public void Init(Vector3 pos)
        {
            transform.localPosition = pos;
        }
    }
}
