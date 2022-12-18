using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class turret : MonoBehaviour
{
    private GameObject projectile;
    private float shoot_delay;
    private float projectile_vel;
    private Vector3 dir_tur_to_player;
    private Vector3 proj_str_pos;
    private Vector3 shoot_dir;
    private bool player_acc;
    // Start is called before the first frame update
    void Start()
    {
        // getting the apples to be thrown from the turrets
        projectile = (GameObject)Resources.Load("Turret Assets/Apple/Prefab/Apple", typeof(GameObject));

        if (projectile == null)
        {
            Debug.LogError("No apple prefab found.");
            shoot_delay = 0.5f;
            projectile_vel = 5.0f;
            dir_tur_to_player = new Vector3(0.0f, 0.0f, 0.0f);
            proj_str_pos = new Vector3(0.0f, 0.0f, 0.0f);
            player_acc = false;
            StartCoroutine("Attack");
        }
    }

    // Update is called once per frame
    void Update()
    {
        GameObject player = GameObject.find("player");  // need player object in scene

        if(player == null)
        {
            Debug.LogError("player not found.");
        }

        //obtaining the distance by using the centroids of the player and turret

        Vector3 player_centroid = player.GetComponent<CapsuleCollider>().bounds.center;
        Vector3 turret_centroid = GetComponent<Collider>().bounds.center;
        dir_tur_to_player = player_centroid - turret_centroid;
        dir_tur_to_player.Normalize();

        RayCast hit;
        

        // making the turret rotate in the direction of the player
        if(Physics.RayCast(turret_centroid, dir_tur_to_player, out hit, Mathf.Infinity))
        {
            if (hit.collider.gameObject == player)
            {
                Vector3 future_target_pos = player_centroid;
                float delta_pos = Mathf.Infinity;


                // start launching apples once the player is within the radius
                while (delta_pos > 20)
                {
                    float dist = Vector3.Distance(future_target_pos, proj_str_pos) - 1.1f;
                    float ahead_time = distance / projectile_vel;
                    Vector3 last_pos = future_target_pos;
                    future_target_pos = player_centroid + ahead_time * player.GetComponent<PlayerController>().velocity * player.GetComponent<PlayerController>().movement_direction;  
                    delta_pos = Vector3.Distance(future_target_pos, last_pos);
                }

                shoot_dir = future_target_pos - proj_str_pos;
                shoot_dir.Normalize();

                float rotate_angle = Mathf.Rad2Deg * Mathf.Atan2(shoot_dir.x, shoot_dir.z);
                transform.eulerAngles = new Victor3(0.0f, rotate_angle, 0.0f);
                Vector3 curr_tur_dir = new Vector3(Mathf.Sin(Mathf.Deg2Rad * transform.eulerAngles.y), 1.1f, Mathf.Cos(Mathf.Deg2Rad * transform.eulerAngles.y));
                proj_str_pos = transform.position + 1.1f * curr_tur_dir;
                player_acc = true;
            }
            else
                player_acc = false;
        }
    }

    //The function to begin the launch of apples if all conditions are met

    private IEnumerator Attack()
    {
        while(true)
        {
            if(player_acc)
            {
                GameObject new_obj = Instantiate(projectile, proj_str_pos, Quaternion.identity);
                new_obj.GetComponent<apple>().direction = shoot_dir;
                new_obj.GetComponent<apple>().velocity = projectile_vel;
                new_obj.GetComponent<apple>().start_time = Time.time;
                new_obj.GetComponent<apple>().from_tur = transform.gameObject;
            }

            yield return new WaitForSeconds(shoot_delay);
        }
    }
}
