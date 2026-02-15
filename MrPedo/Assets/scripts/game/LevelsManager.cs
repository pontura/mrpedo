using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.U2D;
using UnityEngine.UI;
namespace Game
{
    public class LevelsManager : MonoBehaviour
    {
        [SerializeField] LevelData[] levels;
        float _x;
        float next_x;
        Vector3 offset = new Vector3(40,0,0);

        [SerializeField] Transform container;
        [SerializeField] Character character;
        LevelCreator levelCreator;

        void Awake()
        {
            levelCreator = GetComponent<LevelCreator>();
        }
        void FixedUpdate()
        {
            _x = character.transform.position.x;
            if (_x> next_x)
                NextLevel();
            levelCreator.CheckOutOfView(_x);
        }
        void NextLevel()
        {
            Vector3 pos = offset + (Vector3.right * next_x);
            LevelData level = levels[Random.Range(0, levels.Length)];
            level.Init();
            levelCreator.Init(level, pos);
            print("NextLevel" + _x + " next: " + (next_x + level.width));        
            next_x += level.width;
        }
    }

}