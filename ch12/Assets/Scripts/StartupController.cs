using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartupController : MonoBehaviour {
	[SerializeField] Slider progressBar;

	void OnEnable() {
		Messenger<int, int>.AddListener(StartupEvent.MANAGERS_PROGRESS, OnManagersProgress);
		Messenger.AddListener(StartupEvent.MANAGERS_STARTED, OnManagersStarted);
	}
	void OnDisable() {
		Messenger<int, int>.RemoveListener(StartupEvent.MANAGERS_PROGRESS, OnManagersProgress);
		Messenger.RemoveListener(StartupEvent.MANAGERS_STARTED, OnManagersStarted);
	}

	private void OnManagersProgress(int numReady, int numModules) {
		float progress = (float)numReady / numModules;
		progressBar.value = progress;
	}

	private void OnManagersStarted() {
		Managers.Mission.GoToNext();
	}
}
