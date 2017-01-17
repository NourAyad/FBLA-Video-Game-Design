using UnityEngine;
using System.Collections;

public class WeaponController : MonoBehaviour {

    public GameObject shot;
    public Transform shotSpawn;
    public Transform shotSpawn2;
    public float fireRate;
    public float delay;

    //private AudioSource audioSource;

	void Start ()
    {
        //audioSource = GetComponent<AudioSource>();
        InvokeRepeating("Fire", delay, fireRate);    
	}

    void Fire ()
    {
        Instantiate(shot, shotSpawn.position, shotSpawn.rotation);

        if (shotSpawn2 != null)
        {
            Instantiate(shot, shotSpawn2.position, shotSpawn2.rotation);
        }
        //audioSource.Play();
    }
	
	
}
