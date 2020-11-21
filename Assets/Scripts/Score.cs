using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    //this gets the TRANSFORM of the player, so just position/rotation/scale data
    public Transform player;
    public Vector3 initialPos;
    public Text scoreText;

    private float score = 0f;
    //used only for tracking a score differential
    private float lastScore = 0f;
    private float highScore;
    private float oldTime = 0f;
    private bool broMode = false;
    public bool cheater = false;

    private string cheaterFlag = "";

    public Text highScoreText;

    public GameManager gameManager;

    void Start() {
        initialPos = player.position;
        highScore = PlayerPrefs.GetFloat("highScore");
        cheater = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(score > 1300)
            gameManager.CompleteLevel();

        lastScore = score;

        //stop scoring
        if(player.position.y < 0) 
            return;

        score = player.position.z - initialPos.z;
        if(score - lastScore > 10) {
            broMode = true;
            scoreText.text = "you cheatin' sunuvagun";
            oldTime = Time.fixedTime;
            cheaterFlag = "(pumpkin eater) ";
        }
        if(broMode && Time.fixedTime - oldTime > 3)
                broMode = false;

        if(!broMode) {
            if(score > highScore)
                highScore = score;
            scoreText.text = score.ToString("0.00");
            highScoreText.text = cheaterFlag + "High: " + highScore.ToString("0.00");
        }
    }

    public float getScore() {
        return score;
    }

}
