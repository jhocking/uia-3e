using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseDevice : MonoBehaviour {
	public float radius = 3.5f;

	void OnMouseUp() {
		Transform player = GameObject.FindWithTag("Player").transform;
		Vector3 playerPosition = player.position;

		// vertical correction so the direction won't point up or down
		playerPosition.y = transform.position.y;

		if (Vector3.Distance(transform.position, playerPosition) < radius) {
			Vector3 direction = transform.position - playerPosition;
			if (Vector3.Dot(player.forward, direction.normalized) > .5f) {
				Operate();
			}
		}
	}

	public virtual void Operate() {
		// behavior of the specific device
	}
}
