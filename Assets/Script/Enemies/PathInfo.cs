using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;
using System.Linq;

public class PathInfo : MonoBehaviour
{
    [HideInInspector]
    public SplineContainer splineContainer;
    [HideInInspector]
    public float distance;
    // Start is called before the first frame update
    void Start()
    {
        splineContainer = GetComponent<SplineContainer>();
        for (int i = 1; i < splineContainer.Spline.Knots.Count(); i++)
        {
            distance += Vector3.Distance(splineContainer.Spline.ToArray()[i - 1].Position, splineContainer.Spline.ToArray()[i].Position);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
