//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.U2D;

//public class LincolnTrainOnRailsScript : MonoBehaviour
//{
//    public IsaacRailScript CurrentTrack;
//    public SpriteShapeController MySSC;
//    public Spline mySpline;
//    public float yOffset;
//    public float speed;
//    public int splineIndex = 0;
//    public int pointIndex = 0;
//    public float distanceTraveled;
//    public List<float> accumulatedDistances = new List<float>();

//    // Start is called before the first frame update
//    void Start()
//    {
//        MySSC = CurrentTrack.tracks[splineIndex];
//        mySpline = MySSC.spline;
//        distanceTraveled = 0.0f;
//        CalculateAccumulatedDistances();
//        transform.parent = MySSC.transform;
//    }

//    void CalculateAccumulatedDistances()
//    {
//        accumulatedDistances.Clear();
//        accumulatedDistances.Add(0f);
//        for (int i = 1; i < mySpline.GetPointCount(); i++)
//        {
//            Vector2 prevPoint = mySpline.GetPosition(i - 1);
//            Vector2 currentPoint = mySpline.GetPosition(i);
//            accumulatedDistances.Add(accumulatedDistances[i - 1] + Vector2.Distance(prevPoint, currentPoint));
//        }
//    }

//    // Update is called once per frame
//    void Update()
//    {
//        Vector2 newPosition2D = GetPointOnSpline(mySpline, distanceTraveled);
//        Vector3 newPosition = new Vector3(newPosition2D.x, newPosition2D.y, yOffset);
//        transform.localPosition = newPosition;

//        float distanceToTravel = speed * Time.deltaTime;
//        float remainingDistance = accumulatedDistances[accumulatedDistances.Count - 1] - distanceTraveled;

//        if (distanceToTravel >= remainingDistance)
//        {
//            splineIndex++;
//            if (splineIndex >= CurrentTrack.tracks.Count)
//            {
//                splineIndex = 0;
//            }

//            MySSC = CurrentTrack.tracks[splineIndex];
//            transform.parent = MySSC.transform;
//            mySpline = MySSC.spline;
//            CalculateAccumulatedDistances();
//            distanceTraveled = distanceToTravel - remainingDistance;
//        }
//        else
//        {
//            distanceTraveled += distanceToTravel;
//        }
//    }

//    Vector2 GetPointOnSpline(Spline spline, float distance)
//    {
//        int index = 0;
//        while (index < accumulatedDistances.Count - 1 && distance >= accumulatedDistances[index + 1])
//        {
//            index++;
//        }

//        float t = Mathf.InverseLerp(accumulatedDistances[index], accumulatedDistances[index + 1], distance);
//        Vector2 p0 = spline.GetPosition(index);
//        Vector2 p1 = spline.GetPosition(index + 1);
//        Vector2 t0 = p0 + spline.GetRightTangent(index);
//        Vector2 t1 = p1 + spline.GetLeftTangent(index);

//        return CalculateCubicBezierPoint(p0, t0, t1, p1, t);
//    }

//    Vector2 CalculateCubicBezierPoint(Vector2 p0, Vector2 p1, Vector2 p2, Vector2 p3, float t)
//    {
//        float u = 1f - t;
//        float tt = t * t;
//        float uu = u * u;
//        float uuu = uu * u;
//        float ttt = tt * t;

//        Vector2 point = uuu * p0;
//        point += 3f * uu * t * p1;
//        point += 3f * u * tt * p2;
//        point += ttt * p3;

//        return point;
//    }
//}
