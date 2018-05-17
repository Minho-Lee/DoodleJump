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

	private float platformSizeX = 0f;

	GameObject platforms;

	public static List<GameObject> platformList = new List<GameObject>();
	public static float loadingTime = 0f;

	void Awake()
	{
		if (LG == null)
		{
			LG = this;
		}

		platforms = GameObject.Find ("Platforms");
		if (platforms == null){
			Debug.LogError ("Platforms GameObject Not Found!");
			return;
		}
		System.Diagnostics.Stopwatch st = new System.Diagnostics.Stopwatch();
		st.Start ();
		SpawnPlatforms ();
		st.Stop ();
		loadingTime = (float)(st.ElapsedMilliseconds) / 1000f;
	}

	void Start()
	{
		platformSizeX = platformPrefab.GetComponent<SpriteRenderer> ().bounds.size.x;
	}

	void SpawnPlatforms()
	{
		Vector3 spawnPosition = new Vector3();
		int chance;
		GameObject _clone;
		for (int i = 0;i < numOfPlatforms ; i++)
		{
			spawnPosition.x = Random.Range (-levelWidth, levelWidth);
			spawnPosition.y += Random.Range (minY, maxY);

			chance = Random.Range (1, 100);

			// Spawn Platforms
			if (chance < 83) {
				_clone = Instantiate (platformPrefab, spawnPosition, Quaternion.identity);
			} else if (chance < 90) 
			{
				_clone = Instantiate (bluePlatformPrefab, spawnPosition, Quaternion.identity);
			} else if (chance < 95) {
				_clone = Instantiate (whitePlatformPrefab, spawnPosition, Quaternion.identity);
				_clone.transform.parent = platforms.transform;
			} else {
				// Red Platform (spawn it with white to ensure there will be a stepping platform)
				if (0f < spawnPosition.x && spawnPosition.x < platformSizeX / 2)
				{
					spawnPosition.x += platformSizeX ;
				}
				if ( -platformSizeX / 2 < spawnPosition.x && spawnPosition.x < 0f)
				{
					spawnPosition.x -= platformSizeX;
				}
		
				GameObject _redClone = Instantiate (redPlatformPrefab, 
					new Vector3(-spawnPosition.x, spawnPosition.y, spawnPosition.z),
					Quaternion.identity);
				 _clone = Instantiate (platformPrefab,
					new Vector3(spawnPosition.x, spawnPosition.y + Random.Range (-0.5f, 0.5f)), 
					Quaternion.identity);
				_redClone.transform.parent = platforms.transform;
				platformList.Add (_redClone);
			}
			platformList.Add (_clone);	
			_clone.transform.parent = platforms.transform;

			// Spawn Enemies
			if (i > 50 && chance < 15)
			{
				GameMaster.SpawnEnemy (new Vector3(-spawnPosition.x, spawnPosition.y, spawnPosition.z));
			}

		}	 
	}
}
