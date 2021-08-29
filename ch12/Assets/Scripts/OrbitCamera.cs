using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// maintains position offset while orbiting around target

public class OrbitCamera : MonoBehaviour {
	[SerializeField] Transform target;

	public float rotSpeed = 1.5f;

	private float rotY;
	private Vector3 offset;

	// Use this for initialization
	void Start() {
		rotY = transform.eulerAngles.y;
		offset = target.position - transform.position;
	}

	// Update is called once per frame
	void LateUpdate() {
		rotY -= Input.GetAxis("Horizontal") * rotSpeed;
		Quaternion rotation = Quaternion.Euler(0, rotY, 0);
		transform.position = target.position - (rotation * offset);
		transform.LookAt(target);
	}
}
