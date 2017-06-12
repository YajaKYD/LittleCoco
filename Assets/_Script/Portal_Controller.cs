using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement; 
using System.IO;

public class Portal_Controller : MonoBehaviour {
	public GameObject _Player;
	public int _To_Scene;
	public SpriteRenderer _blackout;
	private Color bb;
	public bool enter_;
	public bool exit_ = false;

	void Awake(){
		_Player = GameObject.Find ("Player");
		bb = new Color (0f, 0f, 0f, 1f); //검정,불투명
		_blackout.color = bb;
		_Player.GetComponent<Moving_by_RLbuttons> ().enabled = false;
		StartCoroutine ("FadeIn");
	}

	void OnTriggerEnter2D(Collider2D other){ //collider가 있어야함. 트리거로 해놓자.
		
		if (other.CompareTag("Player")) {
			//exit_ = true;
			StartCoroutine("FadeOut");
			_Player.GetComponent<Moving_by_RLbuttons> ().enabled = false;
			exit_ = true; //해당 씬이 끝남
		}
	}

//	public void Save(){
//		/*
//		BinaryFormatter bf = new BinaryFormatter ();
//		FileStream file = File.Create (Application.persistentDataPath + "/Spectrum" + SceneManager.GetActiveScene ().buildIndex.ToString () + ".ysp");
//
//		SavingData data = new SavingData ();
//		data.num_TO_scene = _To_Scene;
//		//if (_d_c.current_line != null) {
//		//	data.dialogue_line = _d_c.current_line;
//		//}
//
//		bf.Serialize (file,data);
//		file.Close ();
//		*/
//
//		//포탈이 넘어갈 때 위치정보 저장.
//		PlayerPrefs.SetInt (SceneManager.GetActiveScene ().buildIndex.ToString () + "Scene", _To_Scene);
//
//		//Load했을 때 캐릭터가 등장하는 것은 다음 씬이어야 해서, 그것은 Load_data Class에서 저장함.
//	}

	IEnumerator FadeIn(){
		_Player.GetComponent<Moving_by_RLbuttons> ().enabled = false;
		for (float f = 1f; f > 0; f -= Time.deltaTime) {
			bb.a = f;
			_blackout.color = bb;
			yield return null;
		}
		_blackout.color = new Color (0, 0, 0, 0);
		_Player.GetComponent<Moving_by_RLbuttons> ().enabled = true;
		enter_ = true; //해당 씬이 시작했음
	}

	IEnumerator FadeOut(){
		_Player.GetComponent<Moving_by_RLbuttons> ().enabled = false;
		for (float f = 0f; f < 1; f += Time.deltaTime) {
			Color c = _blackout.color;
			c.a = f;
			_blackout.color = c;
			yield return null;
		}
		_Player.GetComponent<Moving_by_RLbuttons> ().enabled = true;
		SceneManager.LoadScene (_To_Scene);
		//Save ();
		//포탈이 넘어갈 때 위치정보 저장.
		PlayerPrefs.SetInt ("SceneFromWhere", SceneManager.GetActiveScene ().buildIndex);
	}


}



