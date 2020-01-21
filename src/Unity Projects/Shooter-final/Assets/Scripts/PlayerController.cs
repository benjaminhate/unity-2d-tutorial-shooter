using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Movement variables
    [Header("Movement variables")]
    public float movementSpeed = 10f;
    public float rotationSpeed = 180f;
    
    // Death variables
    [Header("Death variables")]
    public float timeDead = 2f;
    public int Health { get; private set; }
    public bool Dead { get; private set; }
    private float _timeOfDeath;
    
    // Reset variables
    [Header("Reset variables")]
    public float baseX = 2f;
    public float baseY = -3f;
    public float baseRot = 0;
    public int baseHealth = 3;

    [Header("Fire variables")]
    public float fireRate = 0.25f;
    public GameObject rayPrefab;
    public float raySpeed = 20f;
    private float _fireTime;
    
    [Header("Score variables")] 
    public int score;

    private Renderer _renderer;

    // Start is called before the first frame update
    private void Start()
    {
        _renderer = GetComponent<Renderer>();
        PlayerLive();
    }

    private void Update()
    {
        if (Dead && Time.time - _timeOfDeath > timeDead)
        {
            PlayerLive();
        }
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (!Dead)
        {
            MoveUpdate();
            FireUpdate();
        }
    }

    private void MoveUpdate()
    {
        // Up and Down movement
        if (Input.GetKey(KeyCode.UpArrow))
            transform.Translate(Time.fixedDeltaTime * movementSpeed * Vector2.up);
        if(Input.GetKey(KeyCode.DownArrow))
            transform.Translate(Time.fixedDeltaTime * movementSpeed * Vector2.down);
        
        // Left and Right rotation
        if(Input.GetKey(KeyCode.LeftArrow))
            transform.Rotate(Time.fixedDeltaTime * rotationSpeed * Vector3.forward);
        if(Input.GetKey(KeyCode.RightArrow))
            transform.Rotate(Time.fixedDeltaTime * rotationSpeed * Vector3.back);
    }

    private void FireUpdate()
    {
        if (Input.GetKey(KeyCode.Space) && Time.time - _fireTime > fireRate)
        {
            Fire();
        }
    }

    private void Fire()
    {
        _fireTime = Time.time;
        var rayInstance = Instantiate(rayPrefab, transform.position, transform.rotation);
        rayInstance.GetComponent<Rigidbody2D>().velocity = raySpeed * transform.up;
        rayInstance.GetComponent<RayController>().PlayerController = this;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // If the player collides with a hole, he instantly dies
        if (other.CompareTag("Hole"))
        {
            PlayerDie();
        }

        // If the player collides with an enemy, the enemy dies and the player loses a life point
        if (other.CompareTag("Enemy"))
        {
            var enemyController = other.GetComponent<EnemyController>();
            enemyController.Die();
            Health -= 1;
            if(Health <= 0)
                PlayerDie();
        }
    }

    private void PlayerDie()
    {
        Dead = true;
        _renderer.enabled = false;
        _timeOfDeath = Time.time;
        score -= 3;
        if (score < 0)
            score = 0;
    }

    private void PlayerLive()
    {
        ResetPlayer();
        Dead = false;
        _renderer.enabled = true;
    }

    private void ResetPlayer()
    {
        transform.position = new Vector2(baseX, baseY);
        transform.rotation = Quaternion.AngleAxis(baseRot, Vector3.forward);
        _fireTime = Time.time - fireRate;
        Health = baseHealth;
    }
}
