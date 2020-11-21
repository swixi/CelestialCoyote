using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody rb;
    //int tempSec = 0;
    public Vector3 initPos;

    public float forwardForce = 2000f;
    public float sidewaysForce = 500f;
    public float jumpForce = 1000f;

    // Start is called before the first frame update
    void Start()
    {
        initPos = rb.position;
        //rb.AddForce(0, 200, 500);
    }

    void Update() {
        if(Input.GetKey("f") && rb.position.y < 2)
            rb.AddForce(0, jumpForce, 0, ForceMode.VelocityChange);
    }

    // FixedUpdate is good for physics apparently
    void FixedUpdate()
    {
        /*
        if((int)Time.fixedTime > tempSec) {
            Debug.Log("Another second " + tempSec);
            Debug.Log(rb.velocity);
            Debug.Log(rb.velocity.magnitude);
            tempSec = (int)Time.fixedTime;
        }
        */

        rb.AddForce(0, 0, forwardForce * Time.deltaTime);

        if (rb.position.y < -3) 
            FindObjectOfType<GameManager>().EndGame();

        if(Input.GetKey("d") || Input.GetKey("right"))
            rb.AddForce(sidewaysForce * Time.deltaTime, 0, 0, ForceMode.VelocityChange);
        if(Input.GetKey("a") || Input.GetKey("left"))
            rb.AddForce(-sidewaysForce * Time.deltaTime, 0, 0, ForceMode.VelocityChange);
    }

    public void resetPosition() {
        rb.position = initPos;
        rb.velocity = Vector3.zero;
    }
}