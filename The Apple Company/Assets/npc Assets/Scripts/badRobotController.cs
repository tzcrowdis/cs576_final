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
    public Canvas prompt;
    public Dialogue dialogue;

    public Transform target;
    public float turnSpeed = 10.0f;

    void Start()
    {
        //dont start in convo
        convo = false;
        prompt.enabled = false;
    }

    void Update()
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

    private void OnTriggerStay(Collider other)
    {
        //display prompt to start conversation
        prompt.enabled = true;

        //lock movement
        convo = true;
        other.GetComponent<PlayerController>().convo = true;
        other.GetComponent<CameraController>().convo = true;

        //begin dialogue
        FindObjectOfType<DialogueManager>().startDialogue(dialogue);
    }

    private void OnTriggerExit(Collider other)
    {
        //remove prompt
        prompt.enabled = false;
    }
}
