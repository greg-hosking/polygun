using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    GameObject player;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    void Update()
    {
        // Look at player...
        transform.up = ((Vector2)player.transform.position - (Vector2)transform.position).normalized;

        // Move towards player...
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, Time.deltaTime);
    }
}
