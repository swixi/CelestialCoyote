using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FPSCounter : MonoBehaviour
{

    public Text fpsText;

    void Update() {
        fpsText.text = (1.0f/Time.smoothDeltaTime).ToString("0.0");
        
    }
}
