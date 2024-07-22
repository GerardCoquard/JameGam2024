using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;
using Unity.Mathematics;
using UnityEditor;

public static class SplineRoadSample
{   
    public static int NumSplines(SplineContainer splineContainer)
    {
        return splineContainer.Splines.Count;
    }

    public static void SampleSpline(int splineIndex, float t, out Vector3 _pos, out Vector3 _upVector, out Vector3 _forward, SplineContainer splineContainer)
    {
        float3 pos;
        float3 upVector;
        float3 forward;

        splineContainer.Evaluate(splineIndex,t, out pos, out forward, out upVector);

        _pos = pos;
        _upVector = upVector;
        _forward = forward;
    }    
}
