using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour {

	Transform playerGraphics;

	public float movementSpeed = 10f;
	public static float movement = 0f;

	private bool m_facingRight = true;

	void Awake ()
	{
		playerGraphics = GameObject.FindGameObjectWithTag ("Player").GetComponent <Transform> ();
		if (playerGraphics == null)
		{
			Debug.LogError ("No PlayerGraphics referenced in PlayerControl!");
			return;
		}
	}

	void Update()
	{
		movement = Input.GetAxis ("Horizontal") * movementSpeed;
		Vector3 theScale = playerGraphics.localScale;

		if (Input.GetKeyDown (KeyCode.D) || Input.GetKeyDown (KeyCode.RightArrow)) 
		{
			// Make sure the localScale is always positive to keep on looking right
			theScale.x = 1;
		}
		else if (Input.GetKeyDown (KeyCode.A) || Input.GetKeyDown (KeyCode.LeftArrow))
		{
			// Make sure the localScale is always negative to keep on looking left
			theScale.x = -1;

		}
		playerGraphics.localScale = theScale;
	}

}
