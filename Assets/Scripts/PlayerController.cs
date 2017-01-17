﻿using UnityEngine;
using System.Collections;

[System.Serializable]
public class Boundary
{
    public float xMin, xMax, zMin, zMax;
}

public class PlayerController : MonoBehaviour
{

    private Rigidbody playerRigidBody;
    public GameController gameController;
    private AudioSource audio;

    public Boundary boundary;
    public float speed;

    public int maxHealth;
    private int health;

    public GameObject shot;
    public Transform shotSpawn;
    public float fireRate;
    private float nextFire;

    public GameObject deathExplosion;
    
    
	// Use this for initialization
	void Start ()
    {
        playerRigidBody = GetComponent<Rigidbody>();
        nextFire = Time.time + fireRate;
        health = maxHealth;
        gameController.health = health;
        audio = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update ()
    {
	    if (Input.GetKey(KeyCode.Space) && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
            audio.Play();
        }

        if(health <= 0)
        {
            Die();
        }
	}

    void FixedUpdate ()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        playerRigidBody.velocity = movement * speed;
        playerRigidBody.position = new Vector3(
            Mathf.Clamp(playerRigidBody.position.x, boundary.xMin, boundary.xMax),
            0.0f,
            Mathf.Clamp(playerRigidBody.position.z, boundary.zMin, boundary.zMax)
            );
    }

    void Die()
    {
        gameController.gameOver = true;
        Instantiate(deathExplosion, transform.position, transform.rotation);
        Destroy(gameObject);
    }

    public void LoseLife(int dmg)
    {
        health -= dmg;
        gameController.health = health; 

    }
}
