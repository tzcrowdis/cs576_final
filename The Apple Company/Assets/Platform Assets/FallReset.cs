using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallReset : MonoBehaviour
{
    public Transform respawn_pos;
    public Transform subject;
    
    void OnTriggerEnter(Collider player)
    {
        subject.GetComponent<CharacterController>().enabled = false;
        subject.GetComponent<PlayerController>().enabled = false;
        subject.position = respawn_pos.position + new Vector3(0, 0.1f, 0);
        subject.GetComponent<CharacterController>().enabled = true;
        subject.GetComponent<PlayerController>().enabled = true;
    }
}
