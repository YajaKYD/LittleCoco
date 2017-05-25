using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage4_Dogbed : MonoBehaviour{


	void OnMouseDown(){
		print("Clicked");


		if (Stage4_Controller._stage4_q5 && !Stage4_Controller._stage4_q6) {
			print ("smell");
			Stage4_Controller._stage4_q6 = true;
			Save_Script.Save_Quest_Info ();
		}
		if (Stage4_Controller._stage4_q4 && !Stage4_Controller._stage4_q5) {
			//땅파기
			print("Dig");
			Stage4_Controller._stage4_q5 = true;
			Save_Script.Save_Quest_Info ();
		}
		if (Stage4_Controller._stage4_q9 && !Stage4_Controller._stage4_q10) {
			print ("Sleep");
			Stage4_Controller._stage4_q10 = true;
			Save_Script.Save_Quest_Info ();
		}
	}

}
