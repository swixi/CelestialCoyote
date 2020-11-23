using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CylinderCollision : MonoBehaviour
{

    //public AudioSource soundSource;
    AudioSource audio;
    public AudioClip hitSound1;
    public AudioClip hitSound2;
    public AudioClip hitSound3;
    public AudioClip hitSound4;


    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    void OnCollisionEnter(Collision collisionInfo)
    {
        if (collisionInfo.collider.tag == "player")
        {
            //inclusive
            int choice = (int)Random.Range(1, 5);

            if(choice == 1)
                this.GetComponent<AudioSource>().clip = hitSound1;
            if (choice == 1)
                this.GetComponent<AudioSource>().clip = hitSound2;
            if (choice == 1)
                this.GetComponent<AudioSource>().clip = hitSound3;
            else
                this.GetComponent<AudioSource>().clip = hitSound4;

            this.GetComponent<AudioSource>().Play();
            //AudioSource.PlayClipAtPoint(hitSound, transform.localPosition);
        }
    }
}
