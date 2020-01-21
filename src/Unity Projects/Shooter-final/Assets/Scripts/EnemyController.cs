using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Header("Movement variables")]
    public float movementSpeed = 3f;

    [Header("Player to follow")] 
    public GameObject player;
    private PlayerController _playerController;

    [Header("Enemy Generator")]
    public EnemyGenerator enemyGenerator;
    
    // Start is called before the first frame update
    private void Start()
    {
        _playerController = player.GetComponent<PlayerController>();
        enemyGenerator.EnemyIsAlive();
    }

    // Update is called once per frame
    private void Update()
    {
        if (!_playerController.Dead)
        {
            FollowPlayer();
        }
    }

    private void FollowPlayer()
    {
        // Direction vector calculated
        var direction = player.transform.position - transform.position;
        // Normalize the direction vector for constant movement
        transform.Translate(Time.deltaTime * movementSpeed * direction.normalized, Space.World);
        if (direction != Vector3.zero)
        {
            // We face the player
            var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }

    public void Die()
    {
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        enemyGenerator.EnemyIsDead();
    }
}
