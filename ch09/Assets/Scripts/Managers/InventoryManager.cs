using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour, IGameManager {
	public ManagerStatus status {get; private set;}

	private Dictionary<string, int> items;
	public string equippedItem {get; private set;}

	public void Startup() {
		Debug.Log("Inventory manager starting...");

		items = new Dictionary<string, int>();

		// any long-running startup tasks go here, and set status to 'Initializing' until those tasks are complete
		status = ManagerStatus.Started;
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
