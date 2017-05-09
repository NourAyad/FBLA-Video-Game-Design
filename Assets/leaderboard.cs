using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class leaderboard : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
        Screen.SetResolution(476, 700, false);	
	}
	
	// Update is called once per frame
	void Update ()
    {
	    if (Input.GetKeyUp(KeyCode.Escape))
        {
            SceneManager.LoadScene("MainMenu");
        }	
	}
}
