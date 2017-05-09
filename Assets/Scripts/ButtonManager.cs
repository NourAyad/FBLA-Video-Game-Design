using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
        Screen.SetResolution(476, 700, false);
	}
	
	// Update is called once per frame
	void Update ()
    {
		if(Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }
	}

    public void NewGameButton(string level)
    {
        SceneManager.LoadScene(level);
    }

    public void InstructionsGameButton(string level)
    {
        SceneManager.LoadScene(level);
    }

    public void LeaderboardButton(string level)
    {
        SceneManager.LoadScene(level);
    }

    public void ExitButton()
    {
        Application.Quit();
    }
}
