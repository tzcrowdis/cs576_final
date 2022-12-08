using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public GameObject floor;
    public Transform orientation;
    public Vector3 movement_direction;
    public float velocity;
    public int acceleration;

    private float turn;
    public float gravity;
    public float vert_velocity;
    private float MAX_VEL = 1.5f;
    private Animator animation_controller;
    private CharacterController character_controller;
    private float horizontal;
    private float vertical;
    private Bounds floor_bound;
    private float objectWidth;

    // Start is called before the first frame update
    void Start()
    {
        animation_controller = GetComponent<Animator>();
        character_controller = GetComponent<CharacterController>();
        movement_direction = new Vector3(0.0f, 0.0f, 0.0f);
        velocity = 0.0f;
        acceleration = 2;
        turn = 0.5f;
        vert_velocity = 0.0f;
        gravity = 2.0f;
        floor_bound = floor.GetComponent<Collider>().bounds;
        objectWidth = 0;// transform.GetComponent<MeshRenderer>().bounds.size.x / 2;
    }

    private void GetInput()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();

        movement_direction = orientation.forward * vertical + orientation.right * horizontal; 
        
        animation_controller.SetInteger("state", 1);
        //Forwards
        if (Input.GetKey(KeyCode.W))
        {
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

        character_controller.Move(movement_direction * velocity * Time.deltaTime);\
    }
}