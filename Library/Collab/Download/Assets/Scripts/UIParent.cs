using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

using VRKeys;
using TMPro;
using Normal.Realtime;

public class UIParent : MonoBehaviour
{
    public enum Mode {
        neutral,
        ideaCreation,
        ideaCreated,
        ideaTitle,
        ideaDone
    };

    public GameObject leftHandAnchor;
    public GameObject rightHandAnchor;
    public GameObject player;

    public string title;

    public Mode mode = Mode.neutral;

    public GameObject keyboard;
    //public GameObject keyboardScene;

    Keyboard k;

    GameObject newHeadset;
    float timer;

    //for idea scaling
    static float interval = 0.005f;
    Vector3 intervalVector = new Vector3(interval, interval, interval);
    float headsetSize = 5;
    //for particles
    Vector3 offset = new Vector3(0.0f, 0.0f, 0.0f);

    //all headsets
    List<GameObject> headsets = new List<GameObject>();

    GameObject sparklesObj;

    // Start is called before the first frame update
    void Start()
    {
        timer = 0;
        title = "NEW IDEA!";

        leftHandAnchor = GameObject.Find("LeftHandAnchor");
        rightHandAnchor = GameObject.Find("RightHandAnchor");

    }

    // Update is called once per frame
    void Update()
    {
        if (newHeadset != null) newHeadset.GetComponentInChildren<TextMeshProUGUI>().text = title;
        if (mode == Mode.neutral)
        {
            //DisableKeyboard();
            if (BumperPressed())
            {
                Debug.Log("bump");
                StartIdeaCreation();
            }
        }
        else if (mode == Mode.ideaCreation)
        {
            if (BumperPressed())
            {
                //Debug.Log("updated");
                if (timer < headsetSize)
                {
                    newHeadset.transform.localScale += intervalVector;
                    newHeadset.transform.position = rightHandAnchor.transform.position + new Vector3(0.0f, 0.3f, 0.0f);
                    //newHeadset.transform.rotation = //Quaternion.Euler(rightHandAnchor.transform.rotation.eulerAngles + new Vector3(0.0f, 180f, 0.0f));
                    timer++;
                }
                else
                {
                    Particles("CFX4 Sparks Explosion B", 0.5f, rightHandAnchor.transform.position + new Vector3(0.0f, 0.3f, 0.0f));
                    Debug.Log("done scaling headset");
                    //copy headset and save to list
                    //GameObject headset = Instantiate(newHeadset);
                    headsets.Add(newHeadset);
                    //Destroy(newHeadset);
                    timer = 0;
                    mode = Mode.ideaCreated;
                }
            }
            else
            {
                mode = Mode.neutral;
                Realtime.Destroy(newHeadset);
            }
        }
        else if (mode == Mode.ideaCreated)
        {
            if (timer < 2.0f)
            {
                timer += Time.deltaTime;
            }
            else
            {
                mode = Mode.ideaTitle;
            }
        }
        else if (mode == Mode.ideaTitle)
        {
           // Debug.Log("set title");
            newHeadset.SetActive(false);
            //Debug.Log(GameObject.Find("VRKeys"));
            //GameObject demoScene = GameObject.Find("Demo Scene");
            //demoScene.GetComponent<DemoScene>().enabled = true;
            keyboard.SetActive(true);
            //keyboardScene.SetActive(true);
            GameObject vrk = GameObject.Find("VRKeys");
            if (k == null || k.disabled)
            {
                k = vrk.GetComponent<Keyboard>();
                k.leftHandAnchor = GameObject.Find("LeftHandAnchor");
                k.rightHandAnchor = GameObject.Find("RightHandAnchor");
                k.player = GameObject.Find("OVRPlayerController");
                k.Enable();
                k.SetPlaceholderMessage("Enter a snappy title!");
                k.OnUpdate.AddListener(HandleUpdate);
                k.OnSubmit.AddListener(HandleSubmit);
                k.OnCancel.AddListener(HandleCancel);
            }
            vrk.transform.localPosition = GameObject.Find("Main Camera").transform.position + new Vector3(0.0f, -0.2f, 1.1f);
        }
        else if (mode == Mode.ideaDone)
        {
            Debug.Log("idea done");
            DisableKeyboard();
            keyboard.SetActive(false);
            newHeadset.SetActive(true);
            newHeadset.transform.position = new Vector3(1.019f, 1.507f, 2.262f);
            newHeadset.transform.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
            //newHeadset.GetComponent<Renderer>().material = (Material)Resources.Load("HeadsetDone");
            mode = Mode.neutral;
        }
    }

    void DisableKeyboard()
    {
        //GameObject demoScene = GameObject.Find("Demo Scene");
        //demoScene.GetComponent<DemoScene>().enabled = false;
        if (k != null)
        {
            k.OnUpdate.RemoveListener(HandleUpdate);
            k.OnSubmit.RemoveListener(HandleSubmit);
            k.OnCancel.RemoveListener(HandleCancel);
            
            k.Disable();
        }
    }

    /// <summary>
    /// Hide the validation message on update. Connect this to OnUpdate.
    /// </summary>
    public void HandleUpdate(string text)
    {
        //TODO what is this
        k.HideValidationMessage();
    }

    /// <summary>
    /// Validate the email and simulate a form submission. Connect this to OnSubmit.
    /// </summary>
    public void HandleSubmit(string text)
    {
        title = text;
        mode = UIParent.Mode.ideaDone;
    }

    public void HandleCancel()
    {
        Debug.Log("Cancelled keyboard input!");
        mode = UIParent.Mode.neutral;
    }

    void StartIdeaCreation()
    {
        //TODO replace this line
        newHeadset = Instantiate((GameObject)Resources.Load("HeadsetNew"));
        //newHeadset.GetComponent<Renderer>().material = (Material)Resources.Load("NewHeadset");
        //newHeadset.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        mode = Mode.ideaCreation;
        //Particles("CFX4 Aura Bubble C", 0.5f, newHeadset.transform);
        timer = 0;
    }

    void Particles(string name, float size, Vector3 pos)
    {
        sparklesObj = Instantiate((GameObject)Resources.Load(name));
        sparklesObj.tag = "particles";
        ParticleSystem sparkles = sparklesObj.GetComponent<ParticleSystem>();
        sparklesObj.transform.localScale = new Vector3(size, size, size);
        sparklesObj.transform.localPosition = pos;
        //sparklesObj.transform.parent = trfm;
        sparkles.Emit(100);
    }

    public bool BumperPressed()
    {
        if (Input.GetAxisRaw("Oculus_CrossPlatform_PrimaryIndexTrigger") > 0.1f)
        {
            return true;
        }
        if (Input.GetAxisRaw("Oculus_CrossPlatform_SecondaryIndexTrigger") > 0.1f)
        {
            return true;
        }
        return false;
    }
}
