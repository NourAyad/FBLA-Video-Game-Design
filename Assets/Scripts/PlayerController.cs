using UnityEngine;
using System.Collections;

[System.Serializable]
public class Boundary
{
    public float xMin, xMax, zMin, zMax;
}

public class PlayerController : MonoBehaviour
{

    private Rigidbody playerRigidBody;
    public Boundary boundary;
    public float speed;

    public GameObject shot;
    public Transform shotSpawn;
    public float fireRate;
    private float nextFire;
    
    
	// Use this for initialization
	void Start ()
    {
        playerRigidBody = GetComponent<Rigidbody>();
        nextFire = Time.time + fireRate;
	}
	
	// Update is called once per frame
	void Update ()
    {
	    if (Input.GetKey("space") && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
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
}
