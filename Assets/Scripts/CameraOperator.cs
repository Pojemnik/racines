using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraOperator : MonoBehaviour
{
    [Header("Setup")]
    [SerializeField]
    public GameObject Self;
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

    Vector3 mouse;
    
    void Start()
    {
        
    }
    void Update()
    {
        Vector3 movement = new Vector3();
        float rotationX = Self.transform.root.rotation.eulerAngles.x;
        float rotationY = Self.transform.root.rotation.eulerAngles.y;

        if (Input.GetKey(KeyCode.W))
        {
            movement += Quaternion.Euler(0, Self.transform.root.rotation.eulerAngles.y, Self.transform.root.rotation.eulerAngles.z) * new Vector3(0, 0, SpeedY * Time.deltaTime);

            //movement.z += SpeedY * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.A))
        {
            movement += Quaternion.Euler(0, Self.transform.root.rotation.eulerAngles.y, Self.transform.root.rotation.eulerAngles.z) * new Vector3(-SpeedX * Time.deltaTime, 0, 0);

            //movement.x -= SpeedX * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S))
        {
            movement += Quaternion.Euler(0, Self.transform.root.rotation.eulerAngles.y, Self.transform.root.rotation.eulerAngles.z) * new Vector3(0, 0, -SpeedY * Time.deltaTime);

            //movement.z -= SpeedY * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.D))
        {
            movement += Quaternion.Euler(0, Self.transform.root.rotation.eulerAngles.y, Self.transform.root.rotation.eulerAngles.z) * new Vector3(SpeedX * Time.deltaTime, 0, 0);

            //movement.x += SpeedX * Time.deltaTime;
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

        mouse = Input.mousePosition;
        Self.transform.root.SetPositionAndRotation(movement + Self.transform.root.position, Quaternion.Euler(rotationX, rotationY, Self.transform.root.rotation.eulerAngles.z));
    }
}
