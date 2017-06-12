using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage3_5_tree_other : Stage3_5_tree_coco
{
<<<<<<< HEAD
	private int clickcount;
=======
>>>>>>> 2d9e80396e0292ace09424982220109bc235a130

    void Start()
    {
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
<<<<<<< HEAD
		clickcount++;
		Debug.Log ("clickcount " + clickcount);
=======
>>>>>>> 2d9e80396e0292ace09424982220109bc235a130
		if (Stage3_Controller._Stage3_Quest[14] && closeToTree)
        {
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
