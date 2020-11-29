using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlayerMovement : MonoBehaviour
{
    public Rigidbody rb;
    public Vector3 initPos;

    public float forwardForce = 2000f;
    public float sidewaysForce = 500f;
    public float jumpForce = 1000f;
    public float maxSpeed = 150f;

    private float eulerRotationAngle = 0f;
    private Quaternion rotationQuaternion;
    private Vector3 velocityVector;
    private Vector3 movementVector;

    private bool initiateRotating = false;
    private bool initiateRight = false;
    private bool initiateLeft = false;
    private bool isRotating = false;
    private bool isPressingWASD = false;

    private float timer = 0.0f;
    private float waitTime = 1.0f;

    private float fixedDeltaTime = 0f;
    

    // Start is called before the first frame update
    void Start()
    {
        initPos = rb.position;
        //rb.velocity = new Vector3(0, 0, 1);
    }

    void Update() 
    {
        if(Input.GetKey("f") && rb.position.y < 2)
            rb.AddForce(0, jumpForce, 0, ForceMode.VelocityChange);

        if (Input.GetKey("e"))
        {
            resetRotationAndVelocity();
        }

        // Just to be safe, we really only want to check Input.GetKeyDown once, 
        // in case we check later for what key was pressed by calling the function again, and the player has let go
        initiateLeft = Input.GetKeyDown("left");
        initiateRight = Input.GetKeyDown("right");
        initiateRotating = initiateLeft || initiateRight;
        if (!initiateRotating)
            isRotating = false;

        if (Input.GetKey("w") || Input.GetKey("a") || Input.GetKey("s") || Input.GetKey("d"))
            isPressingWASD = true;
        else
            isPressingWASD = false;

    }

    // FixedUpdate is good for physics apparently
    void FixedUpdate()
    {
        fixedDeltaTime = Time.fixedDeltaTime;

        /*
        if (timer == 0)
        {
            rb.position = new Vector3(-100, 0, 0);
            //Debug.Log("pos at time 0: " + rb.position);
            rb.velocity = Vector3.one;
            //Debug.Log("vel at time 0: " + rb.velocity);
        }

        timer += Time.deltaTime;

        if (timer > waitTime)
        {
            Debug.Log("vel after " + timer + " sec: " + rb.velocity);
            Debug.Log("pos after " + timer + " sec: " + rb.position);
        }
        */


        // Add forward force to velocity vector, rotated accordingly
        velocityVector = new Vector3(0, 0, forwardForce * fixedDeltaTime);
        //rotationQuaternion = Quaternion.Euler(0, eulerRotationAngle, 0); //rotation about y-axis
        //velocityVector = rotationQuaternion * velocityVector;
        //rb.AddForce(velocityVector);

        // This appears to add force in the rotated direction -- no need to rotate the force by the quaternion
        rb.AddRelativeForce(velocityVector);


        if (rb.position.y < -3) 
            FindObjectOfType<GameManager>().EndGame();

        // If there is a constant speed forward, this may cause weird behavior, because that speed is included in the vel vector
        // Maybe get magnitude without forward speed here?
        if (rb.velocity.magnitude < maxSpeed && isPressingWASD)
        {
            if (Input.GetKey("d"))
                rb.AddRelativeForce(new Vector3(sidewaysForce * fixedDeltaTime, 0, 0), ForceMode.Impulse);
            if (Input.GetKey("a"))
                rb.AddRelativeForce(new Vector3(-sidewaysForce * fixedDeltaTime, 0, 0), ForceMode.Impulse);
            if (Input.GetKey("s"))
                rb.AddRelativeForce(new Vector3(0, -sidewaysForce * fixedDeltaTime, 0), ForceMode.Impulse);
            if (Input.GetKey("w"))
                rb.AddRelativeForce(new Vector3(0, sidewaysForce * fixedDeltaTime, 0), ForceMode.Impulse);

            //rb.AddRelativeForce(movementVector, ForceMode.Impulse);
            //Debug.Log("adding force + " + Time.time);
        }

        // Even if the player holds down a rotate button, only do an initial rotation
        if (initiateRotating && !isRotating)
        {
            // This is slightly annoying. It's best practice to use transform.Rotate, but we only do this 45 Euler degrees at a time
            // On the other hand, we kind of want to keep track of the total Euler rotation so we can have a single rotation quaternion to multiply by for e.g. velocity vector
            if (initiateLeft)
            {
                eulerRotationAngle -= 45f;
                transform.Rotate(new Vector3(0, -45f, 0));
            }
            if (initiateRight)
            {
                eulerRotationAngle += 45f;
                transform.Rotate(new Vector3(0, 45f, 0));
            }

            Debug.Log(eulerRotationAngle);

            
            //transform.Rotate(new Vector3(0, eulerRotationAngle, 0));
            //transform.rotation = Quaternion.LookRotation(rb.velocity);

            //transform.rotation *= rotationQuaternion;
            //rb.rotation *= rotationQuaternion;
            //rb.rotation *= Quaternion.Euler(0, 45f, 0);
            //rb.velocity = Quaternion.Euler(0, velocityRotation, 0) * rb.velocity;

            isRotating = true;
        }
    }

    public void resetPosition() 
    {
        rb.position = initPos;
        rb.velocity = Vector3.zero;
    }

    private void resetRotationAndVelocity()
    {
        rb.velocity = Vector3.zero;
        transform.rotation = Quaternion.identity;
        rotationQuaternion = Quaternion.identity;
        eulerRotationAngle = 0f;
    }
}