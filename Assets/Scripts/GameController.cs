using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameController : MonoBehaviour {

    public GameObject[] hazards;
    public Vector3 spawnValues;
    public int hazardsPerWave;
    private int hazardsInWave;
    public float spawnWait;
    public float startWait;
    public float waveWait;

    private int levelNumber;
    public int numberOfWavesInLevel1;

    public int score;
    public Text scoreText;
    public Text livesText;
    public Text waveText;  
    public int health;

    public bool gameOver;

    private AudioSource audio;
    

    // Use this for initialization
    void Start ()
    {
        audio = GetComponent<AudioSource>();
        gameOver = false;
        hazardsInWave = hazardsPerWave;

        scoreText.text = "SCORE: " + score;
        livesText.text = "LIVES: " + health;


        StartCoroutine(SpawnWaves());

	}
	
	// Update is called once per frame
	void Update ()
    {
        UpdateText();
	}

    IEnumerator SpawnWaves ()
    {
        waveText.text = "READY?";
        yield return new WaitForSeconds(startWait);
        waveText.text = "";
        yield return new WaitForSeconds(.5f);
        waveText.text = "LEVEL 1";
        yield return new WaitForSeconds(.5f);
        audio.Play();
        while(true)
        {
            
            for (int i = 0; i <= numberOfWavesInLevel1; i++)
            {
                //StartCoroutine(SpawnWave(hazardsInWave, spawnWait));
                for (int j = 0; j < hazardsInWave; j++)
                {
                    GameObject hazard = hazards[Random.Range(0, hazards.Length)];
                    Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                    Quaternion spawnRotation = Quaternion.identity;
                    Instantiate(hazard, spawnPosition, spawnRotation);
                    yield return new WaitForSeconds(spawnWait);
                }
                hazardsInWave += 5;
            }
            yield return new WaitForSeconds(waveWait);

            if(gameOver)
            {
                GameOver();
                break;
            }
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
}
