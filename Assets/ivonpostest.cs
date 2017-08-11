using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ivonpostest : MonoBehaviour {

	private Text_Importer2 ti;

	// Use this for initialization
	void Start () {
		ti = GameObject.FindWithTag ("Dialogue").GetComponent<Text_Importer2> ();
			
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(){
		ti.Talk ();
	}
}
