using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour, IGameManager {
	public ManagerStatus status {get; private set;}

	private Dictionary<string, int> items;
	public string equippedItem {get; private set;}

	private NetworkService network;

	public void Startup(NetworkService service) {
		Debug.Log("Inventory manager starting...");

		network = service;

		// start empty
		UpdateData(new Dictionary<string, int>());

		// any long-running startup tasks go here, and set status to 'Initializing' until those tasks are complete
		status = ManagerStatus.Started;
	}

	public void UpdateData(Dictionary<string, int> items) {
		this.items = items;
	}

	public Dictionary<string, int> GetData() {
		return items;
	}

	private void DisplayItems() {
		string itemDisplay = "Items: ";
		foreach (KeyValuePair<string, int> item in items) {
			itemDisplay += item.Key + "(" + item.Value + ") ";
		}
		Debug.Log(itemDisplay);
	}

	public void AddItem(string name) {
		if (items.ContainsKey(name)) {
			items[name] += 1;
		} else {
			items[name] = 1;
		}

		DisplayItems();
	}
	
	public bool ConsumeItem(string name) {
		if (items.ContainsKey(name)) {
			items[name]--;
			if (items[name] == 0) {
				items.Remove(name);
			}
		} else {
			Debug.Log($"Cannot consume {name}");
			return false;
		}
		
		DisplayItems();
		return true;
	}

	public List<string> GetItemList() {
		List<string> list = new List<string>(items.Keys);
		return list;
	}

	public int GetItemCount(string name) {
		if (items.ContainsKey(name)) {
			return items[name];
		}
		return 0;
	}

	public bool EquipItem(string name) {
		if (items.ContainsKey(name) && equippedItem != name) {
			equippedItem = name;
			Debug.Log($"Equipped {name}");
			return true;
		}

		equippedItem = null;
		Debug.Log("Unequipped");
		return false;
	}
}
