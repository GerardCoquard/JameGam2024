using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] Transform targetRotate;
    [SerializeField] float cameraSens;
    float rotX = 0;
    float rotY = 0;
    Vector3 currentRot;
    Vector3 currentPos;
    Vector3 smoothVel = Vector3.zero;
    [SerializeField] float smoothTimeRot;
    [SerializeField] float smoothTimeMove;
    Vector3 anteriorMousePos = Vector3.zero;
    Camera camera;
    [SerializeField] LayerMask layer;
    [SerializeField] float moveSpeed;
    [SerializeField] float zoomSpeed;
    [SerializeField] float minZoom;
    [SerializeField] float maxZoom;

    float posX;
    float posY;
    public bool buildingState;
    public bool transitionState;
    Vector3 lastBuildingPos;

    [SerializeField] float timeTransition;
    [SerializeField] AnimationCurve curveTransition;
    float doubleClickTimer = 0;
    Vector3 anteriorDoubleClickPos;
    private void Awake()
    {
        currentRot = targetRotate.localEulerAngles;
        currentPos = targetRotate.position;
        rotX = targetRotate.localEulerAngles.y;
        rotY = targetRotate.localEulerAngles.x;
        posX = targetRotate.position.x;
        posY = targetRotate.position.z;
        camera = GetComponent<Camera>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        doubleClickTimer += Time.deltaTime;
    }

    private void FixedUpdate()
    {
        if (Input.GetMouseButton(0) && Input.GetKey(KeyCode.LeftAlt))
        {
            rotX += Input.GetAxis("Mouse X") * cameraSens;
            rotY -= Input.GetAxis("Mouse Y") * cameraSens;

            rotY = Mathf.Clamp(rotY, 20, 70);

            Vector3 nextRot = new Vector3(rotY, rotX);
            currentRot = Vector3.SmoothDamp(currentRot, nextRot, ref smoothVel, smoothTimeRot);
            targetRotate.localEulerAngles = currentRot;
        }
        if (Input.GetMouseButtonDown(1))
        {
            if (doubleClickTimer < 0.3f)
            {
                Vector3 mousePosition = Input.mousePosition;
                Ray mRay = camera.ScreenPointToRay(mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(mRay, out hit, Mathf.Infinity, layer))
                {
                    StopCoroutine(MoveCameraToCube(anteriorDoubleClickPos));
                    StartCoroutine(MoveCameraToCube(hit.transform.position));
                    anteriorDoubleClickPos = hit.transform.position;
                }
            }
            doubleClickTimer = 0;
        }
        if (Input.GetMouseButton(2))
        {
            //Vector3 mousePosition = Input.mousePosition;
            //Ray mRay = camera.ScreenPointToRay(mousePosition);
            //RaycastHit hit;

            //if (Physics.Raycast(mRay, out hit, Mathf.Infinity, layer))
            //{
            //    Vector3 targetPosition = new Vector3(hit.point.x, 0, hit.point.z);
            //    //targetRotate.position = Vector3.Lerp(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            //    targetRotate.position = targetPosition;
            //    // Optionally, stop moving when very close to the target to prevent jittering
            //    if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
            //    {
            //        targetRotate.position = targetPosition;
            //    }
            //}

            posX = Input.GetAxis("Mouse X");
            posY = Input.GetAxis("Mouse Y");
            Vector3 forward = transform.forward;
            Vector3 right = transform.right;

            // Normalizar los vectores forward y right para que no afecten la velocidad de movimiento
            forward.y = 0;
            right.y = 0;
            forward.Normalize();
            right.Normalize();

            // Calcular la direcci�n deseada en funci�n de la entrada del usuario
            Vector3 desiredMoveDirection = (forward * -posY + right * -posX).normalized;

            // Mover el punto de pivote en la direcci�n deseada
            targetRotate.position += desiredMoveDirection * moveSpeed * Time.deltaTime;
        }

        float scrollInput = Input.mouseScrollDelta.y;

        if (scrollInput != 0.0f)
        {
            // Calcular la nueva distancia
            float distance = Vector3.Distance(transform.position, targetRotate.position);
            distance -= scrollInput * zoomSpeed;
            distance = Mathf.Clamp(distance, minZoom, maxZoom);

            // Mover la c�mara a la nueva posici�n
            Vector3 direction = (transform.position - targetRotate.position).normalized;
            transform.position = targetRotate.position + direction * distance;
        }

    }
    IEnumerator MoveCameraToCube(Vector3 endPos)
    {
        Vector3 startPos = targetRotate.position;
        float time = 0;
        while (time < 0.3f)
        {
            time += Time.deltaTime;
            float percentageDuration = time / timeTransition;       
            targetRotate.position = Vector3.Lerp(startPos, endPos, curveTransition.Evaluate(percentageDuration));
            yield return new WaitForEndOfFrame();
        }
    }
    //public void TransitionExpand()
    //{
    //    lastBuildingPos = targetRotate.position;
    //    StartCoroutine(TransitionExpandCoroutine());

    //}
    //IEnumerator TransitionExpandCoroutine()
    //{
    //    gameManager._gameState = gameState.transition;
    //    transitionState = true;
    //    float time = 0;
    //    Quaternion startRot = targetRotate.rotation;
    //    Vector3 startPos = targetRotate.position;
    //    Vector3 startPosCam = transform.localPosition;
    //    Vector3 direction = (transform.position - targetRotate.position).normalized;
    //    Vector3 endPosCam = new Vector3(transform.localPosition.x,transform.localPosition.y,-1700);
    //    while (time < timeTransition)
    //    {
    //        time += Time.deltaTime;
    //        float percentageDuration = time / timeTransition;
    //        targetRotate.localRotation = Quaternion.Lerp(startRot, Quaternion.Euler(55,-45,0), curveTransition.Evaluate(percentageDuration));
    //        targetRotate.position = Vector3.Lerp(startPos, Vector3.zero, curveTransition.Evaluate(percentageDuration));
    //        transform.localPosition = Vector3.Lerp(startPosCam, endPosCam, curveTransition.Evaluate(percentageDuration));

    //        yield return new WaitForEndOfFrame();
    //    }
    //    rotX = targetRotate.localEulerAngles.y;
    //    rotY = targetRotate.localEulerAngles.x;
    //    posX = targetRotate.position.x;
    //    posY = targetRotate.position.z;
    //    gameManager._gameState = gameState.expand;
    //}
}
