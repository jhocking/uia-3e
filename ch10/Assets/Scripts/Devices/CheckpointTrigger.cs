using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointTrigger : MonoBehaviour {
	public string identifier;

	private bool triggered;

	void OnTriggerEnter(Collider other) {
		if (triggered) {return;}

		Managers.Weather.LogWeather(identifier);
		triggered = true;
	}
}
