using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class Stage4_Dogbed : MonoBehaviour{
	public GameObject player;
	public int _To_Scene;
	public SpriteRenderer _blackout;
	private Color bb;
	public bool enter_;
	public bool exit_ = false;

	void Awake(){
		player = GameObject.Find ("Player");
		bb = new Color (0f, 0f, 0f, 1f); //검정,불투명
		_blackout.color = bb;
	}

	void OnMouseDown(){
		print("Clicked");

		if (Stage4_Controller.q[4] && !Stage4_Controller.q4[0]) { //땅파기
			print("Dig");
			Stage4_Controller.q4 [0] = true;
		}

		if (Stage4_Controller.q4[0] && !Stage4_Controller.q4[1]) {
			print ("smell");
			Stage4_Controller.q4 [1] = true;
			Stage4_Controller.q [5] = true;
		}

		if (Stage4_Controller.q[8] && !Stage4_Controller.q[9]) {
			print ("Sleep");
			StartCoroutine ("FadeOut");
			Stage4_Controller.q[9] = true;
		}
	}

	IEnumerator FadeOut(){
		player.GetComponent<Moving_by_RLbuttons> ().enabled = false;
		for (float f = 0f; f < 1; f += Time.deltaTime) {
			Color c = _blackout.color;
			c.a = f;
			_blackout.color = c;
			yield return null;
		}
		player.GetComponent<Moving_by_RLbuttons> ().enabled = true;
	}
}
