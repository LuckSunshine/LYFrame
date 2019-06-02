using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectRotationControll : MonoBehaviour
{
    private Vector3 objRat;
    public float RotationSpeed = 300f;

    public float minAngle = 0;

    public float maxAngle = 90;

    private float angle;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        objRat = gameObject.transform.rotation.eulerAngles;
        if (Input.GetMouseButton(0))
        {
            float yRot = Input.GetAxis("Mouse X");
            float xRot = Input.GetAxis("Mouse Y");

            Vector3 R = objRat - new Vector3(-xRot, yRot, 0f);

            objRat = Vector3.Slerp(objRat, R, RotationSpeed);

            transform.rotation = Quaternion.Euler(objRat);
        }
    }
}
