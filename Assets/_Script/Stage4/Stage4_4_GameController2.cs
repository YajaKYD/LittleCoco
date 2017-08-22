using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class Stage4_4_GameController2 : MonoBehaviour {

	private GameObject player;
	private GameObject item_Canvas;
	private Text_Importer2 ti;
	public GameObject poster, posterPrefab;
	private Stage4_4_poster posterScript;

	public Transform startPos;
	public SpriteRenderer blackout;

	void Start () {
		ti = GameObject.FindWithTag("Dialogue").GetComponent<Text_Importer2>();
		ti.Import (44);

		player = GameObject.FindWithTag ("Player");
		player.transform.position = startPos.position;
		item_Canvas = GameObject.FindWithTag ("Item_Canvas");
	
		ti.Talk ();

		Save_Script.Save_Now_Point();
	}

	void Update () {
		if (Stage4_Controller.q [24] && !Stage4_Controller.q [25]) {
			if (poster == null) {
				poster = Instantiate (posterPrefab, Vector3.forward, Quaternion.identity) as GameObject;
				ti.Talk (ti.lineNo + 2);
			}
		} else if (Stage4_Controller.q [26] && !Stage4_Controller.q [27]) {
			posterScript = GameObject.Find ("photo_neogulman").GetComponent<Stage4_4_poster> ();
			posterScript.DisappearPoster ();
			Stage4_Controller.q [27] = true;
		} else if (Stage4_Controller.q [28]) {
			StartCoroutine ("FinishPosterPuzzle");
		}
	}

	IEnumerator FinishPosterPuzzle(){
		Debug.Log ("finish 4_4");
		player.GetComponent<Moving_by_RLbuttons> ().enabled = false;
		for (float f = 0f; f < 1; f += Time.deltaTime) {
			Color c = blackout.color;
			c.a = f;
			blackout.color = c;
			yield return null;
		}
		player.GetComponent<Moving_by_RLbuttons> ().enabled = true;
		SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex + 1);
		PlayerPrefs.SetInt ("SceneFromWhere", SceneManager.GetActiveScene ().buildIndex);
	}
}
