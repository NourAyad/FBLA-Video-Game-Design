using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSpeedPowerUp : MonoBehaviour {

    private Rigidbody rb;
    private GameObject player;
    private PlayerController playerController;
    private GameController gameController;
    // Use this for initialization
    void Start ()
    {
        rb = GetComponent<Rigidbody>();
        player = GameObject.FindWithTag("Player");

        if (player != null)
        {
            playerController = player.GetComponent<PlayerController>();
        }
        else
        {
            Debug.Log("Player is dead");
        }
        GetComponent<PlayerController>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerController.fireRate *= .6f;
            Destroy(gameObject);
        }
    }
}
