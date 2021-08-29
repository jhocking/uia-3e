using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeviceOperator : MonoBehaviour {
	public float radius = 1.5f;

	void Update() {
		if (Input.GetKeyDown(KeyCode.C)) {
			Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius);
			foreach (Collider hitCollider in hitColliders) {
				Vector3 hitPosition = hitCollider.transform.position;

				// vertical correction so the direction won't point up or down
				hitPosition.y = transform.position.y;

				Vector3 direction = hitPosition - transform.position;
				if (Vector3.Dot(transform.forward, direction.normalized) > .5f) {
					hitCollider.SendMessage("Operate", SendMessageOptions.DontRequireReceiver);
				}
			}
		}
	}
}
