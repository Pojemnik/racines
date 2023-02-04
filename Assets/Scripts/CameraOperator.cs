using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]

public class CameraOperator : MonoBehaviour
{
    [Header("Setup")]
    [SerializeField]
    public float SpeedX;
    [SerializeField]
    public float SpeedY;
    [SerializeField]
    public float RotationSpeed;
    [SerializeField]
    public float TopRotationLimit;
    [SerializeField]
    public float BottomRotationLimit;
    [SerializeField]
    public float ZoomStep;
    [SerializeField]
    public float MinimalZoom;
    [SerializeField]
    public float MaximalZoom;

    Camera selfCamera;
    Vector3 mouse;
    
    void Start()
    {
        selfCamera = GetComponent<Camera>();
    }
    void Update()
    {
        Vector3 movement = new Vector3();
        float rotationX = transform.rotation.eulerAngles.x;
        float rotationY = transform.rotation.eulerAngles.y;

        if (Input.GetKey(KeyCode.W))
        {
            movement += Quaternion.Euler(0, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z) * new Vector3(0, 0, SpeedY * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.A))
        {
            movement += Quaternion.Euler(0, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z) * new Vector3(-SpeedX * Time.deltaTime, 0, 0);
        }
        if (Input.GetKey(KeyCode.S))
        {
            movement += Quaternion.Euler(0, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z) * new Vector3(0, 0, -SpeedY * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.D))
        {
            movement += Quaternion.Euler(0, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z) * new Vector3(SpeedX * Time.deltaTime, 0, 0);
        }
        if (Input.GetKey(KeyCode.Mouse1))
        {
            rotationY += Time.deltaTime * -RotationSpeed * (Input.mousePosition - mouse).x;
            rotationX += Time.deltaTime * RotationSpeed * (Input.mousePosition - mouse).y;
            if (rotationX >= TopRotationLimit)
            {
                rotationX = TopRotationLimit;
            }
            if (rotationX <= BottomRotationLimit)
            {
                rotationX = BottomRotationLimit;
            }
        }
        if (Input.mouseScrollDelta.y != 0)
        {
            if(selfCamera.fieldOfView - ZoomStep * Input.mouseScrollDelta.y > MaximalZoom)
            {
                selfCamera.fieldOfView = MaximalZoom;
            }
            if(selfCamera.fieldOfView - ZoomStep * Input.mouseScrollDelta.y < MinimalZoom)
            {
                selfCamera.fieldOfView = MinimalZoom;
            }
            if(selfCamera.fieldOfView - ZoomStep * Input.mouseScrollDelta.y <= MaximalZoom && selfCamera.fieldOfView - ZoomStep * Input.mouseScrollDelta.y >= MinimalZoom)
            {
                selfCamera.fieldOfView = selfCamera.fieldOfView - ZoomStep * Input.mouseScrollDelta.y;
            }
        }

        mouse = Input.mousePosition;
        transform.SetPositionAndRotation(movement + transform.position, Quaternion.Euler(rotationX, rotationY, transform.rotation.eulerAngles.z));
    }
}
