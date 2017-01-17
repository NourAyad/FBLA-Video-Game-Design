using UnityEngine;
using System.Collections;

public class DestroyByContact : MonoBehaviour {

    public int damage = 1;
    public int score;
    public GameObject asteroidExplosion;
    private Rigidbody rb;
    private PlayerController player;
    private GameController gameController;
	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
        player = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        gameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();
	}
	
	void Update()
    {
    
    }
	
    void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Boundary") || other.CompareTag("Enemy"))
        {
            return;
        } else if (other.CompareTag("Player")) {
            player.LoseLife(damage);
            Destroy(gameObject);
            Instantiate(asteroidExplosion, transform.position, transform.rotation);
        } else {
            Destroy(other.gameObject);
            Instantiate(asteroidExplosion, transform.position, transform.rotation);
            Destroy(gameObject);
            gameController.AddScore(score);
        }

    }

}
