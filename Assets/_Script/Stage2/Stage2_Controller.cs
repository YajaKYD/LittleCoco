using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//집배경인 Stage2의 경우 인물의 크기가 1.4배

public class Stage2_Controller : MonoBehaviour {

	public Item_Controller ic;
	private GameObject player;

	//public GameObject _dialogue_Canvas;
	//public Text_Importer _ti_stage2;

	public static bool[] _Stage2_Quest = new bool[26];
	public static int[] _Stage2_Quest_intArr = new int[3];

	//public static bool _stage2_q1 = false;								0
	//public static bool _stage2_q2 = false;								1
	//public static bool _stage2_q3 = false;								2
	//public static bool _stage2_q4 = false;								3
	//public static bool _stage2_q5 = false; //멀티탭을 먹음					4
	//public static bool _stage2_q6 = false;								5
	//public static bool _stage2_q6_m1 = false;//multi1 사용					6
	//public static bool _stage2_q6_m2 = false;//multi2 사용					7
	//public static bool _stage2_q6_starsay = false;//1,2 사용 후 별감 대사	8
	//public static bool _stage2_q6_mm = false;//mirror 사용					NONUSED
	//public static bool _stage2_q6_1 = false; //2-3에서 한 번 부딪혀서 공포 띄워야 함.		9
	//public static bool _stage2_q7 = false; //remote 먹기 전 대사			10
	//public static bool _stage2_q7_1 = false; //remote 먹음					11
	//public static bool _stage2_q8 = false; //오디오 켜기					12
	//public static bool _stage2_q9 = false;								13
	//public static bool _stage2_q10 = false; //스피커로 말하기. 반드시 할 필요 없어보임.	14
	//public static bool _stage2_q11 = false;								15
	//public static bool _stage2_q12 = false; //스위치를 켜면 true			16
	//public static bool _stage2_q12_1 = false;								17
	//public static bool _stage2_q13 = false; //오르골						18
	//public static bool _stage2_q14 = false; //리모콘 이용해서 오디오를 꺼야함.19
	//public static bool _stage2_q15 = false; //무드등						20
	//public static bool _stage2_q16 = false; //태엽으로 다시 불 끄기			21
	//public static bool _stage2_q17 = false; //스위치 끄면 true				22
	//public static bool _stage2_q18 = false; //형상에 거울쓰기				23
	//public static bool _stage2_q19 = false; //별감 배치하기					24

	//public static int _stage2_q11_clockwork = 0;
	//public static int _stage2_q13_clockwork = 0;
	//public static int _stage2_q16_clockwork = 0;

	void Awake () {
		player = GameObject.Find ("Player");
	}

	void Update () {

		if (ic != null && ic._now_used_item == "Star") {
			//별감은 계속 멍멍이한테 사용할 수 있다.
			player.GetComponent<Outline> ().used_or_not_for_retry = false;
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