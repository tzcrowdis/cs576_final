using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class badRobotController : MonoBehaviour
{
    public NavMeshAgent robot;
    public Animator robot_anim;

    //dialogue vars
    public bool convo;
    public Dialogue dialogue;

    public Transform target;
    public float turnSpeed = 10.0f;

    //timer of 15s after convo
    private float fatigue_time = 15f;
    private float timer;

    void Start()
    {
        //dont start in convo
        convo = false;

        timer = 0f;
    }

    void Update()
    {
        if (timer <= 0f)
        {
            if (convo == false)
            {
                //traverse to current job
                robot.SetDestination(target.position);

                //control animations and assign new job
                if (robot.velocity.magnitude > 0)
                    robot_anim.SetBool("chase", true);
                else
                {
                    robot_anim.SetBool("chase", false);
                }

            }
            else //in conversation
            {
                //lock movement and anim
                robot_anim.SetBool("chase", false);
                robot.SetDestination(transform.position);
                //rotate to player (from unity api)
                Vector3 targetDirection = target.position - transform.position;
                float singleStep = turnSpeed * Time.deltaTime;
                Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, singleStep, 0.0f);
                transform.rotation = Quaternion.LookRotation(newDirection);
            }
        }
        else
        {
            timer -= Time.deltaTime;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (timer == 0f)
        {
            //lock movement
            convo = true;
            other.GetComponent<PlayerController>().convo = true;
            //rotate towards bad bot
            other.GetComponent<CameraController>().target = transform;
            other.GetComponent<CameraController>().convo = true;

            //begin dialogue
            FindObjectOfType<DialogueManager>().startDialogue(dialogue);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        //end convo
        if (Input.GetKeyDown(KeyCode.Return))
        {
            timer = fatigue_time;
            convo = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        
    }
}
