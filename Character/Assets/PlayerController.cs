using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Vector3 target;
    float speed = 1.0f;

    private Animator animation_controller;
    private CharacterController character_controller;
    public GameObject button;
    public Vector3 movement_direction;
    public float velocity;
    public int acceleration;
    private float turn;
    public float vert_velocity;
    public float gravity;
    private float MAX_VEL = 1.5f;

    // Start is called before the first frame update
    void Start()
    {
        animation_controller = GetComponent<Animator>();
        character_controller = GetComponent<CharacterController>();
        movement_direction = new Vector3(0.0f, 0.0f, 0.0f);
        velocity = 0.0f;
        acceleration = 2;
        turn = 0.2f;
        vert_velocity = 0.0f;
        gravity = 2.0f;
    }

    // Update is called once per frame
    void Update()
    {
        //Turn
        if (Input.GetKey(KeyCode.D))
        {
            animation_controller.SetInteger("state", 3);
            transform.Rotate(0.0f, -turn, 0.0f);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            animation_controller.SetInteger("state", 2);
            transform.Rotate(0.0f, turn, 0.0f);
        }
        //Forwards
        else if (Input.GetKey(KeyCode.W))
        {
            animation_controller.SetInteger("state", 1);
            velocity = Mathf.Clamp(velocity + acceleration * Time.deltaTime, 0, MAX_VEL * 2);
        }
        //Backwards
        else if (Input.GetKey(KeyCode.S))
        {
            animation_controller.SetInteger("state", 4);
            velocity = Mathf.Clamp(velocity - acceleration * Time.deltaTime, -MAX_VEL / 1.5f, 0);
        }
        //Idle
        else
        {
            animation_controller.SetInteger("state", 0);
            velocity = Mathf.Clamp(velocity - acceleration * Time.deltaTime, 0, MAX_VEL);
        }

        float xdirection = Mathf.Sin(Mathf.Deg2Rad * transform.rotation.eulerAngles.y);
        float zdirection = Mathf.Cos(Mathf.Deg2Rad * transform.rotation.eulerAngles.y);
        movement_direction = new Vector3(xdirection, 0.0f, zdirection);

        if (transform.position.y > 0.0f) // if the character starts "climbing" the terrain, drop her down
        {
            Vector3 lower_character = movement_direction * velocity * Time.deltaTime;
            lower_character.y = -100f; // hack to force her down
            character_controller.Move(lower_character);
        }
        else
        {
            character_controller.Move(movement_direction * velocity * Time.deltaTime);
        }
    }

    void SetNewTarget(Vector3 newTarget)
    {
        target = newTarget;
        transform.LookAt(target);
    }
}
