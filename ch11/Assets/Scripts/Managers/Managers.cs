using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioManager))]

public class Managers : MonoBehaviour {
	public static AudioManager Audio {get; private set;}

	private List<IGameManager> startSequence;
	
	void Awake() {
		Audio = GetComponent<AudioManager>();

		startSequence = new List<IGameManager>();
		startSequence.Add(Audio);

		StartCoroutine(StartupManagers());
	}

	private IEnumerator StartupManagers() {
		NetworkService network = new NetworkService();
		
		foreach (IGameManager manager in startSequence) {
			manager.Startup(network);
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
