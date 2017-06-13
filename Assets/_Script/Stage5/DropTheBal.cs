using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DropTheBal : MonoBehaviour {

	private Item_Controller ic;
	public Sprite starImg;

	void Awake(){
		ic = GameObject.FindWithTag ("Item_Canvas").GetComponent<Item_Controller> ();
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.CompareTag ("Player") && Stage5_Controller._Stage5_Quest [18] && !Stage5_Controller._Stage5_Quest[19]) {
			//GameObject.Find ("Player").GetComponent<Moving_by_RLbuttons> ().enabled = false;
			GameObject ball = (GameObject)Instantiate (Resources.Load ("Prefabs/Ball"));
			Destroy (ball.GetComponent<Ball_popup> ());
			Destroy (ball.GetComponent<Player_get_Item> ());
			Destroy (ball.GetComponent<Destroy_tutorialMessage> ());
			//Destroy (ball.GetComponent<Rigidbody2D> ());
			//Destroy (ball.GetComponent<PolygonCollider2D> ());
			Physics2D.IgnoreCollision (GameObject.Find("Player").GetComponent<Collider2D> (), ball.GetComponent<Collider2D> (), true);
			ball.transform.position = this.transform.position;
			Auto_ItemUse ();
			Stage5_Controller._Stage5_Quest [19] = true;
		}
	}

	void Auto_ItemUse(){
		print ("DSF");
		for (int i = 0; i < ic._item_list.Length; i++) {
			if (ic._item_name_list [i] == "Ball") {
				ic._item_name_list [i] = "";
				ic._usable_item [i] = false;
				ic._interaction_object [i] = "";
				ic._the_number_of_items [i] = 0;
				ic._item_list [i].GetComponent<Image> ().color = new Color (1, 1, 1, 0);
				ic._item_list [i].transform.parent.GetComponentInChildren<Text> ().color = new Color (1, 1, 1, 0);
				ic._explanations [i] = "";
				break;
			}
		}
	}

	void OnMouseDown(){
		if (Stage5_Controller._Stage5_Quest [19] && !Stage5_Controller._Stage5_Quest[20]) {
			ic.Get_Item_Auto (5, starImg);
			Stage5_Controller._Stage5_Quest [20] = true;
		}
	}
}
