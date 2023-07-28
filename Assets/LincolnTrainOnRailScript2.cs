using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class LincolnTrainOnRailsScript2 : MonoBehaviour
{
    public IsaacRailScript CurrentTrack;
    public SpriteShapeController MySSC;
    public Spline mySpline;
    public float yOffset;
    public float progress;
    public float speed;
    public int splineIndex = 0;
    public float DistanceBetween2Points;
    // Start is called before the first frame update
    void Start()
    {
        MySSC = CurrentTrack.tracks[splineIndex];
        mySpline = MySSC.spline;
        progress = 0.0f;
        transform.parent = MySSC.transform;
        DistanceBetween2Points = Vector2.Distance(mySpline.GetPosition(0), mySpline.GetPosition(1));
        //mySpline.
    }


    void Update()
    {

        Vector2 newPosition2D = GetPoint(mySpline, progress + 0.001f);
        Vector3 newPosition = new Vector3(newPosition2D.x, newPosition2D.y, yOffset);
        transform.localPosition = newPosition;

        float distanceToNextPoint = DistanceBetween2Points;

        // Calculate time needed to travel the distance at a constant speed
        float timeToReachNextPoint = distanceToNextPoint / speed;

        // Increment the progress based on time instead of deltaTime
        progress += Time.deltaTime / timeToReachNextPoint;

        if (progress >= 1.0f)
        {
            progress = 0.0f;
            splineIndex += 1;
            if (splineIndex == CurrentTrack.tracks.Count)
            {
                splineIndex = 0;
            }
            MySSC = CurrentTrack.tracks[splineIndex];
            transform.parent = MySSC.transform;
            mySpline = MySSC.spline;
        }
    }

    Vector2 GetPoint(Spline spline, float progress)
    {
        var length = spline.GetPointCount();
        var i = Mathf.Clamp(Mathf.CeilToInt((length - 1) * progress), 0, length - 1);

        var t = progress * (length - 1) % 1f;
        if (i == length - 1 && progress >= 1f)
            t = 1;

        var prevIndex = Mathf.Max(i - 1, 0);

        var _p0 = new Vector2(spline.GetPosition(prevIndex).x, spline.GetPosition(prevIndex).y);
        var _p1 = new Vector2(spline.GetPosition(i).x, spline.GetPosition(i).y);
        var _rt = _p0 + new Vector2(spline.GetRightTangent(prevIndex).x, spline.GetRightTangent(prevIndex).y);
        var _lt = _p1 + new Vector2(spline.GetLeftTangent(i).x, spline.GetLeftTangent(i).y);
        
        DistanceBetween2Points = Vector2.Distance(_p0, _p1);
        
        return BezierUtility.BezierPoint(
           new Vector2(_rt.x, _rt.y),
           new Vector2(_p0.x, _p0.y),
           new Vector2(_p1.x, _p1.y),
           new Vector2(_lt.x, _lt.y),
           t
        );
    }

}
