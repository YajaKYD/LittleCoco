using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Main_Select_Stage_Controller : MonoBehaviour {
	
	public RectTransform panel;
	public float speed_control;
	public float speed_control_world;
	public bool xx;
	public bool yy;
	public Vector3 next;
	public BookPro bp;

	Vector3 next_world;
	private Vector3 startPoint;
	private Vector3 endPoint;
	private float startTime;
	private float endTime;
	private float startPoint_world_x;
	private float endPoint_world_x;
	float speed_world;

	private GameObject player;
	private Moving_by_RLbuttons mbr;
	private GameObject Item_canvas;
	public GameObject follow_it;
	public Selecting_stage[] Stage_images;

	public static bool _PAYD_OR_NOT = false;

	void Awake(){
		player = GameObject.FindGameObjectWithTag ("Player");
		mbr = player.GetComponent<Moving_by_RLbuttons> ();
		Item_canvas = GameObject.Find ("Item_Canvas");
		for (int i = 0; i < 6; i++) {
			Stage_images [i].this_num = i; //각 stage에 번호 부여
		}
		player.transform.localScale = new Vector3 (1f, 1f, player.transform.localScale.z);


	}

	void Start(){
		if (Item_canvas && player) {
			Item_canvas.SetActive (false);
			player.transform.position = follow_it.transform.position;
		}


		bp.CurrentPaper = PlayerPrefs.GetInt ("Restart_SceneNum")-1;
	}

	void OnDisable(){
		Item_canvas.SetActive (true);
	}

	//역사상 최고 지저분한 코드!

//	void Update(){
//
//		if (Input.GetMouseButtonDown (0)) {//드래그 시작
//			startPoint = Input.mousePosition;
//			startTime = Time.realtimeSinceStartup;
//			startPoint_world_x = Camera.main.ScreenToWorldPoint (startPoint).x;
//		
//		} else if (Input.GetMouseButtonUp (0)) {//드래그 끝
//			endPoint = Input.mousePosition;
//			endTime = Time.realtimeSinceStartup;
//			endPoint_world_x = Camera.main.ScreenToWorldPoint (endPoint).x;
//
//			float dis = endPoint.x - startPoint.x;
//			float dis_world = endPoint_world_x - startPoint_world_x;
//			float tim = endTime - startTime;
//			float speed = Mathf.Abs( dis )/ tim;
//			speed_world = Mathf.Abs( dis_world )/ tim;
//			speed /= speed_control;
//			speed_world /= speed_control_world; // 속력 계산
//
//			next = panel.localPosition + new Vector3 (dis * speed, 0, 0); //속력만큼 보냄
//			next.x = Mathf.Clamp (next.x, -1400f, 1600f);
//			next_world = player.transform.position + new Vector3 (dis_world * speed_world, 0, 0);
//			xx = true;
//			yy = true;
//			
//		}
//			
//		if (xx) {
//			Vector3 aa = Vector3.zero;
//			panel.localPosition = Vector3.SmoothDamp (panel.localPosition, next, ref aa, Time.deltaTime * 5f);
//
//			/*
//			if (yy && panel.localPosition.x - next.x < -10f) {
//				player.transform.position += Vector3.right / 3f;
//			} else if (yy && panel.localPosition.x - next.x > 10f) {
//				player.transform.position += Vector3.left / 3f;
//			}
//			*/
//
//			if (yy && panel.localPosition.x - next.x < -10f && Mathf.Abs( speed_world ) > 0.5f) {
//				//드래그 끝 + 패널 많이 움직임 + 멍멍이 속력도 일정 이상 = 강아지 밀려남
//				player.transform.position += Vector3.right / 3f;
//			} else if (yy && panel.localPosition.x - next.x > 10f && Mathf.Abs( speed_world ) > 0.5f) {
//				//반대 방향
//				player.transform.position += Vector3.left / 3f;
//			}
//
//
//			if (Vector2.Distance( panel.localPosition ,next) < 50f) {
//				//패널 속력이 어느정도 느려지면 강아지 밀려나는거 끝남
//				yy = false;
//			}
//			if (Vector2.Distance( panel.localPosition ,next) < 1f) {
//				//패널 속력 많이 느려지면 멈춤
//				xx = false;
//			}
//		}
//
//		if(Mathf.Abs( follow_it.transform.position.x - player.transform.position.x ) < 0.1f){
//			//어느정도 가까워지면 멈춤
//			StopCoroutine ("Player_center");
//		}else if (follow_it.transform.position.x != player.transform.position.x) {
//			//강아지랑 가운데랑 차이나는 순간부터 달려오기 시작
//			StartCoroutine ("Player_center");
//		}
//	}
//
//	IEnumerator Player_center(){
//		if (follow_it.transform.position.x > player.transform.position.x){
//			mbr.Moving_Right(6f);
//		}else{
//			mbr.Moving_left (-6f);
//		}
//		yield return null;
//	}

	public void Click_Once(int i){
		for (int k = 0 ; k < 6; k ++){
			if (k == i) {
			} else {
				Stage_images [k].xxx = false;
				Stage_images [k].gameObject.GetComponent<Image> ().color = new Color (0.5f, 0.5f, 0.5f);
			}
		}
	}

}
