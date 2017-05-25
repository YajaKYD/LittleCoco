using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage3_Controller : MonoBehaviour {

	//textimporter랑 clicktoclose에 npccollider를 주인이 나올 때 마다 controller에서 지정해야 함

	public Item_Controller ic;

	public static int sceneIndex;

	public static bool[] _Stage3_Quest = new bool[23];

	//public static bool _stage3_q1 = false; //첫 대사0
	//public static bool _stage3_q2 = false; //3-3 대사1
	//public static bool _stage3_q3 = false; //장난감 고르기 + 3-3 대사 2 완료2
	//public static bool _stage3_q3_1 = false; //1장난감 고르기3
	//public static bool _stage3_q3_2 = false; //2장난감 고르기4
	//public static bool _stage3_q3_3 = false; //3장난감 고르기5
	//public static bool _stage3_q4 = false; // 봉투 가져오기6
    //public static bool _stage3_q4_1 = false; // 봉투 먹는 순간 체크7
    //public static bool _stage3_q5 = false; // 산책 나가기8
    //public static bool _stage3_q6 = false; // Running mission start9
	//public static bool _stage3_q7 = false; // 산책 1/3 + 코코부름10
	//public static bool _stage3_q8 = false; // 1번 공던짐11
	//public static bool _stage3_q9 = false; // 공가져온다음 한번 더 던짐.12
	//public static bool _stage3_q10 = false; // 공놀이 완료. 산책 마무리.13
	//public static bool _stage3_q11 = false; // 나무 최초 클릭14
    //public static bool _stage3_q12 = false; // 개스타그램15
	//public static bool _stage3_q12_1 = false; // 나무 1-16
    //public static bool _stage3_q12_2 = false; // 나무 2-17
    //public static bool _stage3_q12_3 = false; // 나무 3-18
    //public static bool _stage3_q13 = false; // Maze19
	//public static bool _stage3_q13_1 = false; // earphone message click20
	//public static bool _stage3_q13_2 = false; // get basil21
	//public static bool _stage3_q13_3 = false; // get basil 2-22

	void Enable(){
		ic = GameObject.FindWithTag ("Item_Canvas").GetComponent<Item_Controller> ();
	}

	void Update () {
        
		if (ic != null && ic._now_used_item == "Star") {
			//별감은 계속 멍멍이한테 사용할 수 있다.
			GameObject.FindWithTag("Player").GetComponent<Outline> ().used_or_not_for_retry = false;
			//별감은 계속 멍멍이한테 사용할 수 있다.

		}

		//		if (Input.GetKeyDown (KeyCode.M)) {
		//			Text_Importer aa = GameObject.FindGameObjectWithTag ("Dialogue").GetComponent<Text_Importer> ();
		//			aa.currLineArr [1] += 2;//별감 다음대사 치게함.
		//		}
		//
		//		if (Input.GetKeyDown (KeyCode.K)) {
		//			Text_Importer aa = GameObject.FindGameObjectWithTag ("Dialogue").GetComponent<Text_Importer> ();
		//			aa.NPC_Say_yeah ("코코");
		//		}
		//
		//		if (Input.GetKey (KeyCode.Q) && Input.GetKey (KeyCode.W) && Input.GetKey (KeyCode.E)) {
		//			print ("JUMP!");
		//			Stage2_Controller._stage2_q1 = true;
		//			Stage2_Controller._stage2_q2 = true;
		//			Stage2_Controller._stage2_q3 = true;
		//			Stage2_Controller._stage2_q4 = true;
		//			Stage2_Controller._stage2_q5 = true;
		//			Stage2_Controller._stage2_q6 = true;
		//			//Stage2_Controller._stage2_q7 = true;
		//		}
		//
		//		if (Input.GetKeyDown (KeyCode.LeftAlt)) {
		//			print ("1 : " + _stage2_q1);
		//			print ("2 : " +_stage2_q2);
		//			print ("3 : " +_stage2_q3);
		//			print ("4 : " +_stage2_q4);
		//			print ("5 : " +_stage2_q5);
		//			print ("6 : " +_stage2_q6);
		//			print ("7 : " +_stage2_q7);
		//			print ("8 : " +_stage2_q8);
		//			print ("9 : " +_stage2_q9);
		//			print ("10 : " +_stage2_q10);
		//			print ("11 : " +_stage2_q11);
		//			print ("12 : " +_stage2_q12);
		//		}
	}
}
