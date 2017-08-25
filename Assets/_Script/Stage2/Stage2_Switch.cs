using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage2_Switch : MonoBehaviour {

	public GameObject _Switch;
	public GameObject _LightFromSide;

	void OnTriggerEnter2D(Collider2D other){

		if (other.CompareTag ("Player")) {

			if (!Stage2_Controller.q [16]) {
				_Switch.SetActive (true);
				_LightFromSide.SetActive (true);
			}

			Stage2_Controller.q[16] = true;

			if (Stage2_Controller.q[17] && !Stage2_Controller.q[22]) {

				_Switch.SetActive (true);
				_LightFromSide.SetActive (false);

				if (Stage2_Controller.q [18]) {
					Item_Controller aa = GameObject.FindWithTag ("Item_Canvas").GetComponent<Item_Controller> ();
					for (int i = 0; i < 5; i++) {
						if (aa._item_name_list [i] == "Clockwork") {
							aa._consumable [i] = true;
							break;
						}
					}
				}
				Stage2_Controller.q[22] = true;
			}
		}
		
	}
}
