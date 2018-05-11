using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour {

	public static GameMaster gm;

	void Awake () 
	{
		// Ensure Singleton class
		if (gm == null)
		{
			gm = this;
		}
	}

	[SerializeField]
	Player player;

	private float scoreMultiplier;
	private static int _score;
	public static int Score {
		get { return _score; }
		set { _score = value; }
	}

	float maxPosY = 0f;
	float currPosY;

	private int victory = 50;

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
		audioManager = AudioManager.instance;
		if (audioManager == null) {
			Debug.LogError ("No AudioManager instance found in GM");
		}
	}

	void Update()
	{
		gm.UpdateScore ();
		if (Input.GetKeyDown (KeyCode.P))
		{
			TogglePauseMenu ();
		}
	}

	void TogglePauseMenu()
	{
		pauseMenu.SetActive (!pauseMenu.activeSelf);
		onTogglePauseMenu.Invoke (pauseMenu.activeSelf);

	}

	public static void KillPlayer(Player _player)
	{
		Destroy (_player.gameObject);
		gm.EndGame ();
	}

	public void EndGame()
	{
		Debug.Log ("Game Over!");
		audioManager.PlaySound (gameOverSound);
	}

	bool _flag = false;
	void UpdateScore ()
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
