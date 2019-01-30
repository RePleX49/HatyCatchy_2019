//HATY CATCHY by m@ boch - NYU GAMECENTER - Oct 2016
//modifications by Bennett Foddy - Jan 2019

using UnityEngine;
using System.Collections;

public class HatSpawner : MonoBehaviour {
    //public variables are accessible by other scripts, and are often set in the inspector
    //they're great for tunable variables, like these, since we can edit them in play mode.
    public GameObject hatPrefab;
    public GameObject powerupPrefab;
    private int HatsSpawned = 0;
    public int SpeedUpThreshold = 5;
    public float speedUpRate = 0.1f; //rate number by which we decrease timeBetweenSpawns by
    public float timeBetweenSpawns; //interval of time to wait between spawn in seconds
    public float minTimeBetweenSpawns = 0.4f; //minimum interval of time between spawn in seconds
    public float powerUpFrequency = 0.08f; //frequency of the powerup to spawn, the greater the more frequent
   
    public float xSpawnPosMin; //left most spawn point
    public float xSpawnPosMax; //right most spawn point
    public float ySpawnPos; //height of spawn

    //private variables can't be accessed by other scripts
    private float timeUntilSpawn;

    public void Start()
    {
        timeUntilSpawn = 1;
    }

    public void Update()
    {
        //Time.deltaTime is how much time has occured since the last update. 
        //If we decrease the timer by Time.deltaTime every time Update() runs, the timer will decrease by 1.0 every second.
        timeUntilSpawn -= Time.deltaTime;
        //Once timeUntilSpawn is less than 0, we spawn a new hat
        if (timeUntilSpawn <= 0)
        {
            SpawnHat();
            HatsSpawned++;
            //then we reset timeUntilSpawn to the timeBetweenSpawns & start all over again

            if(HatsSpawned == SpeedUpThreshold && timeBetweenSpawns > minTimeBetweenSpawns)
            {
                timeBetweenSpawns -= speedUpRate;
                HatsSpawned = 0;
            }
            
            timeUntilSpawn = timeBetweenSpawns;
        }
    }

    private void SpawnHat()
    {
        //Generate a new spawn position. 
        //For the first value of this Vector3 (x) we use Random.Range to get a position between our min & max X values
        //The second (y) is just the height we spawn at
        //The third (z) is the depth, which is set to 0 as we're in 2D
        Vector3 newPos = new Vector3(Random.Range(xSpawnPosMin, xSpawnPosMax), ySpawnPos, 0);
        //Instantiate creates a new object from a prefab. It needs the prefab, the position, and the rotation as arguements
        //newPos is the position we made, Quaternion.identity just means that we're not rotating the sprite
        if(Random.Range(0.0f, 10.0f) < powerUpFrequency)
        {
            Instantiate(powerupPrefab, newPos, Quaternion.identity);
        }
        else
        {
            Instantiate(hatPrefab, newPos, Quaternion.identity);
        }
        
    }

 

}
