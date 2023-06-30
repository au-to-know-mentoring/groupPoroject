using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class trainOnRailsScript : MonoBehaviour
{

    public SpriteShapeController MySSC;
    public Spline mySpline;

    // Start is called before the first frame update
    void Start()
    {
        mySpline = MySSC.spline;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
