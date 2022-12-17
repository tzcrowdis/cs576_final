using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class robotController : MonoBehaviour
{
    private Vector3 job1;
    private Vector3 job2;
    private Vector3 curr_dest;
    private float curr_time;
    private float work_time;

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
        //get position of the two jobs
        job1 = GameObject.Find("Job 1").transform.position;
        job2 = GameObject.Find("Job 2").transform.position;

        //how long is a job
        work_time = 5.0f;
        curr_time = 0;

        //use randomness to pick first job position
        if (Random.Range(0f, 1f) <= 0.5)
            curr_dest = job1;
        else
            curr_dest = job2;

        //dont start in convo
        convo = false;
        prompt.enabled = false;
    }

    void Update()
    {
        if (convo == false)
        {
            //traverse to current job
            robot.SetDestination(curr_dest);

            //control animations and assign new job
            if (robot.velocity.magnitude > 0)
                robot_anim.SetBool("walking", true);
            else
            {
                robot_anim.SetBool("walking", false);
                if (curr_time <= 0)
                    curr_time = work_time;
            }

            //working timer
            if (curr_time > 0)
                curr_time -= Time.deltaTime;

            //job timer up assign new job
            if (curr_time <= 0 && robot.velocity.magnitude == 0)
            {
                if (Random.Range(0f, 1f) <= 0.5)
                    curr_dest = job1;
                else
                    curr_dest = job2;
            }
        }
        else //in conversation
        {
            //lock movement and anim
            robot_anim.SetBool("walking", false);
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

        //allow player to start convo with F
        if (Input.GetKeyDown(KeyCode.F))
        {
            //lock movement
            convo = true;
            other.GetComponent<PlayerController>().convo = true;
            other.GetComponent<CameraController>().convo = true;

            //begin dialogue
            FindObjectOfType<DialogueManager>().startDialogue(dialogue);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //remove prompt
        prompt.enabled = false;
    }
}
