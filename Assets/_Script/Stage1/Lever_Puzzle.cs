using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever_Puzzle : MonoBehaviour {

	public BoxCollider2D xx;
	public Stage1_3_GameController aa;
	private Mirror_Socket_Controller msc1;
	private Mirror_Socket_Controller msc2;
	private bool a1a1 = false;
	public bool[] Lever_State = new bool[3];
	public Sprite[] cart_light;
	public SpriteRenderer cart;

	public Sprite[] lever_bg = new Sprite[2];//0-on/1-off
	public SpriteRenderer[] lever_each_bg = new SpriteRenderer[6]; //true0,1,2,fake0,1,2(1,2가 2에 붙어있음)
	public GameObject[] lever = new GameObject[6];

	public SpriteRenderer[] fake_lever_images = new SpriteRenderer[6];

	public bool[] judge_mirror = new bool[2];
	public bool[] judge_lever = new bool[3];

	public float lever_speed;

	void Awake(){
		Lever_State[0] = true;
		Lever_State[1] = false;
		Lever_State[2] = true;
		msc1 = GameObject.Find ("Mirror_Socket").GetComponent<Mirror_Socket_Controller> ();
		msc2 = GameObject.Find ("Mirror_Socket_2").GetComponent<Mirror_Socket_Controller> ();
		judge_mirror [0] = msc1.mirror_in_ornot;
		judge_mirror [1] = msc2.mirror_in_ornot;
		for (int x = 0; x < judge_lever.Length; x++) {
			judge_lever [x] = Lever_State [x];
		}
	}

	void Start(){
		xx.enabled = false;

		if (msc1.mirror_in_ornot && msc2.mirror_in_ornot) {
			//짝퉁 0,1 on 2 off
			StartCoroutine(Mirror_Effect_On(fake_lever_images[0]));
			StartCoroutine(Mirror_Effect_On(fake_lever_images[1]));
			StartCoroutine(Mirror_Effect_On(fake_lever_images[2]));
			StartCoroutine(Mirror_Effect_On(fake_lever_images[3]));
			StartCoroutine(Mirror_Effect_Off(fake_lever_images[4]));
			StartCoroutine(Mirror_Effect_Off(fake_lever_images[5]));
		}
//		else if (msc1.mirror_in_ornot && !msc2.mirror_in_ornot) {
//			//0 on 1,2 off
//			StartCoroutine(Mirror_Effect_On(fake_lever_images[0]));
//			StartCoroutine(Mirror_Effect_On(fake_lever_images[1]));
//			StartCoroutine(Mirror_Effect_Off(fake_lever_images[2]));
//			StartCoroutine(Mirror_Effect_Off(fake_lever_images[3]));
//			StartCoroutine(Mirror_Effect_Off(fake_lever_images[4]));
//			StartCoroutine(Mirror_Effect_Off(fake_lever_images[5]));
//		} else if (!msc1.mirror_in_ornot && msc2.mirror_in_ornot) {
//			//2 on 0,1 off
//			StartCoroutine(Mirror_Effect_On(fake_lever_images[4]));
//			StartCoroutine(Mirror_Effect_On(fake_lever_images[5]));
//			StartCoroutine(Mirror_Effect_Off(fake_lever_images[0]));
//			StartCoroutine(Mirror_Effect_Off(fake_lever_images[1]));
//			StartCoroutine(Mirror_Effect_Off(fake_lever_images[2]));
//			StartCoroutine(Mirror_Effect_Off(fake_lever_images[3]));
//		} else {
//			//1,2,3 off
//			StartCoroutine(Mirror_Effect_Off(fake_lever_images[0]));
//			StartCoroutine(Mirror_Effect_Off(fake_lever_images[1]));
//			StartCoroutine(Mirror_Effect_Off(fake_lever_images[2]));
//			StartCoroutine(Mirror_Effect_Off(fake_lever_images[3]));
//			StartCoroutine(Mirror_Effect_Off(fake_lever_images[4]));
//			StartCoroutine(Mirror_Effect_Off(fake_lever_images[5]));
//		}

		for (int i = 0; i < 3; i++) { //lever on and off
			if (Lever_State [i]) {//on
				StartCoroutine(LeverOnOff(true,i));
			} else {//off
				StartCoroutine(LeverOnOff(false,i));
			}
		}

	}

	void Update(){

		//왜.. 거울을 빼고나서 다시 못 넣는 지 모르겠다.

		if (judge_mirror [0] != msc1.mirror_in_ornot || judge_mirror [1] != msc2.mirror_in_ornot) {

			//거울사항이 변경될 때만 작동.
			if (judge_mirror [0] && judge_mirror [1] && !msc1.mirror_in_ornot) {
				StartCoroutine (Mirror_Effect_Off (fake_lever_images [0]));
				StartCoroutine (Mirror_Effect_Off (fake_lever_images [1]));
				StartCoroutine (Mirror_Effect_Off (fake_lever_images [2]));
				StartCoroutine (Mirror_Effect_Off (fake_lever_images [3]));
				StartCoroutine(Mirror_Effect_On(fake_lever_images[4]));
				StartCoroutine(Mirror_Effect_On(fake_lever_images[5]));
			} else if (judge_mirror [0] && judge_mirror [1] && !msc2.mirror_in_ornot) {
				StartCoroutine (Mirror_Effect_Off (fake_lever_images [2]));
				StartCoroutine (Mirror_Effect_Off (fake_lever_images [3]));
				StartCoroutine (Mirror_Effect_Off (fake_lever_images [4]));
				StartCoroutine (Mirror_Effect_Off (fake_lever_images [5]));
			} else if (!judge_mirror [0] && judge_mirror [1] && msc1.mirror_in_ornot) {
				StartCoroutine (Mirror_Effect_On (fake_lever_images [0]));
				StartCoroutine (Mirror_Effect_On (fake_lever_images [1]));
				StartCoroutine (Mirror_Effect_On (fake_lever_images [2]));
				StartCoroutine (Mirror_Effect_On (fake_lever_images [3]));
			} else if (!judge_mirror [0] && judge_mirror [1] && !msc2.mirror_in_ornot) {
				StartCoroutine (Mirror_Effect_Off (fake_lever_images [4]));
				StartCoroutine (Mirror_Effect_Off (fake_lever_images [5]));
			} else if (judge_mirror [0] && !judge_mirror [1] && !msc1.mirror_in_ornot) {
				StartCoroutine (Mirror_Effect_Off (fake_lever_images [0]));
				StartCoroutine (Mirror_Effect_Off (fake_lever_images [1]));
			} else if (judge_mirror [0] && !judge_mirror [1] && msc2.mirror_in_ornot) {
				StartCoroutine (Mirror_Effect_On (fake_lever_images [2]));
				StartCoroutine (Mirror_Effect_On (fake_lever_images [3]));
			} else if (!judge_mirror [0] && !judge_mirror [1] && msc1.mirror_in_ornot) {
				StartCoroutine (Mirror_Effect_On (fake_lever_images [0]));
				StartCoroutine (Mirror_Effect_On (fake_lever_images [1]));
			} else {
				StartCoroutine(Mirror_Effect_On(fake_lever_images[4]));
				StartCoroutine(Mirror_Effect_On(fake_lever_images[5]));
			}


			judge_mirror [0] = msc1.mirror_in_ornot;
			judge_mirror [1] = msc2.mirror_in_ornot;
		}


//
		if (judge_lever [0] != Lever_State [0] || judge_lever [1] != Lever_State [1] || judge_lever [2] != Lever_State [2]) {
			
			for (int i = 0; i < 3; i++) { //lever on and off
				if (Lever_State [i]) {//on
					StartCoroutine(LeverOnOff(true,i));
				} else {//off
					StartCoroutine(LeverOnOff(false,i));
				}
			}

			for (int x = 0; x < judge_lever.Length; x++) {
				judge_lever [x] = Lever_State [x];
			}

		}



		if (Lever_State [0] && !Lever_State [1] && Lever_State [2] && !msc1.mirror_in_ornot && !msc2.mirror_in_ornot && !a1a1) {
			print ("END");
			StartCoroutine (Cart_running ());
			Invoke ("End_LeverPuzzle", 1f);

			a1a1 = true;
		}

	}

	public void End_LeverPuzzle(){
		gameObject.SetActive (false);
		print ("ligth");
		aa.puzzle_end = true;
	}

	IEnumerator Cart_running(){
		GameObject cart = transform.parent.gameObject;
		Vector3[] aa = new Vector3[3];
		aa[0] = new Vector3 (Random.Range (-0.2f, 0.2f), Random.Range (-0.2f, 0.2f), 0f);
		aa[1] = new Vector3 (Random.Range (-0.2f, 0.2f), Random.Range (-0.2f, 0.2f), 0f);
		aa[2] = new Vector3 (Random.Range (-0.2f, 0.2f), Random.Range (-0.2f, 0.2f), 0f);
		for (int i = 0 ; i < aa.Length ; i++) {
			cart.transform.localPosition += aa[i];
			yield return null;
			cart.transform.localPosition -= aa[i];
			yield return new WaitForSeconds(0.2f);
		}
	}

	IEnumerator LeverOnOff(bool aa, int i){
		while (true) {
			if (i == 0) {
				if (aa) {// off > on
					cart.sprite = cart_light[0];
					if (lever [0].transform.localPosition.y >= 2.5f) {
						lever_each_bg [0].sprite = lever_bg [0];
						lever_each_bg [3].sprite = lever_bg [0];
						lever_each_bg [4].sprite = lever_bg [0];
						break;
					} else {
						lever [0].transform.localPosition += new Vector3 (0f, lever_speed * Time.deltaTime, 0f);
						lever [3].transform.localPosition += new Vector3 (0f, lever_speed * Time.deltaTime, 0f);
						lever [4].transform.localPosition += new Vector3 (0f, lever_speed * Time.deltaTime, 0f);
					}
				} else { // on > off
					cart.sprite = cart_light[1];
					if (lever [0].transform.localPosition.y < 0f) {
						lever_each_bg [0].sprite = lever_bg [1];
						lever_each_bg [3].sprite = lever_bg [1];
						lever_each_bg [4].sprite = lever_bg [1];
						break;
					} else {
						lever [0].transform.localPosition += new Vector3 (0f, -lever_speed * Time.deltaTime, 0f);
						lever [3].transform.localPosition += new Vector3 (0f, -lever_speed * Time.deltaTime, 0f);
						lever [4].transform.localPosition += new Vector3 (0f, -lever_speed * Time.deltaTime, 0f);
					}
				}
			}
			if (i == 1) {
				if (aa) {// off > on
					if (lever [1].transform.localPosition.y >= 2.5f) {
						lever_each_bg [1].sprite = lever_bg [0];
						lever_each_bg [5].sprite = lever_bg [0];
						break;
					} else {
						lever [1].transform.localPosition += new Vector3 (0f, lever_speed * Time.deltaTime, 0f);
						lever [5].transform.localPosition += new Vector3 (0f, lever_speed * Time.deltaTime, 0f);
					}
				} else { // on > off
					if (lever [1].transform.localPosition.y < 0f) {
						lever_each_bg [1].sprite = lever_bg [1];
						lever_each_bg [5].sprite = lever_bg [1];
						break;
					} else {
						lever [1].transform.localPosition += new Vector3 (0f, -lever_speed * Time.deltaTime, 0f);
						lever [5].transform.localPosition += new Vector3 (0f, -lever_speed * Time.deltaTime, 0f);
					}
				}
			}
			if (i == 2) {
				if (aa) {// off > on
					if (lever [2].transform.localPosition.y >= 2.5f) {
						lever_each_bg [2].sprite = lever_bg [0];
						break;
					} else {
						lever [2].transform.localPosition += new Vector3 (0f, lever_speed * Time.deltaTime, 0f);
					}
				} else { // on > off
					if (lever [2].transform.localPosition.y < 0f) {
						lever_each_bg [2].sprite = lever_bg [1];
						break;
					} else {
						lever [2].transform.localPosition += new Vector3 (0f, -lever_speed * Time.deltaTime, 0f);
					}
				}
			}

			yield return null;
		}
	}
//
	IEnumerator Mirror_Effect_On(SpriteRenderer a){
		//if (a.size.x < 1f) {
			float i = 0f;
			while (true) {
				i += 0.08f;
				a.size = new Vector2 (i, 3.74f);
				if (i > 1.88) {
					a.size = new Vector2 (1.88f, 3.74f);
					break;
				}
				yield return null;
			}
		//}
	}

	IEnumerator Mirror_Effect_Off(SpriteRenderer a){
		//if (a.size.x > 1f) {
			float i = 1.88f;
			while (true) {
				i -= 0.08f;
				a.size = new Vector2 (i, 3.74f);
				if (i < 0) {
					a.size = new Vector2 (0f, 3.74f);
					break;
				}
				yield return null;
			}
		//}
	}
	public void Click_Lever(int i){
		if (Lever_State [i]) {
			Lever_State [i] = false;
		} else {
			Lever_State [i] = true;
		}

	}
}
