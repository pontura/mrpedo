using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.U2D;
namespace Game
{
    public class LevelCreator : MonoBehaviour
    {
        [SerializeField] Transform container;
        public int totalSamples = 20;
        public List<SceneObject> all;

        public void CheckOutOfView(float character_pos_x)
        {
            if (all.Count < 1) return;
            SceneObject so = all[0];
            if (so.transform.position.x < character_pos_x - 15f)
            {
                all.RemoveAt(0);
                GrabbableSO grabbable = so.GetComponent<GrabbableSO>();
                if (grabbable)
                {
                    GameManager.Instance.pool.ReturnGrabbable(so.GetComponent<GrabbableSO>());
                }
                else
                {
                    Destroy(so.gameObject);
                }
            }
        }
        public void Init(LevelData level, Vector3 pos)
        {
            SceneObject[] allInLevel = level.GetSceneObjects();
            foreach (SceneObject so in allInLevel)
            {
                SceneObject newSO = Instantiate(so);
                newSO.Init(so.transform.localPosition + pos);
                newSO.transform.parent = container;
                all.Add(newSO);
                print("___" + so.transform.localPosition + pos);
            }
            foreach (SpriteShapeController spriteShape in level.GetGrabbablesLines())
            {
                List<Vector3> points = SampleSpline(spriteShape, totalSamples);

                foreach (Vector3 p in points)
                {
                    GrabbableSO newSO = GameManager.Instance.pool.GetGrabbable();
                    newSO.Init(p + pos);
                    newSO.transform.SetParent(container);
                    all.Add(newSO);
                }
                spriteShape.enabled = false;
            }
        }

        List<Vector3> SampleSpline(SpriteShapeController spriteShape, int samples)
        {
            List<Vector3> result = new List<Vector3>();

            Spline spline = spriteShape.spline;
            int segmentCount = spline.GetPointCount() - 1;

            for (int s = 0; s < samples; s++)
            {
                float globalT = s / (float)(samples - 1);

                // Determinar qué segmento corresponde
                float scaledT = globalT * segmentCount;
                int segmentIndex = Mathf.Min(Mathf.FloorToInt(scaledT), segmentCount - 1);

                float localT = scaledT - segmentIndex;

                result.Add(EvaluateSegment(spriteShape, spline, segmentIndex, localT));
            }

            return result;
        }

        Vector3 EvaluateSegment(SpriteShapeController spriteShape, Spline spline, int i, float t)
        {
            Vector3 p0 = spline.GetPosition(i);
            Vector3 p3 = spline.GetPosition(i + 1);

            Vector3 p1 = p0 + spline.GetRightTangent(i);
            Vector3 p2 = p3 + spline.GetLeftTangent(i + 1);

            Vector3 localPos = Bezier(p0, p1, p2, p3, t);

            return spriteShape.transform.TransformPoint(localPos);
        }

        Vector3 Bezier(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t)
        {
            float u = 1f - t;
            return u * u * u * p0 +
                   3f * u * u * t * p1 +
                   3f * u * t * t * p2 +
                   t * t * t * p3;
        }
    }
}
