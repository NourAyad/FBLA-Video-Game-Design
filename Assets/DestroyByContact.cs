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
       // Debug.Log(gameObject.transform.position);
        Debug.Log(rb.velocity);
    }
	
    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Hit");
        if (other.CompareTag("Boundary") || other.CompareTag("Enemy"))
        {
            return;
        }

        Destroy(other.gameObject);
        Destroy(gameObject);
    }

}
