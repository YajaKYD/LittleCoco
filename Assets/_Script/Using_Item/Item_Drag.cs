using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Item_Drag : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler{

	private Image item_img;
	private Item_Controller ic;
	private GameObject item_Info;

	public int numbering;
	public TurnOnOffItemList onoffList;
	public AudioSource _click_sound;
	public AudioSource _diary_sound;
	public Sprite diary_ac;
	public Sprite diary_nac;
	public static bool _NOW_Shaked = false;

	private float before_x;
	private float before_distance = 0f;
	private float dif_bet_beforeandafter = 0f;
	private int countshake = 0;

	private bool drag_ornot = false;

	public bool _diary_usable = false;
	private bool foronce = false;
	void Awake(){
		ic = GameObject.FindWithTag ("Item_Canvas").GetComponent<Item_Controller> ();
		item_Info = GameObject.Find ("Item_Info");
		item_img = GetComponent<Image> ();
	}

	void Start(){
		item_Info.SetActive (false);

		if (ic._item_name_list [numbering] == "Diary" && Stage2_Controller.q[6] && Stage2_Controller.q[7] && !Stage2_Controller.q[25]) {
			print ("Change Diary Image -usable- on startphase");
			this.GetComponent<Image> ().sprite = diary_ac;
			_diary_usable = true;
			foronce = true; //following diary usable value
		}

		for (int i = 0; i < ic._item_name_list.Length; i++) {
			if (ic._item_name_list [i] == "" && i == numbering) {
				gameObject.GetComponent<Item_Drag> ().enabled = false;
			}
		}

	}

	void Update(){
		if (countshake >= 4) {
			_NOW_Shaked = true;
			countshake = 0;
		}

		for (int i = 0; i < ic._item_name_list.Length; i++) {
			if (ic._item_name_list [i] == "" && i == numbering) {
				gameObject.GetComponent<Item_Drag> ().enabled = false;
			}
		}

		if (ic._item_name_list [numbering] == "Diary" && _diary_usable && !foronce) {
			print ("Change Diary Imgae -usable- on Updatephase");
			this.GetComponent<Image> ().sprite = diary_ac;
			foronce = true;
		}

		if (ic._item_name_list [numbering] == "Diary" && !_diary_usable && foronce) {
			print ("Change Diary Image -unusable- on Updatephase");
			this.GetComponent<Image> ().sprite = diary_nac;
			foronce = false;
		}



	}

	public virtual void OnDrag(PointerEventData ped){
		Vector2 pos;
		if (RectTransformUtility.ScreenPointToLocalPointInRectangle (item_img.rectTransform, ped.position, Camera.main, out pos)) {
			item_img.rectTransform.position = ped.position;
		}
		onoffList.OnTime = Time.realtimeSinceStartup;
		if (!drag_ornot) {
			item_Info.SetActive (true);
			item_Info.GetComponentInChildren<Text> ().text = ic._explanations [numbering];
			drag_ornot = true;
		}

		//일기장 사용
		if(ic._item_name_list[numbering] == "Diary" && foronce){
			dif_bet_beforeandafter = Input.mousePosition.x - before_x;
			if (dif_bet_beforeandafter * before_distance >= 0) {
				//방향그대로

			} else {
				//방향바뀜
				if (!_diary_sound.isPlaying) {
					_diary_sound.Play ();
				}
				countshake ++;
			}
			before_distance = dif_bet_beforeandafter;
			before_x = Input.mousePosition.x;

		}
		//
	}

	public virtual void OnPointerDown(PointerEventData ped){
		OnDrag (ped);
		onoffList.OnTime = Time.realtimeSinceStartup;
		_click_sound.Play ();
		//일기장 사용
		if(ic._item_name_list[numbering] == "Diary"){
			before_x = Camera.main.ScreenToWorldPoint (Input.mousePosition).x;
			before_distance = 0f;
			dif_bet_beforeandafter = 0f;
		}
		//
	}


	public virtual void OnPointerUp(PointerEventData ped){

		item_Info.SetActive (false);
		drag_ornot = false;
		_click_sound.Play ();

		Vector2 wp = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		Ray2D ray = new Ray2D (wp, Vector2.zero);
		RaycastHit2D hit = Physics2D.Raycast (ray.origin, ray.direction);

		if (hit.collider != null) {
			//tag 비교해서 아이템사용처럼 해야함
			if (hit.collider.tag == ic._interaction_object [numbering]) {
				//print (hit.collider.name);
				Outline aa = hit.collider.gameObject.GetComponent<Outline>();
				aa._item_num = numbering;
				aa.SSDDSSDD();
				//사용되었다면 사용된 상대에 대한 정보 + 그 후 무슨일이 일어날 지?(GameController 외에도)
				ic._now_object_item_used = hit.collider.name;
				ic._to_run_once = true;
			} else {
				
			}
		}

		item_img.rectTransform.anchoredPosition = Vector3.zero;
	}

}
