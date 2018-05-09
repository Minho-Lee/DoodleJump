using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour {

	public float jumpForce = 11f;

	void OnCollisionEnter2D(Collision2D collision) 
	{
		// Check if the Doodler is coming from below
		if (collision.relativeVelocity.y <= 0f) 
		{
			Rigidbody2D rb = collision.collider.GetComponent <Rigidbody2D>();
			Debug.Log (collision.collider.name);
			Debug.Log (rb);
			if (rb != null)
			{
				Vector2 velocity = rb.velocity;
				velocity.y = jumpForce;
				rb.velocity = velocity;
			}	
		}

	}

}
