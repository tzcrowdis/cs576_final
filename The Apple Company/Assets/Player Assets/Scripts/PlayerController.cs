using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

    //public GameObject floor;
    public Transform orientation;
    public Vector3 movement_direction;
    public float velocity;
    public int acceleration;

    private float turn;
    public float gravity;
    public float vert_velocity;
    private float MAX_VEL = 1.5f;
    private Animator animation_controller;
    public CharacterController character_controller;
    private float horizontal;
    private float vertical;
    //private Bounds floor_bound;
    private float objectWidth;

    //variable to control conversation dynamics
    public bool convo;
    public Canvas tab_menu;

    //jumping
    private float jump_h;

    // Start is called before the first frame update
    void Start()
    {
        convo = false;
        jump_h = 1.0f;
        tab_menu.enabled = false;

        animation_controller = GetComponent<Animator>();
        //character_controller = GetComponent<CharacterController>();
        movement_direction = new Vector3(0.0f, 0.0f, 0.0f);
        velocity = 0.0f;
        acceleration = 2;
        turn = 0.5f;
        vert_velocity = 0.0f;
        gravity = -6.0f;
        //floor_bound = floor.GetComponent<Collider>().bounds;
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
        if (convo == false) //locks movement in conversation ANIMATION STILL PLAYS LOOK INTO STOPPING IT
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

            character_controller.Move(movement_direction * velocity * Time.deltaTime);

            //jump
            //source: https://gamedevbeginner.com/how-to-jump-in-unity-with-or-without-physics/#character_controller_jump
            if (character_controller.isGrounded && vert_velocity < 0)
            {
                vert_velocity = gravity * Time.deltaTime;
            }

            character_controller.minMoveDistance = 0f;

            if (character_controller.isGrounded && Input.GetKeyDown(KeyCode.Space))
            {
                vert_velocity = Mathf.Sqrt(jump_h * -2f * gravity);
            }
            vert_velocity += gravity * Time.deltaTime;
            character_controller.Move(new Vector3(0, vert_velocity, 0) * Time.deltaTime);

            //handle objective menu
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                if (tab_menu.enabled == false)
                    tab_menu.enabled = true;
                else
                    tab_menu.enabled = false;
            }
        }
        else
        {
            //move dialogue forward
            if (Input.GetKeyDown(KeyCode.Return))
            {
                FindObjectOfType<DialogueManager>().DisplayNextSentence();
            }
        }
    }
}
