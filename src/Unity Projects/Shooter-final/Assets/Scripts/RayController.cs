using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class RayController : MonoBehaviour
{
    [Header("Time for the ray to live")]
    public float timeToLive = 3;

    public PlayerController PlayerController { get; set; }

    // Start is called before the first frame update
    private void Start()
    {
        Destroy(gameObject, timeToLive);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            var enemyController = other.GetComponent<EnemyController>();
            enemyController.Die();
            Destroy(gameObject);
            PlayerController.score += 1;
        }
    }
}
