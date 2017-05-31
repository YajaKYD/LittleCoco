using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage4_Dogbed : MonoBehaviour{


	void OnMouseDown(){
		print("Clicked");

		if (Stage4_Controller.q4 && !Stage4_Controller.q5) { //땅파기
			print("Dig");
			Stage4_Controller.q5 = true;
		}

		if (Stage4_Controller.q5 && !Stage4_Controller.q6) {
			print ("smell");
			Stage4_Controller.q6 = true;
		}

		if (Stage4_Controller.q9 && !Stage4_Controller.q10) {
			print ("Sleep");
			Stage4_Controller.q10 = true;
		}
	}

}
