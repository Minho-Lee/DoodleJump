using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour {

	Transform playerGraphics;

	public float movementSpeed = 10f;
	public static float movement = 0f;
	private float mobileMovementSpeed = 15f;

	bool active = true;

	void Awake ()
	{
		playerGraphics = GameObject.FindGameObjectWithTag ("Player").GetComponent <Transform> ();
		if (playerGraphics == null)
		{
			Debug.LogError ("No PlayerGraphics referenced in PlayerControl!");
			return;
		}
	}

	void Start()
	{
		GameMaster.gm.onTogglePauseMenu += OnPauseMenuToggle;
	}

	void OnPauseMenuToggle(bool _active)
	{
		active = !_active;
	}

	void Update()
	{
		if (active && (Application.platform.ToString () == "OSXEditor" ||
					   Application.platform.ToString () == "OSXPlayer")) {
			movement = Input.GetAxis ("Horizontal") * movementSpeed;

		} 
		// Mobile control
		else {
			// use accelerometer
			movement = Input.acceleration.x * mobileMovementSpeed;
		}
		Vector3 theScale = playerGraphics.localScale;

		if (Input.GetKeyDown (KeyCode.D) || Input.GetKeyDown (KeyCode.RightArrow) 
			||	Input.acceleration.x > 0.1) {
			// Make sure the localScale is always positive to keep on looking right
			theScale.x = 1;
		} else if (Input.GetKeyDown (KeyCode.A) || Input.GetKeyDown (KeyCode.LeftArrow)
			|| Input.acceleration.x < -0.1) {
			// Make sure the localScale is always negative to keep on looking left
			theScale.x = -1;

		}
		playerGraphics.localScale = theScale;
	}

}
