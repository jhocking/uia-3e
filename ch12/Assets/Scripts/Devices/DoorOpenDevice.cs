using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpenDevice : BaseDevice {
	[SerializeField] Vector3 dPos;

	private bool open;

	public override void Operate() {
		if (open) {
			Vector3 pos = transform.position - dPos;
			transform.position = pos;
		} else {
			Vector3 pos = transform.position + dPos;
			transform.position = pos;
		}
		open = !open;
	}

	public void Activate() {
		if (!open) {
			Vector3 pos = transform.position + dPos;
			transform.position = pos;
			open = true;
		}
	}
	public void Deactivate() {
		if (open) {
			Vector3 pos = transform.position - dPos;
			transform.position = pos;
			open = false;
		}
	}
}
