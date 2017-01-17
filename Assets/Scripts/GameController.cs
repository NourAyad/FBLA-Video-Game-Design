using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameController : MonoBehaviour {

    public GameObject[] hazards;
    public Vector3 spawnValues;
    public int hazardsPerWave;
    private int hazardsInWave;
    public int hazardsInWaveLevel2;
    public int hazardsInWaveLevel3;
    public float spawnWait;
    public float startWait;
    public float waveWait;

    public ParticleSystem starField;
    public ParticleSystem starField2;

    private int levelNumber;
    public int numberOfWavesInLevel1;
    public int numberOfWavesInLevel2;
    public int numberOfWavesInLevel3;
    public float levelWait;

    public int score;
    public Text scoreText;
    public Text livesText;
    public Text waveText;
    public Text hintText;
    public Text indicatorText;
    public int health;

    public bool gameOver;
    public bool win;


    private AudioSource audio;





    // Use this for initialization
    void Start ()
    {
        Screen.SetResolution(800, 900, false);
        audio = GetComponent<AudioSource>();
        gameOver = false;
        win = false;
        hazardsInWave = hazardsPerWave;
        
        //Initialize UI
        scoreText.text = "SCORE: " + score;
        livesText.text = "LIVES: " + health;
        hintText.text = "";
        indicatorText.text = "Starting...";

        //Begin hazard spawning
        StartCoroutine(SpawnWaves());

	}
	
	// Update is called once per frame
	void Update ()
    {
        //Keep UI updated
        UpdateText();

        if (Input.GetKey(KeyCode.Escape))
        {
            SceneManager.LoadScene("MainMenu");
        }
        if(gameOver || win)
        {
            if(Input.GetKey(KeyCode.M))
            {
                SceneManager.LoadScene("MainMenu");
            }
        }
	}

    IEnumerator SpawnWaves ()
    {
        int hazardsLevel1 = hazards.Length - 12;
        int hazardsLevel2 = hazards.Length - 4;
        int hazardsLevel3 = hazards.Length;

        //Initial intro prompts
        yield return new WaitForSeconds(1);
        waveText.text = "READY, CAPTAIN?";
        yield return new WaitForSeconds(startWait);
        waveText.text = "";
        yield return new WaitForSeconds(.5f);

        //Start background music
        audio.Play();

        waveText.text = "LEVEL 1";
        hintText.text = "Good Luck! We're entering an asteroid field! Remember, spacebar to shoot!";
        indicatorText.text = "L1" + "-" + "W1";
        yield return new WaitForSeconds(4);
        waveText.text = "";
        hintText.text = "";


        while(true)
        {
            
            //Level 1
            //Loop spawning each wave
            for (int i = 1; i <= numberOfWavesInLevel1; i++)
            {
                //Check if player is dead
                if (gameOver)
                {
                    GameOver();
                    break;
                }


                //Wave Prompt
                waveText.text = "WAVE " + i;
                indicatorText.text = "L1-W" + i;
                yield return new WaitForSeconds(1);
                waveText.text = "";
                //Spawn Hazards
                for (int j = 0; j < hazardsInWave; j++)
                {
                    //Random Hazard from defined array of hazards excluding the last 12, which are reserved for future levels
                    GameObject hazard = hazards[Random.Range(0, hazardsLevel1)];
                    //Spawn randomly selected hazard in the spawn range
                    Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                    Quaternion spawnRotation = Quaternion.identity;
                    Instantiate(hazard, spawnPosition, spawnRotation);
                    //Time between each hazard spawn
                    yield return new WaitForSeconds(spawnWait);

                    //Check to ensure player is not dead
                    if (gameOver)
                    {
                        GameOver();
                        break;
                    }
                }
                //Increase number of hazards for next wave
                hazardsInWave += 5;

                //Check to ensure player has not died
                if (gameOver)
                {
                    GameOver();
                    break;
                }

                //Pause before beginning next wave
                yield return new WaitForSeconds(waveWait);
            }

            //Check to ensure player has not died
            if (gameOver)
            {
                GameOver();
                break;
            }

            //prompts once level is complete
            waveText.text = "LEVEL 1 COMPLETE!";
            hintText.text = "Well done Captain!";
            yield return new WaitForSeconds(levelWait);
            waveText.text = "";
            hintText.text = "";
            yield return new WaitForSeconds(1);

            //Begin Level 2
            waveText.text = "LEVEL 2";
            hintText.text = "We're entering a denser asteroid field! Watch out for faster asteroids!";
            indicatorText.text = "Level 2 starting...";
            yield return new WaitForSeconds(4);
            waveText.text = "";
            hintText.text = "";
            yield return new WaitForSeconds(1);

            for (int i = 1; i <= numberOfWavesInLevel2; i++)
            {
                //Wave Prompt
                waveText.text = "WAVE " + i;
                indicatorText.text = "L2-W" + i;
                yield return new WaitForSeconds(1);
                waveText.text = "";
                //Spawn Hazards
                for (int j = 0; j < hazardsInWaveLevel2; j++)
                {
                    //Random Hazard from defined array of hazards excluding the last , which are reserved for future levels
                    GameObject hazard = hazards[Random.Range(0, hazardsLevel2)];
                    //Spawn randomly selected hazard in the spawn range
                    Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                    Quaternion spawnRotation = Quaternion.identity;
                    Instantiate(hazard, spawnPosition, spawnRotation);
                    //Time between each hazard spawn
                    yield return new WaitForSeconds(spawnWait);

                    //Check to ensure player is not dead
                    if (gameOver)
                    {
                        GameOver();
                        break;
                    }
                }
                //Increase number of hazards for next wave
                hazardsInWaveLevel2 += 5;

                //Check to ensure player has not died
                if (gameOver)
                {
                    GameOver();
                    break;
                }


                //Pause before beginning next wave
                yield return new WaitForSeconds(waveWait);
            }

            if (gameOver)
            {
                GameOver();
                break;
            }

            //prompts once level is complete
            waveText.text = "LEVEL 2 COMPLETE!";
            hintText.text = "Excellent work Captain! We're almost back to the base!";
            yield return new WaitForSeconds(2);
            waveText.text = "";
            hintText.text = "";
            yield return new WaitForSeconds(1);

            //Begin Level 2
            waveText.text = "LEVEL 3";
            hintText.text = "Oh no! Enemy ships inbound! Watch out for their guns!";
            indicatorText.text = "Level 3 starting...";
            yield return new WaitForSeconds(3);
            waveText.text = "";
            hintText.text = "";
            yield return new WaitForSeconds(1);

            for (int i = 1; i <= numberOfWavesInLevel3; i++)
            {
                //Wave Prompt
                waveText.text = "WAVE " + i;
                indicatorText.text = "L3-W" + i;
                yield return new WaitForSeconds(1);
                waveText.text = "";
                //Spawn Hazards
                for (int j = 0; j < hazardsInWaveLevel3; j++)
                {
                    //Random Hazard from defined array of hazards with enemy ships
                    GameObject hazard = hazards[Random.Range(0, hazardsLevel3)];
                    //Spawn randomly selected hazard in the spawn range
                    Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                    Quaternion spawnRotation = Quaternion.identity;
                    Instantiate(hazard, spawnPosition, spawnRotation);
                    //Time between each hazard spawn
                    yield return new WaitForSeconds(spawnWait);

                    //Check to ensure player is not dead
                    if (gameOver)
                    {
                        GameOver();
                        break;
                    }
                }
                //Increase number of hazards for next wave
                hazardsInWaveLevel3 += 5;

                //Check to ensure player has not died
                if (gameOver)
                {
                    GameOver();
                    break;
                }


                //Pause before beginning next wave
                yield return new WaitForSeconds(waveWait);
            }

            if (gameOver)
            {
                GameOver();
                break;
            }

            WinSequence();
            break;
        }
    }

    public void AddScore (int value)
    {
        score += value;
    }

    void UpdateText()
    {
        scoreText.text = "SCORE: " + score;
        livesText.text = "LIVES: " + health;
    }
    void GameOver()
    {
        waveText.text = "GAME OVER";
        hintText.text = "Oh no! You're ship has been blasted to bits! Press M to go back to the main menu and try again!";
        indicatorText.text = "Lost!";
        starField.Pause();
        starField2.Pause();
       
    }

    void WinSequence()
    {
        waveText.text = "YOU WIN!";
        hintText.text = "You did it! Amazing work Captain! You're final score is " + score + "! Wow! Press M to go back to the main menu and do it all again!";
        indicatorText.text = "Won!";
        starField.Pause();
        starField2.Pause();
        win = true;
    }
    IEnumerator SpawnWave(int numberofHazards, float spawnRate)
    {
        for (int i = 0; i < numberofHazards; i++)
        {
            GameObject hazard = hazards[Random.Range(0, hazards.Length)];
            Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
            Quaternion spawnRotation = Quaternion.identity;
            Instantiate(hazard, spawnPosition, spawnRotation);
            yield return new WaitForSeconds(spawnRate);
        }
    }

    IEnumerator UpdateWaveText(string text)
    {
        waveText.text = text;
        yield return new WaitForSeconds(1);
        waveText.text = "";
    }
}
