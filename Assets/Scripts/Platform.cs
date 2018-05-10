using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour {

	public float jumpForce;

	void Awake()
	{
		if (this.tag == "Platform") {
			jumpForce = 11f;
		} else if (this.tag == "BluePlatform") {
			jumpForce = 15f;
		} else {
			jumpForce = 0f;
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
			// Make WhitePlatform disappear and disable collision as soon as it bumps

			if (this.name.Contains ("BluePlatform"))
			{
				Debug.Log ("BLUE PLATFORM");
			}
			else if (this.name.Contains ("Platform"))
			{
				Debug.Log ("PLATFORM");
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
		}
	}

	private IEnumerator DisappearingAnimation()
	{
		Animator animator = transform.GetComponent <Animator>();
		animator.enabled = true;
		yield return new WaitForSeconds (1f);
		Destroy (this.gameObject);
	}

}
