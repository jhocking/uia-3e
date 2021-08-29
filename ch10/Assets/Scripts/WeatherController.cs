using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherController : MonoBehaviour {
	[SerializeField] Material sky;
	[SerializeField] Light sun;
	
	private float fullIntensity;
	
	void OnEnable() {
		Messenger.AddListener(GameEvent.WEATHER_UPDATED, OnWeatherUpdated);
	}
	void OnDisable() {
		Messenger.RemoveListener(GameEvent.WEATHER_UPDATED, OnWeatherUpdated);
	}
	
	// Use this for initialization
	void Start() {
		fullIntensity = sun.intensity;
	}
	
	private void OnWeatherUpdated() {
		SetOvercast(Managers.Weather.cloudValue);
	}
	
	private void SetOvercast(float value) {
		sky.SetFloat("_Blend", value);
		sun.intensity = fullIntensity - (fullIntensity * value);
	}
}
