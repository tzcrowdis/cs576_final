using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStick : MonoBehaviour
{
    //source: https://www.youtube.com/watch?v=rO19dA2jksk

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
