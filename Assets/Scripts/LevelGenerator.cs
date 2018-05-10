using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour {

	public static LevelGenerator LG;

	public GameObject platformPrefab;
	public GameObject bluePlatformPrefab;
	public GameObject whitePlatformPrefab;

	public int numOfPlatforms = 200;
	public float levelWidth = 3f;
	public float minY = .3f;
	public float maxY = 1.2f;


	void Awake()
	{
		if (LG == null)
		{
			LG = this;
		}
	}
	// Use this for initialization
	void Start () {
		Vector3 spawnPosition = new Vector3();
		int chance;
		for (int i = 0;i < numOfPlatforms ; i++)
		{
			spawnPosition.x = Random.Range (-levelWidth, levelWidth);
			spawnPosition.y += Random.Range (minY, maxY);
			chance = Random.Range (1, 100);
			if (chance < 85) {
				Instantiate (platformPrefab, spawnPosition, Quaternion.identity);
			} else if (chance < 95) {
				Instantiate (bluePlatformPrefab, spawnPosition, Quaternion.identity);
			} else {
				Instantiate (whitePlatformPrefab, spawnPosition, Quaternion.identity);
			}
		}	 
	}
}
