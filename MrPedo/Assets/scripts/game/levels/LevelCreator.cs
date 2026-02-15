using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
namespace Game
{
    public class LevelCreator : MonoBehaviour
    {
        [SerializeField] Transform container;
        public int totalSamples = 20;
        public GrabbableSO grabbableSO;

        public void Init(LevelData ld, Vector3 pos)
        {
            foreach (SpriteShapeController spriteShape in ld.GetGrabbablesLines())
            {
                List<Vector3> points = SampleSpline(spriteShape, totalSamples);

                foreach (Vector3 p in points)
                {
                    GrabbableSO so = Instantiate(grabbableSO, container);
                    so.Init(p + pos);
                    Debug.Log(p);
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
