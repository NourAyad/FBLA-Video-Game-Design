using UnityEngine;
using System.Collections;

public class DestroyByContact : MonoBehaviour {

    private Rigidbody rb;
	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
	}
	
	void Update()
    {
    
    }
	
    void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Boundary") || other.CompareTag("Enemy"))
        {
            return;
        }

        Destroy(other.gameObject);
        Destroy(gameObject);
    }

}
