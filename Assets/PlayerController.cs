using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5.0f;
    public float turnSpeed = 200.0f;

    private Rigidbody rb;

    // camera 

    private float x;
    private float y;
    private Vector3 rotateValue;

    void Start()
    {
        
        rb = GetComponent<Rigidbody>();
        
    }

    
    void FixedUpdate()
    {
        
        if (Input.GetKey(KeyCode.W))
        {
            rb.MovePosition(rb.position + transform.forward * Time.fixedDeltaTime * speed);
        }

        
        if (Input.GetKey(KeyCode.S))
        {
            rb.MovePosition(rb.position - transform.forward * Time.fixedDeltaTime * speed);
        }
        if (Input.GetKey(KeyCode.D))
        {

        }

        // camera 
        y = Input.GetAxis("Mouse X");
        x = Input.GetAxis("Mouse Y");
        Debug.Log(x + ":" + y);
        rotateValue = new Vector3(x, y * -1, 0);
        transform.eulerAngles = transform.eulerAngles - rotateValue;


    }
}
