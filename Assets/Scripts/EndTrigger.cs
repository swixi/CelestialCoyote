using UnityEngine;

public class EndTrigger : MonoBehaviour
{
    public GameManager gameManager;

    void OnTriggerEnter(Collider collider) {
        if(collider.tag == "player")
            gameManager.CompleteLevel();
    }
    
}
