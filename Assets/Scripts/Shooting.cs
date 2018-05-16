using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour {

	public float fireRate = 0.5f;
	public int damage = 10;
	public LayerMask whatToHit;

	public GameObject bulletPrefab;

	private Transform firePoint;
	[SerializeField]
	private Transform bullets;

	private float timeToFire;
	private float bulletSpeed = 10f;

	// caching
	private AudioManager audioManager;

	void Awake()
	{
		firePoint = transform.Find ("FirePoint");
		if (firePoint == null)
		{
			Debug.LogError("FirePoint Not found in Player!");
		}
	}

	// Use this for initialization
	void Start () {
		audioManager = AudioManager.instance;
		if (bullets == null)
		{
			Debug.LogError ("Bullet container not found in Main Scene!");
		}
		if (audioManager == null)
		{
			Debug.LogError ("No AudioManager instance found in Main Scene");
		}
	}
	
	// Update is called once per frame
	void Update () {
		// Single fire only
		if (Input.GetButtonDown ("Fire1") && Time.time > timeToFire)
		{
			//Debug.Log (Time.time + " / " + timeToFire);
			timeToFire = Time.time + fireRate;
			Shoot ();
		}
	}
	IEnumerator AnimateShooting()
	{
		Animator animator = this.GetComponentInParent<Animator> ();
		animator.SetBool ("Shooting", true);
		yield return new WaitForSeconds (0.2f);
		animator.SetBool ("Shooting", false);
	}

	void Shoot()
	{
		GameObject bulletClone = Instantiate (bulletPrefab, firePoint.position, firePoint.rotation);
		bulletClone.transform.parent = bullets;

		Rigidbody2D _rb = bulletClone.GetComponent <Rigidbody2D> ();

		_rb.velocity = new Vector2 (0f, bulletSpeed);
		//_rb.AddForce (bulletSpeed * Vector3.up);
		StartCoroutine(AnimateShooting ());

		// If bullet already collided with an enemy, destroy after 2 seconds
		if (bulletClone != null) {
			Destroy (bulletClone, 2f);
		}
	}
}
