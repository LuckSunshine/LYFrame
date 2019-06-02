using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CameraCotrel : MonoBehaviour
{

    public float   Speed = 100f;
    public float RotationSpeed = 300f;
    private Vector3 CameraR;

    private Vector3 Face;
    private Vector3 Left;
    private Vector3 Right;

    void Start()
    {
        
    }

    void Update()
    {
        CameraR = Camera.main.transform.rotation.eulerAngles;
        Face    = transform.rotation * Vector3.forward;
        Face    = Face.normalized;

        Left = transform.rotation * Vector3.left;
        Left = Left.normalized;

        Right = transform.rotation * Vector3.right;
        Right = Right.normalized;
        if (Input.GetMouseButton(1))
        {
            //官方脚本
            float yRot = Input.GetAxis("Mouse X");
            float xRot = Input.GetAxis("Mouse Y");

            Vector3 R = CameraR + new Vector3(-xRot, yRot, 0f);

            CameraR = Vector3.Slerp(CameraR, R, RotationSpeed);

            transform.rotation = Quaternion.Euler(CameraR);
        }

        if (Input.GetKey("w"))
        {
            transform.position += Face * Speed * Time.deltaTime;
        }

        if (Input.GetKey("a"))
        {
            transform.position += Left * Speed * Time.deltaTime;
        }

        if (Input.GetKey("d"))
        {
            transform.position += Right * Speed * Time.deltaTime;
        }

        if (Input.GetKey("s"))
        {
            transform.position -= Face * Speed * Time.deltaTime;
        }

        if (Input.GetKey("q"))
        {
            transform.position -= Vector3.up * Speed * Time.deltaTime;
        }

        if (Input.GetKey("e"))
        {
            transform.position += Vector3.up * Speed * Time.deltaTime;
        }

    }
}