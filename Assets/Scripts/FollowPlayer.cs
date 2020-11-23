using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform playerTransform;
    public Rigidbody playerRB;
    public Vector3 cameraShift;
   

    // Update is called once per frame
    void FixedUpdate()
    {


        // RENAME this file.. what is its purpose? think of design


        
        //Vector3 difference = playerRB.position - playerTransform.position;
        //Debug.Log(difference.ToString());


        //cameraShift = playerRB.velocity.normalized;
        //cameraShift.Scale(new Vector3(10,10,10));
        //transform.position = playerRB.position - cameraShift;

        //transform.position = playerTransform.position + cameraShift;
        //transform.SetPositionAndRotation(playerTransform.position + cameraShift, playerTransform.rotation);
        //transform.RotateAround(playerTransform.position, Vector3.up, 90 * Time.deltaTime);
    }


    

}
