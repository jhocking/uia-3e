using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIController : MonoBehaviour {
	[SerializeField] TMP_Text scoreLabel;
	[SerializeField] SettingsPopup settingsPopup;

	private int score;

	void OnEnable() {
		Messenger.AddListener(GameEvent.ENEMY_HIT, OnEnemyHit);
	}
	void OnDisable() {
		Messenger.RemoveListener(GameEvent.ENEMY_HIT, OnEnemyHit);
	}

	void Start() {
		score = 0;
		scoreLabel.text = score.ToString();

		settingsPopup.Close();
	}

	private void OnEnemyHit() {
		score += 1;
		scoreLabel.text = score.ToString();
	}

	public void OnOpenSettings() {
		settingsPopup.Open();
	}

	public void OnPointerDown() {
		Debug.Log("pointer down");
	}
}
