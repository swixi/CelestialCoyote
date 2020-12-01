
using UnityEngine;

public class PlayerCollision : MonoBehaviour {

    public PlayerMovement movement;
    public bool isOnTrack = false;
    public Plane trackPlane { get; set; }

    private void BuildTrackPlane(GameObject track)
    {
        MeshFilter filter = track.GetComponent<MeshFilter>();
        Vector3 normal;

        if (filter && filter.mesh.normals.Length > 0)
        {
            normal = filter.transform.TransformDirection(filter.mesh.normals[0]);
            trackPlane = new Plane(normal, filter.transform.position);
        }
        else 
        {
            Debug.Log("Failure to create track plane!");
        }
        
    }

    void OnCollisionEnter(Collision collisionInfo) {
        if(collisionInfo.collider.tag == "obstacle") {
            Collider collider = collisionInfo.collider;
            
            /*
            AudioSource audioSource = collider.gameObject.AddComponent<AudioSource>();
            audioSource.clip = Resources.Load("Sounds/C.wav") as AudioClip;
            audioSource.Play();
            */

            //FindObjectOfType<GameManager>().EndGame();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "track")
        {
            isOnTrack = true;
            BuildTrackPlane(other.gameObject);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "track")
        {
            isOnTrack = true;
            //Debug.Log(other.);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "track")
        {
            isOnTrack = false;
        }
    }


}
