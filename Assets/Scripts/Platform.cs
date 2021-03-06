﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour {

	public float jumpForce;

	void Awake()
	{
		if (this.tag == "Platform" || this.tag == "WhitePlatform") {
			jumpForce = 11f;
		} else if (this.tag == "BluePlatform") {
			jumpForce = 15f;
		} else {
			// RedPlatform will just break;
		}		
	}

	// caching
	private AudioManager audioManager;

	string platformSound = "Platform";
	string bluePlatformSound = "BluePlatform";
	string redPlatformSound = "RedPlatform";

	void Start()
	{
		audioManager = AudioManager.instance;
		if (audioManager == null) {
			Debug.LogError ("No AudioManager instance found in GM");
		}
	}

	void OnCollisionEnter2D(Collision2D collision) 
	{
		// Check if the Doodler is coming from below
		if (collision.relativeVelocity.y <= 0f) 
		{
			Rigidbody2D rb = collision.collider.GetComponent <Rigidbody2D>();
			//Debug.Log (collision.collider.name);
			if (rb != null)
			{
				Vector2 velocity = rb.velocity;
				velocity.y = jumpForce;
				rb.velocity = velocity;
			}	
			// Make WhitePlatform disappear

			if (this.name.Contains ("BluePlatform"))
			{
				Debug.Log ("BLUE PLATFORM");
				audioManager.PlaySound (bluePlatformSound);
			}
			else if (this.name.Contains ("WhitePlatform"))
			{
				Debug.Log ("WHITE PLATFORM");
				audioManager.PlaySound (platformSound);
				// Make whitePlatform no longer bounceable while animation is active
				this.GetComponent <EdgeCollider2D> ().enabled = false;
				// Queue disappering animation
				this.GetComponent <Animator>().enabled = true;
				Destroy (this, 0.5f);
			} else {
				Debug.Log ("PLATFORM");
				audioManager.PlaySound (platformSound);
			}

		} 
	}

	void OnTriggerEnter2D(Collider2D collider)
	{
		Rigidbody2D rb = collider.GetComponent <Rigidbody2D>();
		//Debug.Log (rb.velocity.y);
		if (rb.velocity.y <= 0)
		{
			//this.GetComponent <EdgeCollider2D>().enabled = false;
			//Debug.Log ("WHITE PLATFORM");
			StartCoroutine ("DisappearingAnimation");
			audioManager.PlaySound (redPlatformSound);
		}
	}

	IEnumerator DisappearingAnimation()
	{
		// For RedPlatform which the Doodler just passes through
		Animator animator = transform.GetComponent <Animator>();
		animator.enabled = true;
		yield return new WaitForSeconds (1f);
		Destroy (this.gameObject);
	}

}
