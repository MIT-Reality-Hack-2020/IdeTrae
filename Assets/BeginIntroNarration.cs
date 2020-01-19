using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeginIntroNarration : MonoBehaviour
{
	bool canFire;
	float timer;
	private AudioSource sound;
    // Start is called before the first frame update
    void Start()
    {
        canFire = true;
        //timer = time.time;
        sound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (canFire) Invoke("Sound", 2);
    }
    void Sound(){
    	sound.Play(0);
    	canFire = false;
    }
}