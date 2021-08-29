using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveTrigger : MonoBehaviour {
	void OnTriggerEnter(Collider other) {
		Managers.Mission.ReachObjective();
	}
}