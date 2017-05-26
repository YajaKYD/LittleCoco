using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine.UI;

[RequireComponent(typeof(Renderer))]
public class Outline : MonoBehaviour
{
	//Always 		= will always have a outline around the object
	//MouseOver 	= will create a rigidbody + collider to make it work automatically
	//Click 		= will show it when clicking on it and remove it when clicking again. Also, create rigidbody + collider
	//OFF  			= you will have to enable/disable this component to show it.
	public enum OutlineType {Always, MouseOver, Click, OFF};

	//variables
	private OutlineManager vOutlineManager;
	private bool CanShow = false;
	public Outline MasterOutline;							//always keep who's the top Outline in all parts to ONLY call him once
	public int color;
	public OutlineType vOutlineType = OutlineType.OFF;		//by default, it will be OFF 
	public bool eraseRenderer;
	public bool ShowOutline = true;
//	private bool IsChild = false;

	//내가 추가//
	public bool used_or_not_for_retry;
	public int _item_num;
	//내가 추가//

	[HideInInspector]
	public int originalLayer;
	[HideInInspector]
	public Material originalMaterial;

	void Start()
    {

    }

	void StartOutline()
	{
		switch (vOutlineType) {
		case OutlineType.Always: 
			ShowHide_Outline (true);
			break;

			//create rigibody + use MouseEnter, MouseExit and MouseDown properly
		case OutlineType.Click: 
		case OutlineType.MouseOver: 

			//check if we have a RigidBody2D.
			Rigidbody2D vRigidBody2d = gameObject.GetComponent<Rigidbody2D> ();

			//if not, we create one
			if (vRigidBody2d == null)
				vRigidBody2d = gameObject.AddComponent<Rigidbody2D> ();

			//make him to not fall 
			vRigidBody2d.isKinematic = true;

			//check if we have a BoxCollider
			BoxCollider2D vBoxCollider2D = gameObject.GetComponent<BoxCollider2D> ();

			//if not, we create one
			if (vBoxCollider2D == null)
				vBoxCollider2D = gameObject.AddComponent<BoxCollider2D> ();

			//make him trigger 
			vBoxCollider2D.isTrigger = true;

			break;

			//disable itself on start
		case OutlineType.OFF:
			ShowHide_Outline (ShowOutline);
			break;
		}
	}

	public void Initialise()
	{
		//create the list which contain all the spriterenderer
		List<SpriteRenderer> sprites = new List<SpriteRenderer> ();

		//try to get the outlinemanager
		if (vOutlineManager == null) {
			vOutlineManager = Camera.main.GetComponent<OutlineManager> ();

			//if not create the outline manager on the main camera
			//by default, it will have red, blue and green color
			if (vOutlineManager == null)
				vOutlineManager = Camera.main.gameObject.AddComponent<OutlineManager> ();
		}

		if (vOutlineManager == null)
			Debug.Log ("Add a Camera to the game");

		//is his master
		SetMasterOutline (this);

		//get all the sprite renderer below
		foreach (SpriteRenderer vCurRenderer in GetComponentsInChildren<SpriteRenderer> ().OfType<SpriteRenderer> ().ToList ())
			sprites.Add (vCurRenderer);

		foreach (SpriteRenderer vRenderer in sprites) {
			//try to get this component
			Outline vOutline = vRenderer.GetComponent<Outline> (); 

			//if doesn't exist, create it
			if (vOutline == null) {
				vOutline = vRenderer.gameObject.AddComponent<Outline> ();
				vOutline.vOutlineManager = vOutlineManager;
				vOutline.color = color;
				vOutline.vOutlineType = vOutlineType;

				//save master for this once
				vOutline.SetMasterOutline (this);
			}

			//start them all
			vOutline.StartOutline ();
		}
	}

	//save the master outline here
	public void SetMasterOutline(Outline vMasterOutline)
	{
		MasterOutline = vMasterOutline;
	}

	public void ShowHide_Outline(bool vChoice)
	{
		//get all outlines child
		if (MasterOutline != null) {
			Outline[] Outlines = MasterOutline.GetComponentsInChildren<Outline> ();

			//add/remove them all
			foreach (Outline vCurOutline in Outlines) {
				if (vChoice)
					vOutlineManager.AddOutline (vCurOutline);
				else
					vOutlineManager.RemoveOutline (vCurOutline);
			}
		}

		//save the preference
		CanShow = vChoice;
	}

