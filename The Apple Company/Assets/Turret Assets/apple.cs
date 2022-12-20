// referenced Unity forum for collision queries
// referenced assignment 3 of the course

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class apple : MonoBehaviour
{
    public Vector3 direction;
    public float velocity;
    public float start_time;
    public GameObject from_tur;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        // apple destroyed if it exists beyond 10 seconds
        if(Time.time - start_time > 10.0f)
        {
            Destroy(transform.gameObject);
        }

        // apple's movement
        transform.position = transform.position + velocity * direction * Time.deltaTime;
    }

    // destroying the apple if it hits any object in the scene
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "hit")
        {
            Destroy(transform.gameObject);
        }
    }
}
