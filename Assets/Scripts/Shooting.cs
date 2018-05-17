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

	private bool shootingActive = true;

	private Animator animator;

	// caching
	private AudioManager audioManager;

	void Awake()
	{
		firePoint = transform.Find ("FirePoint");
		if (firePoint == null)
		{
			Debug.LogError("FirePoint Not found in Player!");
		}

		GameMaster.gm.onTogglePauseMenu += OnPauseMenuToggle;
	}

	// Use this for initialization
	void Start () {
		audioManager = AudioManager.instance;
		animator = this.GetComponentInParent<Animator> ();
		if (bullets == null)
		{
			Debug.LogError ("Bullet container not found in Main Scene!");
		}
		if (audioManager == null)
		{
			Debug.LogError ("No AudioManager instance found in Main Scene");
		}
		if (animator == null)
		{
			Debug.LogError ("No Animator instance in Doodler!");
		}
	}
	
	// Update is called once per frame
	void Update () {
		// Single fire only
		if (Input.GetButtonDown ("Fire1") && Time.time > timeToFire)
		{
			//Debug.Log (Time.time + " / " + timeToFire);
			timeToFire = Time.time + fireRate;
			if (shootingActive)
				Shoot ();
		}
	}
	void Shoot()
	{
		GameObject bulletClone = Instantiate (bulletPrefab, firePoint.position, firePoint.rotation);
		bulletClone.transform.parent = bullets;

		Rigidbody2D _rb = bulletClone.GetComponent <Rigidbody2D> ();

		_rb.velocity = new Vector2 (0f, bulletSpeed);

		StartCoroutine(AnimateShooting ());

		// If bullet already collided with an enemy, destroy after 2 seconds
		if (bulletClone != null) {
			Destroy (bulletClone, 2.5f);
		}
	}
	IEnumerator AnimateShooting()
	{
		animator.SetBool ("Shooting", true);
		yield return new WaitForSeconds (0.2f);
		animator.SetBool ("Shooting", false);
	}

	void OnPauseMenuToggle(bool _active)
	{
		//animator.enabled = !_active;
		shootingActive = !_active;
	}

	void OnDestroy()
	{
		GameMaster.gm.onTogglePauseMenu -= OnPauseMenuToggle;
	}
}
