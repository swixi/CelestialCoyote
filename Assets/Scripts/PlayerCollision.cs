
using UnityEngine;

public class PlayerCollision : MonoBehaviour {

    public PlayerMovement movement;
    
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
    
}
