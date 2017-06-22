using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage4_5_Neogulman : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(){
		if (!Stage4_Controller.q [17]) {
			//conversation
			this.gameObject.SetActive(false); // add animation
			Stage4_Controller.q[17] = true;
		}
	}
}
