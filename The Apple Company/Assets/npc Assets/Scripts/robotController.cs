using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class robotController : MonoBehaviour
{
    private Vector3 job1;
    private Vector3 job2;
    private Vector3 curr_dest;
    private float curr_time;
    private float work_time;

    public NavMeshAgent robot;
    public Animator robot_anim;

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
    }

    void Update()
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
}
