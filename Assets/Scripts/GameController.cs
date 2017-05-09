using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameController : MonoBehaviour {

    public GameObject playerShip;
    public GameObject[] hazards;
    public GameObject AttackSpeedBoost;

    public UpgradeSystem upgradeSystem;
    public BackgroundSpawner backgroundSpawner;

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
    public bool spawnNextWave;

    public int score;
    public Text scoreText;
    public Text livesText;
    public Text waveText;
    public Text healthText;
    public Text hintText;
    public Text indicatorText;
    public Text pointsToUpgrade;

    public Image cptLazer;

    public int maxHealth;
    public int health;
    public int maxLives;
    public int lives;
    public Slider healthSlider;
    public Image damageImage;
    public float flashSpeed = 5f;
    public Color flashColor = new Color(1f, 0f, 0f, 0.4f);
    public bool damaged;

    public int lastCompletedWave;
    public bool playerAlive;
    public bool playerDead;
    public bool gameOver;
    public bool win;


    private AudioSource audio;





    // Use this for initialization
    void Start ()
    {
        Screen.SetResolution(833, 700, false);
        //DontDestroyOnLoad(this);
        audio = GetComponent<AudioSource>();
        gameOver = false;
        win = false;
        hazardsInWave = hazardsPerWave;
        
        //Initialize UI
        scoreText.text = "SCORE: " + score;
        livesText.text = "LIVES: " + lives;
        healthText.text = "HP: " + health;
        waveText.text = "";
        hintText.text = "";
        pointsToUpgrade.text = "";
        indicatorText.text = "Starting...";
        HideCaptainLazer();

        healthSlider.maxValue = maxHealth;
        healthSlider.value = health;

        damaged = false;

        lives = maxLives;
        //Begin hazard spawning
        StartCoroutine(GameStart());

	}
	
	// Update is called once per frame
	void Update ()
    {
        //Keep UI updated
        UpdateUI();

        if (playerDead)
        {
            if (lives > 0)
            {
                if (Input.GetKey(KeyCode.R))
                {
                    PlayerReset();
                    UpdateUI();
                    if (lastCompletedWave == 1)
                    {
                        StartCoroutine(SpawnLevel1());
                    }

                    if (lastCompletedWave == 2)
                    {
                        StartCoroutine(SpawnLevel2());
                    }

                    if (lastCompletedWave == 3)
                    {
                        StartCoroutine(SpawnLevel3());
                    }
                }
                
            }
            if (lives == 0)
            {
                GameOver();
            }
        }

        DamageImageFlash();

        if (spawnNextWave)
        {
            Debug.Log(lastCompletedWave);
            if (lastCompletedWave == 0)
            {
                StartCoroutine(SpawnLevel1());
                Debug.Log("Starting 1");
            } else if (lastCompletedWave == 1)
            {
                StartCoroutine(SpawnLevel2());
            } else if (lastCompletedWave == 2)
            {
                StartCoroutine(SpawnLevel3());
                Debug.Log("Starting 3");
            }

            
            spawnNextWave = false;
        }
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

    IEnumerator GameStart ()
    {
        //Initial intro prompts
        yield return new WaitForSeconds(1);
        waveText.text = "READY, CAPTAIN?";
        yield return new WaitForSeconds(startWait);
        waveText.text = "";
        yield return new WaitForSeconds(.5f);

        //Start background music
        audio.Play();

        lastCompletedWave = 0;
        spawnNextWave = true;
    }

    IEnumerator SpawnLevel1 ()
    {
        int hazardsLevel1 = hazards.Length - 12;

        lastCompletedWave = 1;
        waveText.text = "LEVEL 1";
        ShowCaptainLazer();
        hintText.text = "Good Luck! We're entering an asteroid field! Remember, spacebar to shoot!";
        indicatorText.text = "L1" + "-" + "W1";
        yield return new WaitForSeconds(4);
        waveText.text = "";
        HideCaptainLazer();
        hintText.text = "";

        //Level 1
        //Loop spawning each wave
        for (int i = 1; i <= numberOfWavesInLevel1; i++)
        {
            //Check if player is dead
            if (playerDead)
            {
                PlayerDead();
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
                if (playerDead)
                {
                    PlayerDead();
                    break;
                }
            }
            //Increase number of hazards for next wave
            hazardsInWave += 5;

            //Check to ensure player has not died
            if (playerDead)
            {
                PlayerDead();
                break;
            }

            //Pause before beginning next wave
            yield return new WaitForSeconds(waveWait);
        }
        //Check to ensure player has not died
        if (playerDead)
        {
            PlayerDead();
        }
        //If player has not died, level must be complete
        else
        {
            //prompts once level is complete
            waveText.text = "LEVEL 1 COMPLETE!";
            ShowCaptainLazer();
            hintText.text = "Well done Captain!";
            yield return new WaitForSeconds(levelWait);
            waveText.text = "";
            HideCaptainLazer();
            hintText.text = "";
            yield return new WaitForSeconds(1);
            spawnNextWave = true;
        }
    }

    IEnumerator SpawnLevel2 ()
    {
        int hazardsLevel2 = hazards.Length - 4;

        // Begin Level 2
        lastCompletedWave = 2;
        waveText.text = "LEVEL 2";
        ShowCaptainLazer();
        hintText.text = "We're entering a denser asteroid field! Watch out for faster asteroids!";
        indicatorText.text = "Level 2 starting...";
        yield return new WaitForSeconds(4);
        waveText.text = "";
        HideCaptainLazer();
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
                if (playerDead)
                {
                    PlayerDead();
                    break;
                }
            }
            //Increase number of hazards for next wave
            hazardsInWaveLevel2 += 5;

            //Check to ensure player has not died
            if (playerDead)
            {
                PlayerDead();
                break;
            }


            //Pause before beginning next wave
            yield return new WaitForSeconds(waveWait);
        }

        if (playerDead)
        {
            PlayerDead();
        }
        else
        {
            //prompts once level is complete
            waveText.text = "LEVEL 2 COMPLETE!";
            ShowCaptainLazer();
            hintText.text = "Excellent work Captain! We're almost back to the base!";
            yield return new WaitForSeconds(2);
            waveText.text = "";
            HideCaptainLazer();
            hintText.text = "";
            yield return new WaitForSeconds(1);
            spawnNextWave = true;
        }
    }

    IEnumerator SpawnLevel3 ()
    {
        int hazardsLevel3 = hazards.Length;

        //Begin Level 3
        lastCompletedWave = 3;
        waveText.text = "LEVEL 3";
        ShowCaptainLazer();
        hintText.text = "Oh no! Enemy ships inbound! Watch out for their guns!";
        indicatorText.text = "Level 3 starting...";
        yield return new WaitForSeconds(3);
        waveText.text = "";
        HideCaptainLazer();
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
                if (playerDead)
                {
                    PlayerDead();
                    break;
                }
            }
            //Increase number of hazards for next wave
            hazardsInWaveLevel3 += 5;

            //Check to ensure player has not died
            if (playerDead)
            {
                PlayerDead();
                break;
            }


            //Pause before beginning next wave
            yield return new WaitForSeconds(waveWait);
        }

        if (playerDead)
        {
            PlayerDead();
        }
        else
        {
            WinSequence();
        }
    }
    public void AddScore (int value)
    {
        score += value;
        upgradeSystem.currentScore = score;
    }

    void UpdateUI()
    {
        scoreText.text = "SCORE: " + score;
        livesText.text = "LIVES: " + lives;
        healthText.text = "HP: " + health;
        pointsToUpgrade.text = "NEXT UPGRADE IN " + (upgradeSystem.targetScore - score) + " POINTS";
        healthSlider.value = health;
    }

    void PlayerDead()
    {
        ShowCaptainLazer();
        waveText.text = "YOU DIED!";
        hintText.text = "Oh no, you're ship sustained too much damage and you've lost a life! Press R to try the level again!";
        indicatorText.text = "Exploded!";
        upgradeSystem.upgradeAvailable = false;
        upgradeSystem.upgradeInitiated = false;
        starField.Pause();
        starField2.Pause();

        backgroundSpawner.running = false;
        backgroundSpawner.started = false;
    }

    void PlayerReset ()
    {
        HideCaptainLazer();
        waveText.text = "";
        hintText.text = "";
        indicatorText.text = "Respawning!";
        Instantiate(playerShip, new Vector3(0, 0, 0), new Quaternion(0, 0, 0, 0));  
        starField.Play();
        starField2.Play();
        playerDead = false;
        upgradeSystem.FindPlayer();
        backgroundSpawner.running = true;
        
    } 
    void GameOver()
    {

        gameOver = true;
        waveText.text = "GAME OVER";
        ShowCaptainLazer();
        hintText.text = "Oh no! You're ship has been blasted to bits! Press M to go back to the main menu and try again!";
        indicatorText.text = "Lost!";
        starField.Pause();
        starField2.Pause();
       
    }

    void WinSequence()
    {
        waveText.text = "YOU WIN!";
        ShowCaptainLazer();
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

    void DamageImageFlash()
    {
        if (damaged)
        {
            damageImage.color = flashColor;
        }
        else
        {
            damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        }
        damaged = false;
    }

    void ShowCaptainLazer()
    {
        cptLazer.enabled = true;
    }

    void HideCaptainLazer()
    {
        cptLazer.enabled = false;
    }
}
