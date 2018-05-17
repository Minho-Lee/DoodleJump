using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameMaster : MonoBehaviour {

	public static GameMaster gm;

	public Camera camera;

	void Awake () 
	{
		// Ensure Singleton class
		if (gm == null)
		{
			gm = this;
		}

		if (camera == null)
		{
			camera = Camera.main;
		}
	}

	[SerializeField]
	Player player;

	[SerializeField]
	private Text commentText;
//	[SerializeField]
//	private Text testingText;

	public Transform enemyPrefab;

	private float scoreMultiplier;
	private static int _score;
	public static int Score {
		get { return _score; }
		set { _score = value; }
	}

	float maxPosY = 0f;
	float currPosY;

	private int victory = 150;

	[SerializeField]
	private GameObject pauseMenu;

	public delegate void PauseMenuCallback(bool _active);
	public PauseMenuCallback onTogglePauseMenu;

	// caching
	private AudioManager audioManager;

	string gameOverSound = "GameOver";
	string victorySound = "Victory";

	void Start () 
	{
		Debug.Log (Application.platform);

		audioManager = AudioManager.instance;
		if (audioManager == null) {
			Debug.LogError ("No AudioManager instance found in GM");
		}
		if (enemyPrefab == null)
		{
			Debug.LogError ("No EnemyPrefab found in GM!");
		}
		if (commentText == null)
		{
			Debug.LogError ("No commentText Found in GM!");
		}
	}


	void Update()
	{
		gm.UpdateScore ();
		if (Application.platform.ToString () == "OSXEditor" || 
			Application.platform.ToString () == "OSXPlayer")
		{
			if (Input.GetKeyDown (KeyCode.P)) {
				TogglePauseMenu ();
			}
//			if (Input.GetKeyDown (KeyCode.Mouse1)){
//				testingText.text = "" + camera.ScreenToWorldPoint (commentText.transform.position);
//				commentText.text = "" + camera.ScreenToWorldPoint (Input.mousePosition);	
//			}

		}
		// Mobile Devices
		else {
			if (Input.touchCount > 0) {
				Touch touch = Input.GetTouch (0);
				if (touch.phase == TouchPhase.Began) {
					
					Ray ray = camera.ScreenPointToRay (touch.position);
					float _posY = camera.ScreenToWorldPoint (commentText.transform.position).y;
					//testingText.text = "" + camera.ScreenToWorldPoint (touch.position);
					if (-2 < ray.origin.x && ray.origin.x < 2 &&
						_posY - 0.7 < ray.origin.y && ray.origin.y <= _posY)
					{
						TogglePauseMenu ();
					}
				}
			}
		}
		if (Input.GetKeyDown (KeyCode.Escape))
			Application.Quit ();


	}

	void TogglePauseMenu()
	{
		pauseMenu.SetActive (!pauseMenu.activeSelf);
		onTogglePauseMenu.Invoke (pauseMenu.activeSelf);

	}

	public static void KillPlayer(Player _player)
	{
		Destroy (_player.gameObject);
		Debug.Log ("Player DEAD!");
		gm.EndGame ();
	}

	public static void KillEnemy(Enemy enemy)
	{
		Debug.Log ("Enemy Killed!");
		gm._KillEnemy (enemy);
	}

	private void _KillEnemy(Enemy _enemy)
	{
		// TODO: Add Particle Effects

		// TODO: Camera Shake

		Destroy (_enemy.gameObject);
	}

	public static void SpawnEnemy(Vector3 pos)
	{
		gm._SpawnEnemy (pos);
	}
	private void _SpawnEnemy(Vector3 _pos)
	{
		Instantiate (enemyPrefab, _pos, Quaternion.identity);
	}
		
	public void EndGame()
	{
		Debug.Log ("Game Over!");
		audioManager.PlaySound (gameOverSound);
		StartCoroutine (Restart ());

	}
	IEnumerator Restart()
	{
		yield return new WaitForSeconds (3f);
		SceneManager.LoadScene ("MainLevel");
	}

	bool _flag = false;
	private void UpdateScore ()
	{
		// Check if player is still alive
		if (player != null) {
			currPosY = player.transform.position.y;
			//Debug.Log (maxPosY + " / " + currPosY);
			if (currPosY > maxPosY) {
				_score = (int) Mathf.Ceil (currPosY);
				maxPosY = currPosY;
			}
		}
		if (_score > victory && _flag == false)
		{
			audioManager.PlaySound (victorySound);
			_flag = true;
		}
	}
}
