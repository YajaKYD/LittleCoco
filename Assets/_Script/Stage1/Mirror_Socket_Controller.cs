using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mirror_Socket_Controller : MonoBehaviour {

	public bool mirror_in_ornot = false;
	public Sprite mirror;
	public Sprite socket;

//	private GameObject player;
	private Item_Controller i_c;
	private Outline o_l;

	void Awake(){
//		player = GameObject.Find ("Player");
		i_c = GameObject.Find ("Item_Canvas").GetComponent<Item_Controller>();
		o_l = GetComponent<Outline> ();
	}

	void Update(){
		if (o_l.used_or_not_for_retry) { //없는 상태 -> 거울(놓기)
			GetComponent<SpriteRenderer> ().sprite = mirror;
			mirror_in_ornot = true;
			o_l.eraseRenderer = true;
		}

	}

	void OnMouseDown(){
		if (mirror_in_ornot && i_c.cant_pick_during_using) { //거울이 들어있는 상태 -> 없는 상태(거울 얻기)
			GetComponent<SpriteRenderer> ().sprite = socket;
			i_c.Get_Item_Auto (3, mirror);
			//GameObject prefab = (GameObject)Instantiate(Resources.Load("Prefabs/거울"));
			//prefab.transform.position = player.transform.position + Vector3.up;
			mirror_in_ornot = false;
			o_l.used_or_not_for_retry = false;
		}
	}

}
