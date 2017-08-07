using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoogleTest : MonoBehaviour {

	public GoogleAnalyticsV4 googleAnalytics;
	public static GoogleTest instance = null;

	void Awake()
	{
		Instantiate(googleAnalytics);
		googleAnalytics.StartSession();

		if (instance == null) {
			instance = this;
		} else if (instance != this) {
			Destroy (gameObject);
		}
	}

	void OnApplicationQuit () {
		googleAnalytics.StopSession();
	}
}
