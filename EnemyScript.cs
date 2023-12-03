using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public float speed = 5.0f;
    private Transform player;
    private GameObject playerObj;

    void Start()
    {
        playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
        }
    }

    void Update()
    {
        if (player)
        {
            // Calculate the direction from the enemy to the player.
            Vector2 direction = (player.position - transform.position).normalized;

            // Move the enemy towards the player.
            transform.position += (Vector3)direction * (speed * Time.deltaTime);
            
            // Calculate the angle between the positive X-axis and the direction to the Player.
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        
            // Make the enemy rotate to face the player
            transform.rotation = Quaternion.Euler(0, 0, angle);   
        }
    }
}
