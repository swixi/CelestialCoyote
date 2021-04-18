
using UnityEngine;

public class PlayerCollision : MonoBehaviour {

    public PlayerMovement movement;
    public bool isOnTrack = false;
    public Plane trackPlane { get; set; }

    // Build a Plane (which is just a struct) from a Plane (Game Object with mesh renderer, etc)
    // The struct Plane represents the mathematical (infinite) plane which is determined by a normal vector and a point on the plane
    // This is useful to have because it has math methods like "closest point on plane" so we can avoid re-inventing the wheel
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

            //TODO: check rotation of plane and player, and set the correct one to build
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
