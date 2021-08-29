using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(WeatherManager))]
[RequireComponent(typeof(ImagesManager))]

public class Managers : MonoBehaviour {
	public static WeatherManager Weather {get; private set;}
	public static ImagesManager Images {get; private set;}

	private List<IGameManager> startSequence;
	
	void Awake() {
		Weather = GetComponent<WeatherManager>();
		Images = GetComponent<ImagesManager>();

		startSequence = new List<IGameManager>();
		startSequence.Add(Weather);
		startSequence.Add(Images);

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
