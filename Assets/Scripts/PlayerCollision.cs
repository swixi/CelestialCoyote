
using UnityEngine;

public class PlayerCollision : MonoBehaviour {

    public PlayerMovement movement;
    
    void OnCollisionEnter(Collision collisionInfo) {
        if(collisionInfo.collider.tag == "obstacle") {
            FindObjectOfType<GameManager>().EndGame();
        }
    }
    
}
