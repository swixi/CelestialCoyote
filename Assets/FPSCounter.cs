using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FPSCounter : MonoBehaviour
{

    public Text fpsText;
    public PlayerMovement player;

    void Update() {
        fpsText.text = "fps: " + (1.0f/Time.smoothDeltaTime).ToString("0.0");
        fpsText.text += "\n";
        fpsText.text += "vel: " + (player.rb.velocity.magnitude).ToString("0.00");
        
    }
}
