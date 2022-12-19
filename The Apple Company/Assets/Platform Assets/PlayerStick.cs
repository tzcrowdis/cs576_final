using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStick : MonoBehaviour
{
    public GameObject Player;
    
    void OnTriggerEnter(Collider other)
    {
        Player.transform.parent = transform;
    }

    void OnTriggerExit(Collider other)
    {
        Player.transform.parent = null;
    }
}
