using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage3_5_tree_coco : MonoBehaviour {
    private GameObject player;
    protected Stage3_5_GameController controller;
    protected bool closeToTree;
    private Item_Controller ic;
    private Text_Importer ti;
    public GameObject posting;
    public bool postingOn;
    public bool like;
    public int ID;
    public int treeNo;
    public bool showPosting;
    public GameObject[] postings;
    private bool[] instructQuest;

    void Awake()
    {
        postings = GameObject.FindGameObjectsWithTag("posting");
		instructQuest = new bool[2];
    }

    void Start () {
        ti = GameObject.FindWithTag("Dialogue").GetComponent<Text_Importer>();
        controller = GameObject.Find("Stage3_5_GameController").GetComponent<Stage3_5_GameController>();
        like = true;
    }

    void OnTriggerStay2D()
    {
        closeToTree = true;
    }
    void OnTriggerExit2D()
    {
        closeToTree = false;
    }
	
//    void OnMouseDown()
//    {
//		if (!Stage3_Controller._Stage3_Quest[14])
//        {
//			Stage3_Controller._Stage3_Quest[14] = true;
//			Save_Script.Save_Quest_Info ();
//            ti.NPC_Say_yeah("별감"); // 나무를 만지면 흔적을 볼 수 있어(설명1)
//		} else if (!postingOn && Stage3_Controller._Stage3_Quest[14] && closeToTree)
//        {
//            for(int i=0; i<postings.Length; i++)
//            {
//                postings[i].SetActive(false);
//                controller.trees[i].GetComponent<Stage3_5_tree_coco>().postingOn = false;
//            }
//            posting.SetActive(true);
//            postingOn = true;
//
//            if (!instructQuest)
//            {
//                ti.currLineArr[2] += 2; // 별감이 다음 대사
//                ti.NPC_Say_yeah("별감"); // 화살표를 클릭해서 좋아요를 눌러준 친구를 확인하고 너도 좋아요를 눌러주렴
//                instructQuest = true;
//            }
//
//		} else if(postingOn && Stage3_Controller._Stage3_Quest[14] && closeToTree)
//        {
//            posting.SetActive(false);
//            postingOn = false;
//        }
//    }


	void OnMouseDown()
	{
		if (!instructQuest[0])
		{
			instructQuest[0] = true;
			//Save_Script.Save_Quest_Info ();
			ti.NPC_Say_yeah("별감"); // 나무를 만지면 흔적을 볼 수 있어(설명1)
		} else if (!postingOn && instructQuest[0] && closeToTree)
		{
			for(int i=0; i<postings.Length; i++)
			{
				postings[i].SetActive(false);
				controller.trees[i].GetComponent<Stage3_5_tree_coco>().postingOn = false;
			}
			posting.SetActive(true);
			postingOn = true;

			if (!instructQuest[1])
			{
				ti.currLineArr[2] += 2; // 별감이 다음 대사
				ti.NPC_Say_yeah("별감"); // 화살표를 클릭해서 좋아요를 눌러준 친구를 확인하고 너도 좋아요를 눌러주렴
				instructQuest[1] = true;
			}

		} else if(postingOn && instructQuest[0] && closeToTree)
		{
			posting.SetActive(false);
			postingOn = false;
		}
	}
}
