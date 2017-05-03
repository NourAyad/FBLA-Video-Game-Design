using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundSpawner : MonoBehaviour {

    public GameObject[] planets;
    public Vector3 spawnValues;
    public float spawnWait;
    private GameController gameController;
    private bool running;

	// Use this for initialization
	void Start () {
        running = true;
        StartCoroutine(SpawnBackground());
	}
	
	// Update is called once per frame
	void Update () {
	}

    IEnumerator SpawnBackground()
    {
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

                //Wait for set amount of seconds before spawning next planet
                yield return new WaitForSeconds(spawnWait);
            }           
        }
    }
}
