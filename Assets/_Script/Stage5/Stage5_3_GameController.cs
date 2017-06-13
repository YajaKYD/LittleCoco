using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage5_3_GameController : MonoBehaviour {

	void Awake(){
		
	}

	void Start(){
		if (Stage5_Controller._Stage5_Quest [24] && !Stage5_Controller._Stage5_Quest [25]) {
			Save_Script.Save_Now_Point ();
			//시작할 때 저장
		}
	}
}
