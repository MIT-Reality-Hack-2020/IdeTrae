using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Record : MonoBehaviour
{
	public int record;
	public int stop;
	public bool play;
	bool recorded;
	AudioSource audioSource;
	public GameObject stopMesh;
	public GameObject recordMesh;

    private OVRGrabbable ovrGrabbable;

    private bool isRecording = false;
    private bool playOnce = true;

    // Update is called once per frame
	void Start() {
		recorded = false;
		audioSource = GetComponent<AudioSource>();
        ovrGrabbable = GetComponent<OVRGrabbable>();
		stopMesh.SetActive(false);
		recordMesh.SetActive(true);
        stop = 0;
        record = 0;
    }

    void Update()
    {
        
        if (ovrGrabbable.isGrabbed == true)
        {
            StartRecording();
        }
        if (ovrGrabbable.isGrabbed == false) {

            StopRecord();
            PlayAudio();
        }
        
        /*
        if (record == 1){
        	//audioSource = GetComponent<AudioSource>();
        	audioSource.clip = Microphone.Start(null, true, 180, 44100);
        	recorded = true;
        	Debug.Log("recording");
        	record = 2;
        	recordMesh.SetActive(true);
        	stopMesh.SetActive(false);
        }
        if (record != 1 && recorded == true && stop == 1) {
        	Microphone.End(null); //"Built-in Microphone"
        	audioSource.Play();
        	Debug.Log("Stopped");
        	recorded = false;
        	stop = 2;
        	recordMesh.SetActive(false);
        	stopMesh.SetActive(true);
        }

        if (play) {
        	audioSource.Play();
        	play = false;
        	Debug.Log("Playing");
        }
        */
    }

    public void StartRecording()
    {
        if(!isRecording)
        {
            //audioSource = GetComponent<AudioSource>();
            audioSource.clip = Microphone.Start(null, true, 180, 44100);
            recorded = true;
            Debug.Log("recording");
            record = 2;
            recordMesh.SetActive(true);
            stopMesh.SetActive(false);
            isRecording = true;
        }

    }

    public void StopRecord()
    {
        if(isRecording)
        {
            Microphone.End(null); //"Built-in Microphone"
            audioSource.Play();
            Debug.Log("Stopped");
            recorded = false;
            stop = 2;
            recordMesh.SetActive(false);
            stopMesh.SetActive(true);
            isRecording = false;
        }

    }

    public void PlayAudio()
    {
        if(playOnce)
        {
            audioSource.Play();
            playOnce = false;
            Debug.Log("Playing");
        }

    }
}
