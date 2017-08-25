using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Save_Script : MonoBehaviour {

	public static bool[] S1 = new bool[11];
	public static bool[] S2 = new bool[40];
	public static int[] S2_intArr = new int[3];
	public static bool[] S3 = new bool[23];
	public static bool[] S4 = new bool[50];
	public static bool[] S4_puzzle = new bool[2];
	public static bool[] S5 = new bool[84];
    public static bool[] S6 = new bool[60];
	public static Item_Controller _ic_for_Save;
	//public static Text_Importer2 _ti_for_Save;

	//save는 전부 특정 시점에만 실행됨.

	public static void Save_Now_Point(){
		Save_Item_Info ();
		Save_Dialogue_Info ();
		Save_Quest_Info ();
		Save_Scene_Info ();
	}

	public static void Save_Item_Info(){ //아이템 정보

		_ic_for_Save = GameObject.FindWithTag ("Item_Canvas").GetComponent<Item_Controller> ();

		PlayerPrefsX.SetStringArray ("IC_nameList", _ic_for_Save._item_name_list);
		PlayerPrefsX.SetBoolArray ("Usable_item", _ic_for_Save._usable_item);
		PlayerPrefsX.SetIntArray ("NumOfItem", _ic_for_Save._the_number_of_items);
		PlayerPrefsX.SetStringArray ("Interaction", _ic_for_Save._interaction_object);
		PlayerPrefsX.SetBoolArray ("Consumable", _ic_for_Save._consumable);
		PlayerPrefsX.SetStringArray ("Explanation", _ic_for_Save._explanations);

	}
		
	public static void Save_Dialogue_Info(){ //대사 정보
		
//		if (GameObject.FindWithTag ("Dialogue")) {
//			_ti_for_Save = GameObject.FindWithTag ("Dialogue").GetComponent<Text_Importer2> ();
//		}
//		if (_ti_for_Save.textFile != null) {
//			PlayerPrefs.SetInt ("CurrArr", _ti_for_Save.lineNo);
//		}

	}

	public static void Save_Scene_Info(){ //씬 정보
		PlayerPrefs.SetInt("Restart_SceneNum",PlayerPrefs.GetInt("Now_SceneNum"));
	}

	public static void Save_Quest_Info(){ //퀘스트 정보
		Save_S1 ();
		Save_S2 ();
		Save_S3 ();
		Save_S4 ();
		Save_S5 ();
        Save_S6();
		PlayerPrefsX.SetBoolArray ("Stage1_Quest", S1);
		PlayerPrefsX.SetBoolArray ("Stage2_Quest", S2);
		PlayerPrefsX.SetIntArray ("Stage2_Quest_INT", S2_intArr);
		PlayerPrefsX.SetBoolArray ("Stage3_Quest", S3);
		PlayerPrefsX.SetBoolArray ("Stage4_Quest", S4);
		PlayerPrefsX.SetBoolArray ("Stage4_Quest_puzzle", S4_puzzle);
		PlayerPrefsX.SetBoolArray ("Stage5_Quest", S5);
        PlayerPrefsX.SetBoolArray("Stage6_Quest", S6);
    }

	public static void Save_S1(){
		//S1 = new bool[10];
		S1 = Stage1_Controller.q;
		//S1 [0] = Stage1_Controller._stage1_q1;
		//S1 [1] = Stage1_Controller._stage1_q2;
		//S1 [2] = Stage1_Controller._stage1_q3;
		//S1 [3] = Stage1_Controller._stage1_q4;
		//S1 [4] = Stage1_Controller._stage1_q5;
		//S1 [5] = Stage1_Controller._stage1_q6;
		//S1 [6] = Stage1_Controller._stage1_q7;
		//S1 [7] = Stage1_Controller._stage1_q8;
		//S1 [8] = Stage1_Controller._stage1_q9;
		//S1 [9] = Stage1_Controller._stage1_q10;
	}

	public static void Save_S2(){
		//S2 = new bool[25];
		//S2_intArr = new int[3];

		S2 = Stage2_Controller.q;
		S2_intArr = Stage2_Controller._Stage2_Quest_intArr;
		//		S2 [0] = Stage2_Controller._stage2_q1;
		//		S2 [1] = Stage2_Controller._stage2_q2;
		//		S2 [2] = Stage2_Controller._stage2_q3;
		//		S2 [3] = Stage2_Controller._stage2_q4;
		//		S2 [4] = Stage2_Controller._stage2_q5;
		//		S2 [5] = Stage2_Controller._stage2_q6;
		//		S2 [6] = Stage2_Controller._stage2_q7;
		//		S2 [7] = Stage2_Controller._stage2_q8;
		//		S2 [8] = Stage2_Controller._stage2_q9;
		//		S2 [9] = Stage2_Controller._stage2_q10;
		//		S2 [10] = Stage2_Controller._stage2_q11;
		//		S2 [11] = Stage2_Controller._stage2_q12;
		//		S2 [12] = Stage2_Controller._stage2_q12_1;
		//		S2 [13] = Stage2_Controller._stage2_q13;
		//		S2 [14] = Stage2_Controller._stage2_q14;
		//		S2 [15] = Stage2_Controller._stage2_q15;
		//		S2 [16] = Stage2_Controller._stage2_q16;
		//		S2 [17] = Stage2_Controller._stage2_q17;
		//		S2 [18] = Stage2_Controller._stage2_q18;
		//		S2 [19] = Stage2_Controller._stage2_q19;
	}

	public static void Save_S3(){
		//S3 = new bool[23];
		S3 = Stage3_Controller._Stage3_Quest;
		//Debug.Log ("save quest info activated");
	}

	public static void Save_S4(){
		S4 = Stage4_Controller.q;
		S4_puzzle = Stage4_Controller.q20;
	}

	public static void Save_S5(){
		S5 = Stage5_Controller.q;
	}

    public static void Save_S6()
    {
        S6 = Stage6_Controller.q;
    }
}
