using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FishCompanion : MonoBehaviour
{
    public NavMeshAgent fish;
    public Animator fish_anim;

    public Transform player;

    public AudioSource footstep;

    void Start()
    {
        footstep.enabled = false;
    }

    void Update()
    {
        fish.SetDestination(player.position);

        if (fish.velocity.magnitude > 0)
        {
            fish_anim.SetBool("follow", true);
            footstep.enabled = true;
        }
        else
        {
            fish_anim.SetBool("follow", false);
            footstep.enabled = false;
        }
    }
}
