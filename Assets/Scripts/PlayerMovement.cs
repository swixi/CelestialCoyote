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
    public float maxSpeed = 150f;

    private bool initiateRotating = false;
    private bool isRotating = false;

    // Start is called before the first frame update
    void Start()
    {
        initPos = rb.position;
        //rb.AddForce(0, 200, 500);
    }

    void Update() 
    {
        if(Input.GetKey("f") && rb.position.y < 2)
            rb.AddForce(0, jumpForce, 0, ForceMode.VelocityChange);

        initiateRotating = Input.GetKeyDown("right") || Input.GetKeyDown("left");
        if (!initiateRotating)
            isRotating = false;

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

        // If there is a constant speed forward, this may cause weird behavior, because that speed is included in the vel vector
        // Maybe get magnitude without forward speed here?
        if (rb.velocity.magnitude < maxSpeed)
        {
            if (Input.GetKey("d"))
                rb.AddForce(sidewaysForce * Time.deltaTime, 0, 0, ForceMode.VelocityChange);
            if (Input.GetKey("a") || Input.GetKey("left"))
                rb.AddForce(-sidewaysForce * Time.deltaTime, 0, 0, ForceMode.VelocityChange);
            if (Input.GetKey("s") || Input.GetKey("down"))
                rb.AddForce(0, -sidewaysForce * Time.deltaTime, 0, ForceMode.VelocityChange);
            if (Input.GetKey("w") || Input.GetKey("up"))
                rb.AddForce(0, sidewaysForce * Time.deltaTime, 0, ForceMode.VelocityChange);
        }

        // Even if the player holds down a rotate button, only do an initial rotation
        if (initiateRotating && !isRotating)
        {
            transform.rotation *= Quaternion.Euler(0, 45f, 0);
            rb.rotation *= Quaternion.Euler(0, 45f, 0);
            
            isRotating = true;
        }
    }

    public void resetPosition() 
    {
        rb.position = initPos;
        rb.velocity = Vector3.zero;
    }
}