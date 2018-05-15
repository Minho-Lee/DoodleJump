using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour {

	public float fireRate = 0.5f;
	public int damage = 10;
	public LayerMask whatToHit;

	public GameObject bulletPrefab;

	public Transform firePoint;

	private float timeToFire;
	private float bulletSpeed = 8f;

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
			Debug.Log (Time.time + " / " + timeToFire);
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
		// TODO: Deal with collision later
		CircleCollider2D _cc = bulletClone.GetComponent<CircleCollider2D> ();
		Rigidbody2D _rb = bulletClone.GetComponent <Rigidbody2D> ();

		_rb.velocity = new Vector2 (0f, bulletSpeed);
		StartCoroutine(AnimateShooting ());

		Destroy (bulletClone, 2f);
	}
}
