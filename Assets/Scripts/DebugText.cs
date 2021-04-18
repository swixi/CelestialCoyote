using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugText : MonoBehaviour
{
    public Text fpsText;
    public PlayerMovement player;

    void Update() {
        fpsText.text = "fps: " + (1.0f/Time.smoothDeltaTime).ToString("0.0");
        fpsText.text += "\n";
        fpsText.text += "vel: " + (player.rb.velocity).ToString();
        fpsText.text += "\n";
        fpsText.text += "vel mag: " + (player.rb.velocity.magnitude).ToString("0.00");
        fpsText.text += "\n";
        fpsText.text += "on track: " + player.GetComponent<PlayerCollision>().isOnTrack;
    }
}
