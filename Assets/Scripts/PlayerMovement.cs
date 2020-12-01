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
    public float trackForce = 100f;
    public float maxSpeed = 150f; // Should there be a max speed?

    private float eulerRotationAngle = 0f;
    private Quaternion rotationQuaternion;
    private Vector3 velocityVector;
    private Vector3 movementVector;

    private bool initiateRotating = false;
    private bool initiateRight = false;
    private bool initiateLeft = false;
    private bool isRotating = false;
    private bool isPressingWASD = false;

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

        // Add forward force to velocity vector, rotated accordingly
        velocityVector = new Vector3(0, 0, forwardForce * fixedDeltaTime);
        //rotationQuaternion = Quaternion.Euler(0, eulerRotationAngle, 0); //rotation about y-axis
        //velocityVector = rotationQuaternion * velocityVector;
        //rb.AddForce(velocityVector);

        // This appears to add force in the rotated direction -- no need to rotate the force by the quaternion
        rb.AddRelativeForce(velocityVector);


        if (rb.position.y < -3) 
            FindObjectOfType<GameManager>().EndGame();

        // Note that pressing one of these keys will increase the velocity magnitude,
        // but the Z-axis velocity ("forward") is still constant.
        if (isPressingWASD)
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
        }
        else
        {
            PlayerCollision playerCollision = GetComponent<PlayerCollision>();
            if (playerCollision.isOnTrack)
            {
                Plane trackPlane = playerCollision.trackPlane;
                Debug.Log(trackPlane.ClosestPointOnPlane(transform.position));
                // Add a force toward the current track, whenever player is NOT holding WASD
                rb.AddForce(trackPlane.ClosestPointOnPlane(rb.transform.position) - rb.transform.position);
            }
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