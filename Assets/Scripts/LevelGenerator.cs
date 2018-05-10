using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour {

	public GameObject platformPrefab;
	public GameObject bluePlatformPrefab;

	public int numOfPlatforms = 200;
	public float levelWidth = 3f;
	public float minY = .3f;
	public float maxY = 1.2f;

	// Use this for initialization
	void Start () {
		Vector3 spawnPosition = new Vector3();
		int chance;
		for (int i = 0;i < numOfPlatforms ; i++)
		{
			spawnPosition.x = Random.Range (-levelWidth, levelWidth);
			spawnPosition.y += Random.Range (minY, maxY);
			chance = Random.Range (1, 100);
			if (chance < 80) {
				Instantiate (platformPrefab, spawnPosition, Quaternion.identity);
			} 
			else
			{
				Instantiate (bluePlatformPrefab, spawnPosition, Quaternion.identity);
			}
		}	 
	}
}
