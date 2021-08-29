using System.Runtime.InteropServices;
using UnityEngine;

public class WebTestObject : MonoBehaviour {
	private string message;

	[DllImport("__Internal")]
	private static extern void ShowAlert(string msg);

	void Start() {
		message = "No message yet";
	}

	void Update() {
		if (Input.GetMouseButtonDown(0)) {
			ShowAlert("Hello out there!");
		}
	}

	void OnGUI() {
		GUI.Label(new Rect(10, 10, 200, 20), message);
	}

	public void RespondToBrowser(string message) {
		this.message = message;
	}
}
