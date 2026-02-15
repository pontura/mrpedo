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
        float next_area_x;
        float next_level_x;
        Vector3 offset = new Vector3(40,0,0);
        int levelID = 0;

        [SerializeField] Transform container;
        [SerializeField] Character character;
        LevelCreator levelCreator;
        LevelData level;

        void Awake()
        {
            levelCreator = GetComponent<LevelCreator>();
            NewLevel();
        }
        void FixedUpdate()
        {
            _x = character.transform.position.x;
            if (_x> next_area_x)
                NextLevel();
            levelCreator.CheckOutOfView(_x);
        }
        void NewLevel()
        {
            levelID++;
            level = levels[levelID-1];
            next_level_x = level.width;
            print("NewLevel" + next_level_x + " character X: " + _x);
        }
        void NextLevel()
        {
            Vector3 pos = offset + (Vector3.right * next_area_x);
            if (next_area_x > next_level_x)
                NewLevel();

            AreaData area = level.GetRandomArea();
            area.Init();
            levelCreator.Init(area, pos);
            print("NextLevel" + next_level_x + " character X: " + _x + " next area: " + (next_area_x + area.width));
            next_area_x += area.width;
        }
    }

}