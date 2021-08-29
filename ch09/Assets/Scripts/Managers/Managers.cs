using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerManager))]
[RequireComponent(typeof(InventoryManager))]

public class Managers : MonoBehaviour {
	public static PlayerManager Player {get; private set;}
	public static InventoryManager Inventory {get; private set;}

	private List<IGameManager> startSequence;
	
	void Awake() {
		Player = GetComponent<PlayerManager>();
		Inventory = GetComponent<InventoryManager>();

		startSequence = new List<IGameManager>();
		startSequence.Add(Player);
		startSequence.Add(Inventory);

		StartCoroutine(StartupManagers());
	}

	private IEnumerator StartupManagers() {
		foreach (IGameManager manager in startSequence) {
			manager.Startup();
		}

		yield return null;

		int numModules = startSequence.Count;
		int numReady = 0;

		while (numReady < numModules) {
			int lastReady = numReady;
			numReady = 0;

			foreach (IGameManager manager in startSequence) {
				if (manager.status == ManagerStatus.Started) {
					numReady++;
				}
			}

			if (numReady > lastReady)
				Debug.Log($"Progress: {numReady}/{numModules}");
			
			yield return null;
		}
		
		Debug.Log("All managers started up");
	}
}
