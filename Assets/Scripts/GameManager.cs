using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public float restartDelay = 1.5f;
    public PlayerMovement player;
    public GameObject completeLevelUI;
    public Score score;
    public float boundsTop;
    public float boundsBottom;
    public float boundsLeft;
    public float boundsRight; 

    void Start() {
        //PlayerPrefs.DeleteAll();
        Application.targetFrameRate = 60;
    }

    public void CompleteLevel() {
        completeLevelUI.SetActive(true);
    }

    public void EndGame() {
        //player.sidewaysForce = 0f;
        //player.forwardForce = 10f;

        Invoke("Restart", restartDelay);
    }

    void Restart() {
        if(score.getScore() > PlayerPrefs.GetFloat("highScore") && score.cheater == false)
            PlayerPrefs.SetFloat("highScore", score.getScore());
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
