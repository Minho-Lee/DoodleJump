using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour {

	public static LevelGenerator LG;

	public GameObject platformPrefab;
	public GameObject bluePlatformPrefab;
	public GameObject whitePlatformPrefab;
	public GameObject redPlatformPrefab;

	public int numOfPlatforms = 200;
	public float levelWidth = 3f;
	public float minY = 1.2f;
	public float maxY = 2f;

	GameObject platforms;

	void Awake()
	{
		if (LG == null)
		{
			LG = this;
		}
	}
	// Use this for initialization
	void Start () {
		platforms = GameObject.FindGameObjectWithTag ("Platforms");
		if (platforms == null){
			Debug.LogError ("Platforms Gameobject Not Found!");
			return;
		}
		SpawnPlatforms ();
	}

	void SpawnPlatforms()
	{
		Vector3 spawnPosition = new Vector3();
		int chance;
		for (int i = 0;i < numOfPlatforms ; i++)
		{
			spawnPosition.x = Random.Range (-levelWidth, levelWidth);
			spawnPosition.y += Random.Range (minY, maxY);
			chance = Random.Range (1, 100);
			if (chance < 83) {
				GameObject _clone = Instantiate (platformPrefab, spawnPosition, Quaternion.identity);
				_clone.transform.parent = platforms.transform;
			} else if (chance < 90) {
				GameObject _clone = Instantiate (bluePlatformPrefab, spawnPosition, Quaternion.identity);
				_clone.transform.parent = platforms.transform;
			} else if (chance < 95) {
				GameObject _clone = Instantiate (whitePlatformPrefab, spawnPosition, Quaternion.identity);
				_clone.transform.parent = platforms.transform;
			} else {
				float platformSizeX = redPlatformPrefab.GetComponent<SpriteRenderer> ().bounds.size.x;

				if (0f < spawnPosition.x && spawnPosition.x < platformSizeX / 2)
				{
					spawnPosition.x += platformSizeX / 2;
				}
				if ( -platformSizeX / 2 < spawnPosition.x && spawnPosition.x < 0f)
				{
					spawnPosition.x -= platformSizeX / 2;
				}

				GameObject _redClone = Instantiate (redPlatformPrefab, spawnPosition, Quaternion.identity);
				GameObject _clone = Instantiate (platformPrefab,
					new Vector3(-spawnPosition.x, spawnPosition.y + Random.Range (-0.5f, 0.5f)), 
					Quaternion.identity);
				_redClone.transform.parent = platforms.transform;
				_clone.transform.parent = platforms.transform;
			}
		}	 
	}
}
