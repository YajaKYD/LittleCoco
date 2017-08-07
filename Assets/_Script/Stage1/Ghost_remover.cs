using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost_remover : MonoBehaviour {

	public bool remover = false;
	private GameObject player;

	public AnimateSprite idle1;
	public AnimateSprite re1;

	// Use this for initialization
	void Start () {
		player = GameObject.Find ("Player");

	}
	
	// Update is called once per frame
	void Update () {
		if (remover) {
			StartCoroutine (GhostGG ());
			remover = false;
		}
	}

	IEnumerator GhostGG(){
		idle1.enabled = false;
		//idle2.enabled = false;
		re1.enabled = true;
		//re2.enabled = true;
		yield return new WaitForSeconds(3.5f);
//		xx [0].SetActive (false);
//		xx [1].SetActive (false);
		this.gameObject.SetActive(false);
		player.GetComponent<Moving_by_RLbuttons> ().enabled = true;

	}
}
