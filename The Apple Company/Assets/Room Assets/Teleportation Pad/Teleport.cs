using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Teleport : MonoBehaviour
{
    public Transform destination;
    public Transform subject;
    private float timer;
    private bool go;

    public Canvas countdown;
    public TextMeshProUGUI three;
    public TextMeshProUGUI two;
    public TextMeshProUGUI one;

    public bool end;
    public int curr_level;

    // Start is called before the first frame update
    void Start()
    {
        timer = 3.0f;
        countdown.enabled = false;
        go = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (go && end) //for starting the next level
        {
            if (curr_level == 1)
                SceneManager.LoadScene("Level2", LoadSceneMode.Single);
            else if (curr_level == 2)
                SceneManager.LoadScene("Level3", LoadSceneMode.Single);
            else if (curr_level == 3)
                SceneManager.LoadScene("Main Menu", LoadSceneMode.Single);
        }
        else if (go) //for teleporting within level
        {
            subject.GetComponent<CharacterController>().enabled = false;
            subject.GetComponent<PlayerController>().enabled = false;
            subject.position = destination.position + new Vector3(0, 0.01f, 0);
            subject.GetComponent<CharacterController>().enabled = true;
            subject.GetComponent<PlayerController>().enabled = true;
            go = false;
            timer = 3.0f;
        }
    }

    void OnTriggerEnter(Collider player)
    {
        countdown.enabled = true;
        two.enabled = false;
        one.enabled = false;
    }

    void OnTriggerStay(Collider player)
    {
        timer -= Time.deltaTime;
        if (timer > 2)
        {
            three.enabled = true;
            two.enabled = false;
            one.enabled = false;
        }
        else if (timer <= 2 && timer > 1)
        {
            three.enabled = false;
            two.enabled = true;
            one.enabled = false;
        }
        else if (timer <= 1 && timer > 0)
        {
            three.enabled = false;
            two.enabled = false;
            one.enabled = true;
        }
        else if (timer <= 0)
        {
            go = true;
            countdown.enabled = false;
            three.enabled = false;
            two.enabled = false;
            one.enabled = false;
        }
    }

    void OnTriggerExit(Collider player)
    {
        timer = 3.0f;
        countdown.enabled = false;
        three.enabled = false;
        two.enabled = false;
        one.enabled = false;
        go = false;
    }
}
