using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveEnemy : MonoBehaviour
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
    public float distanceToEnd = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        moveDistance += speed * Time.deltaTime;
        distanceToEnd = moveDistance / pathInfo.distance;
        Debug.Log(distanceToEnd);
        SplineRoadSample.SampleSpline(0, distanceToEnd, out Vector3 pos, out Vector3 up,out Vector3 forward, pathInfo.splineContainer);
        float time = (Time.time % durationJump) / durationJump;
        publicPos = pos;
        transform.position = new Vector3(pos.x, pos.y + jumpCurve.Evaluate(time),pos.z);
        transform.forward = forward;      
    }
    
}
