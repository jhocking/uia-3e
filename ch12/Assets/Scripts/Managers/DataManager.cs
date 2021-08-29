using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;

public class DataManager : MonoBehaviour, IGameManager {
	public ManagerStatus status {get; private set;}
	
	private string filename;
	
	private NetworkService network;
	
	public void Startup(NetworkService service) {
		Debug.Log("Data manager starting...");
		
		network = service;

		filename = Path.Combine(Application.persistentDataPath, "game.dat");

		// any long-running startup tasks go here, and set status to 'Initializing' until those tasks are complete
		status = ManagerStatus.Started;
	}

	public void SaveGameState() {
		Dictionary<string, object> gamestate = new Dictionary<string, object>();
		gamestate.Add("inventory", Managers.Inventory.GetData());
		gamestate.Add("health", Managers.Player.health);
		gamestate.Add("maxHealth", Managers.Player.maxHealth);
		gamestate.Add("curLevel", Managers.Mission.curLevel);
		gamestate.Add("maxLevel", Managers.Mission.maxLevel);

		using (FileStream stream = File.Create(filename)) {
			BinaryFormatter formatter = new BinaryFormatter();
			formatter.Serialize(stream, gamestate);
		}
	}

	public void LoadGameState() {
		if (!File.Exists(filename)) {
			Debug.Log("No saved game");
			return;
		}

		Dictionary<string, object> gamestate;

		using (FileStream stream = File.Open(filename, FileMode.Open)) {
			BinaryFormatter formatter = new BinaryFormatter();
			gamestate = formatter.Deserialize(stream) as Dictionary<string, object>;
		}

		Managers.Inventory.UpdateData((Dictionary<string, int>)gamestate["inventory"]);
		Managers.Player.UpdateData((int)gamestate["health"], (int)gamestate["maxHealth"]);
		Managers.Mission.UpdateData((int)gamestate["curLevel"], (int)gamestate["maxLevel"]);
		Managers.Mission.RestartCurrent();
	}
}
