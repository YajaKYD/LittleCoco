using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class Player_get_Item : MonoBehaviour {

	public TextAsset csv_Item_list;
	public int _item_numbering;
	public string _interaction;
	public string _explanation;

	private bool usable;
	private Item_Controller aa;
	private char lineSeperator = '\n';
	private char fieldSeperator = ',';
	private bool consumable;


	void Awake(){
		aa = GameObject.Find ("Item_Canvas").GetComponent<Item_Controller> ();
		readData ();
	}

	void OnCollisionEnter2D (Collision2D other){

		if (other.gameObject.CompareTag("Player") && aa.cant_pick_during_using){//줍는게 가능할 때만
			aa.Get_Item (this.gameObject,this.name, this.GetComponent<SpriteRenderer> ().sprite, usable, _interaction, consumable, _explanation);
		}
	}


	private void readData(){
		string[] records = csv_Item_list.text.Split (lineSeperator);
		string[] fields = records[_item_numbering].Split(fieldSeperator);

		if (fields [2] == "0") { //사용불가능
			usable = false;
		} else { //사용가능
			usable = true;
			_interaction = fields[3]; //사용대상
			if (fields [4] == "1") {//소비재
				consumable = true;
			} else {//안 소비재
				consumable = false;
			}
		}

		_explanation = fields [5];
	}

	void OnMouseDown(){
		GameObject.Find ("Player").GetComponent<Moving_by_RLbuttons> ().Click_item_to_get ((Vector2)this.transform.position);
	}

}
