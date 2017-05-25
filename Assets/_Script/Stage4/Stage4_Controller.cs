using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage4_Controller : MonoBehaviour {
	
	public Item_Controller ic;

	//public static bool[] _Stage4_Quest = new bool[20];

	public static bool _stage4_q1 = false;
	public static bool _stage4_q2 = false;
	public static bool _stage4_q3 = false;
	public static bool _stage4_q4 = false;
	public static bool _stage4_q5 = false;
	public static bool _stage4_q6 = false;
	public static bool _stage4_q7 = false;
	public static bool _stage4_q8 = false;
	public static bool _stage4_q9 = false;
	public static bool _stage4_q10 = false;

	void Enable(){
		ic = GameObject.FindWithTag ("Item_Canvas").GetComponent<Item_Controller> ();
	}

	void Update () {

		if (ic != null && ic._now_used_item == "Star") {
			//별감은 계속 멍멍이한테 사용할 수 있다.
			GameObject.FindWithTag("Player").GetComponent<Outline> ().used_or_not_for_retry = false;
			//별감은 계속 멍멍이한테 사용할 수 있다.

		}
	}

}
