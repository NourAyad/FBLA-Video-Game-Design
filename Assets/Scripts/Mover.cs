using UnityEngine;
using System.Collections;

public class Mover : MonoBehaviour
{
    private Rigidbody objectRigidBody;
    public float speed;

    void Start()
    {
        objectRigidBody = GetComponent<Rigidbody>();
        objectRigidBody.velocity = transform.forward * speed;
    }
}
