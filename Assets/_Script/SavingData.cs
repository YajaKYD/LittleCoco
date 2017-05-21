using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

[Serializable]
public class SavingData{
	public int num_TO_scene; //어디 맵으로 넘어가는 지?
	public int dialogue_line; //현재 대화의 진행 정도
}

[Serializable]
public class AutoSavingToRestart{
	public int now_Scene_number; //지금 어디 씬?
	public GameObject player;
	public GameObject Item_Canvas; 
	//public bool q0 = Stage1_2_GameController.stage1_2_mirror_or_not;
	//public bool q1 = Stage1_4_GameController.stage1_4_mirror_or_not;


}