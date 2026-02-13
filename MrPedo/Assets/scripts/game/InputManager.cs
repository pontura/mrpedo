using UnityEngine;
using UnityEngine.TextCore.Text;

namespace Game
{
    public class InputManager : MonoBehaviour
    {
        Character character;
        float _lastY;
        void Start()
        {
            character = GetComponent<Character>();
        }
        void Update()
        {
            if (Input.GetMouseButton(0))
            {
                if(_lastY == 0)
                    _lastY = Input.mousePosition.y;

                float mouseY = Input.mousePosition.y;
                character.OnPedo((mouseY - _lastY) * -1);
                _lastY = mouseY;
            } else
            {
                _lastY = 0;
                character.OnPedo(0);
            }
        }
    }
}
