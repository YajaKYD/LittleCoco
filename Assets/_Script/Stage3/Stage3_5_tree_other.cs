using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage3_5_tree_other : Stage3_5_tree_coco
{
	private int clickcount;
	private Moving_by_RLbuttons mbr;

    void Start()
    {
		mbr = GameObject.FindWithTag ("Player").GetComponent<Moving_by_RLbuttons>();
        if (GameObject.Find("Stage3_5_GameController"))
        {
            controller = GameObject.Find("Stage3_5_GameController").GetComponent<Stage3_5_GameController>();
        } else
        {
            controller = GameObject.Find("Stage3_6_GameController").GetComponent<Stage3_6_GameController>();
        }
    }

    void OnTriggerStay2D()
    {
        closeToTree = true;
    }
    void OnTriggerExit2D()
    {
        closeToTree = false;
    }

    void OnMouseDown()
    {
		clickcount++;
		Debug.Log ("clickcount " + clickcount);



		if (Stage3_Controller._Stage3_Quest[14] && closeToTree)
        {
			//animation
			mbr.SetState(CocoState.Smell);

            if (!postingOn)
            {
                for (int i = 0; i < postings.Length; i++)
                {
                    postings[i].SetActive(false);
                    controller.trees[i].GetComponent<Stage3_5_tree_coco>().postingOn = false;
                }
                posting.SetActive(true);
                postingOn = true;
            }
            else if (postingOn)
            {
                posting.SetActive(false);
                postingOn = false;
            }
        }
    }
}
