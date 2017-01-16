using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

    public GameObject[] hazards;
    public Vector3 spawnValues;
    public int hazardsPerWave;
    private int hazardsInWave;
    public float spawnWait;
    public float startWait;
    public float waveWait;

    private bool gameOver;
    private AudioSource audio;
	// Use this for initialization
	void Start ()
    {
        audio = GetComponent<AudioSource>();
        gameOver = false;
        hazardsInWave = hazardsPerWave;

        StartCoroutine(SpawnWaves());

	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    IEnumerator SpawnWaves ()
    {
        yield return new WaitForSeconds(startWait);
        audio.Play();
        while(true)
        {
            for (int i = 0; i < hazardsInWave; i++)
            {
                GameObject hazard = hazards[Random.Range(0, hazards.Length)];
                Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                Quaternion spawnRotation = Quaternion.identity;
                Instantiate(hazard, spawnPosition, spawnRotation);
                yield return new WaitForSeconds(spawnWait);
            }
            yield return new WaitForSeconds(waveWait);

            if(gameOver)
            {
                GameOver();
                break;
            }
        }
    }

    void GameOver()
    {

    }

}
