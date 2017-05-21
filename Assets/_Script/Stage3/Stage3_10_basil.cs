using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage3_10_basil : MonoBehaviour {

	void OnCollisionEnter2D(Collision2D coll) {
		if (coll.gameObject.tag == "Player") {
			Stage3_Controller._Stage3_Quest[22] = true;
			Save_Script.Save_Quest_Info ();
		}
	}
}
