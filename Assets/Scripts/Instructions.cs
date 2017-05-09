﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Instructions : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
        Screen.SetResolution(476, 800, false);	
	}
	
	// Update is called once per frame
	void Update ()
    {
	    if(Input.GetKey(KeyCode.Space))
        {
            SceneManager.LoadScene("Game");
        } else if (Input.GetKey(KeyCode.Escape)) {
            SceneManager.LoadScene("MainMenu");
        }
        
	}
}
