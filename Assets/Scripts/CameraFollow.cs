using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

	public Transform target;
	public float smoothSpeed = .3f;

//	private Vector3 currVelocity;
	// LateUpdate() is used so that the camera follows the player movement, instead of trying to match it 
	// live-time. This allows smoother camera movement (no jitter)
	void LateUpdate () {
		if (target.position.y > transform.position.y)
		{
			Vector3 newPos = new Vector3 (transform.position.x, target.position.y, transform.position.z);
//			transform.position = Vector3.SmoothDamp (transform.position, newPos, ref currVelocity, smoothSpeed * Time.deltaTime);
			transform.position = newPos;
		}
	}
}
