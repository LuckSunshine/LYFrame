using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayTest : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                print(hit.collider.gameObject.name+" "+ hit.collider.gameObject.transform.position);

                Debug.DrawRay(transform.position, hit.point*100, Color.red);

            }
        }
    }
}
