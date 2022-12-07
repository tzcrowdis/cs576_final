using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class robotController : MonoBehaviour
{
    private Vector3 job1;
    private Vector3 job2;
    private Vector3 curr_dest;

    public NavMeshAgent robot;

    void Start()
    {
        //get position of the two jobs
        job1 = GameObject.Find("Job 1").transform.position;
        job2 = GameObject.Find("Job 2").transform.position;

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

        //remember to set walking to true for animation
    }
}