    void OnDisable()
    {
//		vOutlineManager.RemoveOutline(this);
    }

	//ONLY work on mouseover type
	void OnMouseOver()
	{
		if (vOutlineType == OutlineType.MouseOver)
			ShowHide_Outline (true);
		else if (vOutlineType == OutlineType.Click && Input.GetMouseButtonDown(0))
		{
			//show/hide this outline
			ShowHide_Outline (!CanShow);
		}
	}

	//내가 추가//
//	void OnMouseDown(){
//
//		Item_Controller aa = GameObject.Find ("Item_Canvas").GetComponent<Item_Controller> ();
//
//		if (eraseRenderer == false && !used_or_not_for_retry) {//활성화일때 && 사용되지 않은 상태
//			aa._now_used_item = aa._item_name_list[_item_num]; // 사용한 아이템 이름
//
//			if (aa._consumable [_item_num]) {//얘가 소모성일 때
//				if (aa._the_number_of_items [_item_num] == 1) {
//					aa._item_name_list [_item_num] = null;
//					aa._usable_item [_item_num] = false;
//					aa._interaction_object [_item_num] = null;
//					aa._the_number_of_items [_item_num] = 0;
//					aa._item_list [_item_num].GetComponent<Image> ().color = new Color (255, 255, 255, 0);
//					aa._item_list [_item_num].transform.parent.GetComponentInChildren<Text> ().color = new Color (255, 255, 255, 0);
//					aa.cant_pick_during_using = true;
//				} else {
//					aa._the_number_of_items [_item_num]--;
//					aa._item_list [_item_num].transform.parent.GetComponentInChildren<Text> ().text = aa._the_number_of_items [_item_num].ToString ();
//					aa.cant_pick_during_using = true;
//				}
//			}
//			//Camera.main.GetComponent<OutlineManager>().enabled = false;
//			Time.timeScale = 1;//아이템 사용하면 퍼즈 풀림
//			eraseRenderer = true;
//			used_or_not_for_retry = true; //사용된 상태(차지한 상태)면 ture
//		}
//	}

	public void SSDDSSDD(){
		Item_Controller aa = GameObject.Find ("Item_Canvas").GetComponent<Item_Controller> ();

		if (!used_or_not_for_retry) {//활성화일때 && 사용되지 않은 상태
			
			aa._now_used_item = aa._item_name_list[_item_num]; // 사용한 아이템 이름

			if (aa._consumable [_item_num]) {//얘가 소모성일 때
				if (aa._the_number_of_items [_item_num] == 1) {
					aa._item_name_list [_item_num] = "";
					aa._usable_item [_item_num] = false;
					aa._interaction_object [_item_num] = "";
					aa._the_number_of_items [_item_num] = 0;
					aa._item_list [_item_num].GetComponent<Image> ().color = new Color (1, 1, 1, 0);
					aa._item_list [_item_num].transform.parent.GetComponentInChildren<Text> ().color = new Color (1, 1, 1, 0);
					aa._explanations [_item_num] = "";
					aa.cant_pick_during_using = true;
				} else {
					aa._the_number_of_items [_item_num]--;
					aa._item_list [_item_num].transform.parent.GetComponentInChildren<Text> ().text = aa._the_number_of_items [_item_num].ToString ();
					aa.cant_pick_during_using = true;
				}

			}
			//사용되었다면 사용된 상대에 대한 정보 + 그 후 무슨일이 일어날 지?(GameController 외에도)
			// -> Item_Drag Class에서 다룸. hit.collider.tag로 판별이 더 쉬움

			//Camera.main.GetComponent<OutlineManager>().enabled = false;
			//Time.timeScale = 1;//아이템 사용하면 퍼즈 풀림
			//eraseRenderer = true;
			used_or_not_for_retry = true; //사용된 상태(차지한 상태)면 ture
		}
	}
	//내가 추가//

	void OnMouseExit()
	{
		if (vOutlineType == OutlineType.MouseOver)
			ShowHide_Outline (false);
	}
}
