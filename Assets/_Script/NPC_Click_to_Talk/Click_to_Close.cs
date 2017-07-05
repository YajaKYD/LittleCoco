using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Click_to_Close : MonoBehaviour {

	//private Moving_by_RLbuttons player_moving;
	private BoxCollider2D npc_collider;
	private Text_Importer _ti;
	public bool aa = true;
	private Moving_by_RLbuttons mbr;

	void Awake(){
		//player_moving = GameObject.Find ("Player").GetComponent<Moving_by_RLbuttons> ();
		if (GameObject.Find (name.Remove (name.Length - 5, 5))) {
			npc_collider = GameObject.Find (name.Remove (name.Length - 5, 5)).GetComponent<BoxCollider2D> ();
		}
		_ti = GetComponentInParent<Text_Importer> ();
		mbr = GameObject.FindWithTag ("Player").GetComponent<Moving_by_RLbuttons> ();
	}

	void Update(){
		mbr.enabled = false;
	}

	void OnMouseDown(){
		aa = _ti.NPC_Say_yeah (name.Remove (name.Length - 5, 5));

		if (!aa) {
			//
			transform.parent.gameObject.SetActive (false);
			//
			this.gameObject.SetActive (false);
			if (npc_collider) {
				npc_collider.enabled = true;
			}
			//player_moving.enabled = true;

			if (name.Remove (name.Length - 5, 5) == "코코") {
				for(int i = 0 ; i < _ti._coco_dialogue.Length ; i++){
					_ti._coco_dialogue [i].SetActive (false);
				}
			}
		}
	}
}
