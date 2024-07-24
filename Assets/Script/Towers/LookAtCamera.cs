using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    Transform camera;
    private void Awake()
    {
        camera = FindObjectOfType<Camera>().transform;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(camera);
        transform.forward = -transform.forward;
    }
}
