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

        // apple destroyed if it exists beyond 5 seconds
        if(Time.time - start_time > 10.0f)
        {
            Destroy(transform.gameObject);
        }

        transform.position = transform.position + velocity * direction * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider hitter)
    {
        if (hitter.gameObject.tag == "glass" || hitter.gameObject.tag == "character")
            Destroy(transform.gameObject);
    }
}