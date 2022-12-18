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
<<<<<<< HEAD:The Apple Company/Assets/Resources/Turret Assets/apple.cs
        Destroy(transform.gameObject);
=======
        // if it hits the player
        if(hitter.gameObject.tag == "character")
        {
            //GameObject.Find("player").GetComponent<PlayerController>().num_lives -= 1;
            Destroy(transform.gameObject);
        }
        else
        {
            Destroy(transform.gameObject);
        }
>>>>>>> a39b9c8396c694715976d5a576865277ef390d6b:The Apple Company/Assets/Turret Assets/apple.cs
    }
}
