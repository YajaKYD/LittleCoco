using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class onlydontdestroy : MonoBehaviour {

	// Use this for initialization
	void Start () {
		DontDestroyOnLoad (transform.gameObject);
		GetComponent<Text_Importer> ().Import (15);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
