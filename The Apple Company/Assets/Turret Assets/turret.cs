// referenced assignment 3

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class turret : MonoBehaviour
{
    private float shoot_delay;
    public GameObject projectile;
    private Vector3 dir_tur_player;
    private Vector3 shoot_dir;
    private Vector3 proj_str_pos;
    private float proj_vel;
    private bool player_acc;

    void Start()
    {
        // if apple not present
        if (projectile == null)
            Debug.LogError("Apple prefab not found");

        shoot_delay = 0.5f;
        proj_vel = 1.0f;
        dir_tur_player = new Vector3(0.0f, 0.0f, 0.0f);
        proj_str_pos = new Vector3(0.0f, 0.0f, 0.0f);

        player_acc = false;
        StartCoroutine("Attack");
    }

    // Update is called once per frame
    void Update()
    {

        //finds the player in scene
        GameObject player = GameObject.Find("PlayerModel");

        if (player == null)
            Debug.LogError("Player not found.");

        //getting the centroids of player and turret
        Vector3 player_center = player.GetComponent<Collider>().bounds.center;
        Vector3 tur_center = GetComponent<Collider>().bounds.center;
        Vector3 aim = new Vector3(player_center.x, player_center.y + 0.7f, player_center.z);
        dir_tur_player = aim - tur_center;
        dir_tur_player.Normalize();


        // if the player is within the range of the raycast
        RaycastHit hit;
        if (Physics.Raycast(tur_center, dir_tur_player, out hit, Mathf.Infinity))
        {
            if (hit.collider.gameObject == player)
            {
                shoot_dir = dir_tur_player;

                float rotate_angle = Mathf.Rad2Deg * Mathf.Atan2(shoot_dir.x, shoot_dir.z);
                transform.eulerAngles = new Vector3(0.0f, rotate_angle, 0.0f);
                Vector3 curr_dir = new Vector3(Mathf.Sin(Mathf.Deg2Rad * transform.eulerAngles.y), 1.1f, Mathf.Cos(Mathf.Deg2Rad * transform.eulerAngles.y));
                proj_str_pos = transform.position + 0.5f * curr_dir;
                player_acc = true;
            }
            else
                player_acc = false;
        }
    }

    // function to launch the apple

    private IEnumerator Attack()
    {
        while(true)
        {
            if (player_acc)
            {
                GameObject new_obj = Instantiate(projectile, proj_str_pos, Quaternion.identity);
                new_obj.GetComponent<apple>().direction = shoot_dir;
                new_obj.GetComponent<apple>().velocity =  proj_vel;
                new_obj.GetComponent<apple>().start_time = Time.time;
                new_obj.GetComponent<apple>().from_tur = transform.gameObject;
            }

            yield return new WaitForSeconds(shoot_delay);
        }
    }
}
