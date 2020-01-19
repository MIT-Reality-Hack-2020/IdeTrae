using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneLoader : MonoBehaviour

    
{
    private Vector3 orig_pos;
    public bool ideation_room;
    public GameObject confetti;
    public GameObject grabSoundObj;
    public GameObject telePortSoundObj; 

    AudioSource grabSound;
    AudioSource telePortSound;

    private void Start()
    {
        ideation_room = false;
        confetti.SetActive(false);
        telePortSound = telePortSoundObj.GetComponent<AudioSource>();
        grabSound = grabSoundObj.GetComponent<AudioSource>();
    }

    void OnTriggerEnter(Collider collider)
    {

        if (collider.gameObject.tag == "Portal")
        {
            //string name = collider.gameObject.name;
            //char roomNumber =  name[name.Length - 1];
            //SceneManager.LoadScene("Room" + roomNumber);
            telePortSound.Play(0);
            ideation_room = true;
            Debug.Log("ideation bool:");
            Debug.Log(ideation_room);
            SceneManager.LoadScene("Scene 2 - Ideation Room");
            //Debug.Log("Teleporting to Room" + roomNumber);
        }

        else if (collider.gameObject.name == "Return")
        {
            //if (ideation_room)
            //{
                telePortSound.Play(0);

                Debug.Log("return collider");
                SceneManager.LoadScene("Scene 1 - Main Hall");
                Debug.Log("Teleporting to Normcore");
                ideation_room = false;
            //}
            
        }
        
        else if (collider.gameObject.tag == "Button")
        {
            grabSound.Play(0);


            Vector3 pos = collider.gameObject.transform.position;
            orig_pos = pos;
            if (pos.y >= -5.51)
            {
                pos.y -= 0.005f;
            }
            collider.gameObject.transform.position = pos;
            Debug.Log("asdkoadskijoasijdoi");
            confetti.SetActive(true);
        }
        
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.tag == "Button")
        {
            grabSound.Play(0);

            collider.gameObject.transform.position = orig_pos;
            Debug.Log("asdkoadskijoasijdoi");
        }
    }

    /*  if (collider.gameObject.name == "Teleporter A")
    {
        SceneManager.LoadScene("Room A");
        Debug.Log("Teleporting to Room A");

    }

    else if (collider.gameObject.name == "Return")
    {
        SceneManager.LoadScene("Normcore");
        Debug.Log("Teleporting to Normcore");
    }
}*/


}
