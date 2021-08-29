using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIController : MonoBehaviour {
	[SerializeField] TMP_Text healthLabel;
	[SerializeField] TMP_Text levelEnding;
	[SerializeField] InventoryPopup popup;

	void OnEnable() {
		Messenger.AddListener(GameEvent.HEALTH_UPDATED, OnHealthUpdated);
		Messenger.AddListener(GameEvent.LEVEL_FAILED, OnLevelFailed);
		Messenger.AddListener(GameEvent.LEVEL_COMPLETE, OnLevelComplete);
		Messenger.AddListener(GameEvent.GAME_COMPLETE, OnGameComplete);
	}
	void OnDisable() {
		Messenger.RemoveListener(GameEvent.HEALTH_UPDATED, OnHealthUpdated);
		Messenger.RemoveListener(GameEvent.LEVEL_FAILED, OnLevelFailed);
		Messenger.RemoveListener(GameEvent.LEVEL_COMPLETE, OnLevelComplete);
		Messenger.RemoveListener(GameEvent.GAME_COMPLETE, OnGameComplete);
	}

	void Start() {
		OnHealthUpdated();

		levelEnding.gameObject.SetActive(false);
		popup.gameObject.SetActive(false);
	}

	void Update() {
		if (Input.GetKeyDown(KeyCode.M)) {
			bool isShowing = popup.gameObject.activeSelf;
			popup.gameObject.SetActive(!isShowing);
			popup.Refresh();
		}
	}

	private void OnHealthUpdated() {
		string message = $"Health: {Managers.Player.health}/{Managers.Player.maxHealth}";
		healthLabel.text = message;
	}

	private void OnLevelFailed() {
		StartCoroutine(FailLevel());
	}
	private IEnumerator FailLevel() {
		levelEnding.gameObject.SetActive(true);
		levelEnding.text = "Level Failed";
		
		yield return new WaitForSeconds(2);

		Managers.Player.Respawn();
		Managers.Mission.RestartCurrent();
	}

	private void OnLevelComplete() {
		StartCoroutine(CompleteLevel());
	}
	private IEnumerator CompleteLevel() {
		levelEnding.gameObject.SetActive(true);
		levelEnding.text = "Level Complete!";

		yield return new WaitForSeconds(2);

		Managers.Mission.GoToNext();
	}

	private void OnGameComplete() {
		levelEnding.gameObject.SetActive(true);
		levelEnding.text = "You Finished the Game!";
	}

	public void SaveGame() {
		Managers.Data.SaveGameState();
	}

	public void LoadGame() {
		Managers.Data.LoadGameState();
	}
}
