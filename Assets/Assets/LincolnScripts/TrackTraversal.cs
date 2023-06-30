using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class TrackTraversal : MonoBehaviour
{
    public SpriteShapeController shapeController;
    public float TraversalSpeed;
    public Spline MySpline;
    public float currentProgress = 0;
    public float offset = 0.5f;
    private Vector3 offsetV3;
    // Start is called before the first frame update
    void Start()
    {
        offsetV3 = new Vector3(0, 0, -offset);
        MySpline = shapeController.spline;
        transform.parent = shapeController.transform;
        
        Vector2 StartPos = GetPoint(MySpline, 0f);
        //transform.localPosition = new Vector3(StartPos.x, offset ,StartPos.y);
        transform.localPosition = MySpline.GetPosition(0);
        transform.localPosition += offsetV3;
        // transform.localPosition = new Vector3(StartPos.x, StartPos.y, offset);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public static Vector2 GetPoint(Spline spline, float progress)
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

        return BezierUtility.BezierPoint(
           new Vector2(_rt.x, _rt.y),
           new Vector2(_p0.x, _p0.y),
           new Vector2(_p1.x, _p1.y),
           new Vector2(_lt.x, _lt.y),
           t
        );
    }
}
