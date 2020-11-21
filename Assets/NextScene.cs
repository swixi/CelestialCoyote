using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextScene : MonoBehaviour
{
    void Start()
    {
        PlayerPrefs.DeleteAll();
    }

    void Update() {
        if(Time.fixedTime > 1)
            SceneManager.LoadScene(1);
    }

}
