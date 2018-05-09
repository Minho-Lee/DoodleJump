using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(SpriteRenderer))]
public class Player : MonoBehaviour {

	public float movementSpeed = 10f;

	[SerializeField]
	private Camera camera;

	Rigidbody2D rb;
	SpriteRenderer sr;

	float movement = 0f;

	private float leftConstraint;
	private float rightConstraint;
	float spriteSizeX;
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
	}
	
	// Update is called once per frame
	void Update () {
		movement = Input.GetAxis ("Horizontal") * movementSpeed;

		if (transform.position.x + Mathf.Abs (spriteSizeX / 2) < leftConstraint)
		{
			transform.position = new Vector3 (rightConstraint, transform.position.y, transform.position.z);
			//Debug.Log ("Passed Left Side!");
		}
		if (transform.position.x - (spriteSizeX / 2) > rightConstraint)
		{
			transform.position = new Vector3 (leftConstraint, transform.position.y, transform.position.z);
			//Debug.Log ("Passed Right Side!");
		}
	}

	void FixedUpdate() 
	{
		Vector2 velocity = rb.velocity;
		velocity.x = movement;
		rb.velocity = velocity;
	}
}
