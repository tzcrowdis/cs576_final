using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CookieScript : MonoBehaviour
{
    public Light spotlight;
    public Canvas prompt;

    public AudioSource take;

    void Start()
    {
        //start light disabled
        spotlight.enabled = false;
        prompt.enabled = false;

        take.enabled = false;
    }

    void OnTriggerEnter(Collider player)
    {
        //enable light
        spotlight.enabled = true;
        //enable button prompt
        prompt.enabled = true;
    }

    void OnTriggerStay(Collider player)
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            //destroy the cookie
            take.enabled = true;
            Destroy(gameObject);

            //add to final score
        }
    }

    void OnTriggerExit(Collider player)
    {
        prompt.enabled = false;
        spotlight.enabled = false;
    }
}
