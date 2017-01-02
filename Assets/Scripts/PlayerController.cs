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
    
    
	// Use this for initialization
	void Start ()
    {
        playerRigidBody = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update ()
    {
	
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
