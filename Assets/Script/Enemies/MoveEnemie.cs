using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveEnemie : MonoBehaviour
{
    public PathInfo pathInfo;
    float moveDistance = 0;
    public float speed;
    public bool slowed;
    [SerializeField] AnimationCurve jumpCurve;
    [SerializeField] float durationJump;
    [SerializeField] AnimationCurve tiltCurve;
    [SerializeField] float durationtilt;
    public Vector3 publicPos;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        moveDistance += speed * Time.deltaTime;
        SplineRoadSample.SampleSpline(0, moveDistance / pathInfo.distance, out Vector3 pos, out Vector3 up,out Vector3 forward, pathInfo.splineContainer);
        float time = (Time.time % durationJump) / durationJump;
        publicPos = pos;
        transform.position = new Vector3(pos.x, pos.y + jumpCurve.Evaluate(time),pos.z);
        transform.forward = forward;
    }
}
