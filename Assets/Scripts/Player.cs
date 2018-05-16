using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(SpriteRenderer))]
public class Player : MonoBehaviour {

	public Camera camera;

	public int fallBoundary = -20;

	Rigidbody2D rb;
	SpriteRenderer sr;

	float leftConstraint;
	float rightConstraint;
	float spriteSizeX;

	private List<GameObject> _platforms = new List<GameObject>();

	// caching
	private AudioManager audioManager;

	void Awake ()
	{
		if (camera == null)
		{
			camera = Camera.main;
		}
	}

	// Use this for initialization
	void Start () {
		rb = GetComponent <Rigidbody2D> ();
		sr = GetComponent <SpriteRenderer> ();
		// Since Camera has z-value of -10, need to factor that in.
		leftConstraint = camera.ScreenToWorldPoint (new Vector3 (0f, 0f, 10f)).x;
		rightConstraint = camera.ScreenToWorldPoint (new Vector3(Screen.width, 0f, 10f)).x;
		spriteSizeX = sr.bounds.size.x;
		if (camera == null)
		{
			Debug.LogError ("No Camera referenced in Player!");
		}

		audioManager = AudioManager.instance;
		if (audioManager == null) {
			Debug.LogError ("No AudioManager instance found in GM");
		}
		StartCoroutine (FetchPlatforms (LevelGenerator.loadingTime));

		GameMaster.gm.onTogglePauseMenu += OnPauseMenuToggle;
	}
	IEnumerator FetchPlatforms(float _time) 
	{
		yield return new WaitForSeconds (_time);
		_platforms = LevelGenerator.platformList;
	}
	
	// Update is called once per frame
	void Update () {
		
		//movement = Input.GetAxis ("Horizontal") * movementSpeed;
		Vector3 screenPos = camera.WorldToScreenPoint (transform.position);
		// Remove platforms
		if (_platforms.Count > 0)
		{
			foreach (GameObject _platform in _platforms)	
			{
				if (_platform != null){
					Vector3 platformPos = camera.WorldToScreenPoint (_platform.transform.position);
					if (platformPos.y < fallBoundary + 10)
					{
						Destroy (_platform);
					}	
				} 
			}
		}
				

		if (screenPos.y < fallBoundary)
		{
			GameMaster.KillPlayer (this);
		} else {
			CheckWrapAround ();	
		}
	}
	void OnPauseMenuToggle(bool _active)
	{
		GetComponent <Rigidbody2D>().simulated = !_active;
	}

	void CheckWrapAround() 
	{
		if (transform.position.x + Mathf.Abs (spriteSizeX / 2) < leftConstraint)
		{
			transform.position = new Vector3 (rightConstraint, transform.position.y, transform.position.z);
		}
		if (transform.position.x - (spriteSizeX / 2) > rightConstraint)
		{
			transform.position = new Vector3 (leftConstraint, transform.position.y, transform.position.z);
		}
	}

	void FixedUpdate() 
	{
		Vector2 velocity = rb.velocity;
		velocity.x = PlayerControl.movement;
		rb.velocity = velocity;
	}

	void OnDestroy ()
	{
		// Unsubscribe to OnUpgradeMenuToggle
		GameMaster.gm.onTogglePauseMenu -= OnPauseMenuToggle;
	}
}
