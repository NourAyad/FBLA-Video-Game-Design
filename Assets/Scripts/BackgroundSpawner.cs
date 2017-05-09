using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundSpawner : MonoBehaviour {

    public GameObject[] planets;
    public Vector3 spawnValues;
    public float spawnWait;
    public GameController gameController;
    public bool running;
    public bool started;

	// Use this for initialization
	void Start () {
        running = true;
        started = false;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (!started && running)
        {
            StartCoroutine(SpawnBackground());
            started = true;
        }
	}

    public IEnumerator SpawnBackground()
    {
        started = true;
        Debug.Log("background started");
        //initial wait time
        yield return new WaitForSeconds(5);
        //To avoid having to recall this function everytime the player dies, the function will instead just run continuously
        while (running) {
            //If the player is dead don't spawn planets, this is like pausing the starfield
            if (!gameController.playerDead)
            {
                //Picks a random planet from the array
                GameObject planet = planets[Random.Range(0, planets.Length)];

                //Picks a random position and rotation to spawn the planet
                Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                Quaternion spawnRotation = Quaternion.identity;

                //Spawns selected planet at selected location
                Instantiate(planet, spawnPosition, spawnRotation);
                Debug.Log("Spawn planet");
                //Wait for set amount of seconds before spawning next planet
                yield return new WaitForSeconds(spawnWait);
            } 
            if (!running)
            {
                break;
            }          
        }
       
    }
}
