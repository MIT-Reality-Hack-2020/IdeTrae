using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class SparkEffect : MonoBehaviour
{
    GameObject sparksObj;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!sparksObj)
        {
            sparksObj = (GameObject)Resources.Load("CFX4 Sparks Explosion B");
            Debug.Log("sparks " + sparksObj);
            ParticleSystem sparks = sparksObj.GetComponent<ParticleSystem>();
        }
        
        //TODO should work for both hands
        sparksObj.transform.localPosition = InputTracking.GetLocalPosition(XRNode.LeftHand);
        
    }
    /*
    public static void SparkExplosion()
    {
        GameObject sparksObj = (GameObject)Resources.Load("CFX4 Sparks Explosion B");
        Debug.Log("sparks " + sparksObj);
        ParticleSystem sparks = sparksObj.GetComponent<ParticleSystem>();
        //TODO should work for both hands
        sparksObj.transform.localPosition = InputTracking.GetLocalPosition(XRNode.LeftHand);
        sparks.Emit(100);
    }
    */
}
