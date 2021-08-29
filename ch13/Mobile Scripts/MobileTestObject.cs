using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobileTestObject : MonoBehaviour {
	private string message;
	
	void Awake() {
		TestPlugin.Initialize();
	}
	
	// Use this for initialization
	void Start() {
		message = "START: " + TestPlugin.TestString("ThIs Is A tEsT");
	}
	
	// Update is called once per frame
	void Update() {
		
		// Make sure the user touched the screen
		if (Input.touchCount==0){return;}
		
		Touch touch = Input.GetTouch(0);
		if (touch.phase == TouchPhase.Began) {
			message = "TOUCH: " + TestPlugin.TestNumber();
		}
	}
	
	void OnGUI() {
		GUI.Label(new Rect(10, 10, 200, 20), message);
	}
}
