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
        Vector3 offset = new Vector3(20,0,0);

        [SerializeField] Transform container;
        [SerializeField] Character character;
        LevelCreator levelCreator;

        private void Start()
        {
            levelCreator = GetComponent<LevelCreator>();
        }
        void FixedUpdate()
        {
            _x = character.transform.position.x;
            if (_x> next_x)
                NextLevel();
        }
        void NextLevel()
        {
            Vector3 pos = offset + (Vector3.right * next_x);
            LevelData level = Instantiate(levels[Random.Range(0, levels.Length)], container);
            levelCreator.Init(level, pos);

            print("NextLevel" + _x + " next: " + next_x + level.width);

            level.Init();
            level.transform.position += pos;            
            next_x += level.width;
        }
    }

}