using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewBehaviourScript : MonoBehaviour {

	public Image aa;
	string file_path;
	private bool aabb = false;

	void Update () {

		if (Input.GetKey (KeyCode.C) && Input.GetMouseButtonDown (0)) {
			print ("Capture");
			//print (System.DateTime.Now.ToString ("yy_MM_dd_HH_mm_ss"));
			//Application.CaptureScreenshot ("Assets/Resources/" + System.DateTime.Now.ToString ("yy_MM_dd_HH_mm_ss") + ".png");
			//Texture2D x = Resources.Load (System.DateTime.Now.ToString ("yy_MM_dd_HH_mm_ss") + ".png") as Texture2D;

			file_path = System.DateTime.Now.ToString ("yy_MM_dd_HH_mm_ss");

			//Application.CaptureScreenshot ("Assets/Resources/" + file_path + ".png");
			//print(Resources.Load ("a"));
			//Texture2D x = Resources.Load (file_path) as Texture2D;


			Application.CaptureScreenshot ("Assets/Resources/" + "a" + ".png");
			//print(Resources.Load ("a"));

			aabb = true;
			//aa.sprite = Sprite.Create (x, new Rect (0f, 0f, x.width, x.height), new Vector2 (0.5f, 0.5f));
		}
		if (aabb) {
			AA ();
			aabb = false;
		}

		//캡쳐를 해서 파일로 저장은 문제가 없음
		//근데 그 캡쳐한 것을 다시 sprite로 쓰려고 하면 문제
		//캡쳐한 파일이 unity에 다시 로딩되는 것이 즉각적이지 않기 때문
		//해결하려면 아예 파일이 아니라 array로 메모리상에서 옮겨야 할 듯?

		if (Input.GetKey (KeyCode.A) && Input.GetKey (KeyCode.S) && Input.GetKey (KeyCode.D)) {
			print (Stage1_Controller._Stage1_Quest.Length);
			print (Stage2_Controller._Stage2_Quest.Length);
		}

	}

	void AA(){
		Texture2D x = Resources.Load ("a") as Texture2D;
		print (x);
	}
}
