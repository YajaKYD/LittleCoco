using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stage3_5_LikeButton : MonoBehaviour {

    public Sprite likedButton;
    public Sprite likeButton;
    public GameObject likeListUI;
    public Stage3_5_likeList likeListScript;
    public Stage3_6_GameController controller;
    private Stage3_5_tree_coco tree;

    void Start()
    {
        tree = GetComponentInParent<Stage3_5_tree_coco>();
        transform.parent.gameObject.SetActive(false);
        if (GameObject.Find("Stage3_6_GameController"))
        {
            controller = GameObject.Find("Stage3_6_GameController").GetComponent<Stage3_6_GameController>();
            Debug.Log("Find controller");
        }
    }
    	
    void OnMouseDown()
    {
        if (tree.like)
        {
            GetComponent<SpriteRenderer>().sprite = likeButton;
            tree.like = false;
            if (tree.ID == 1)
            {
				Stage3_Controller._Stage3_Quest[16] = false;
				Save_Script.Save_Quest_Info ();
                showHeart(0);
            }
            else if (tree.ID == 2)
            {
				Stage3_Controller._Stage3_Quest[17] = false;
				Save_Script.Save_Quest_Info ();
                showHeart(1);
            }
            else if (tree.ID == 3)
            {
				Stage3_Controller._Stage3_Quest[18] = false;
				Save_Script.Save_Quest_Info ();
                showHeart(2);
            }
        }
        else {
            GetComponent<SpriteRenderer>().sprite = likedButton;
            tree.like = true;
            if (tree.ID == 1)
            {
				Stage3_Controller._Stage3_Quest[16] = true;
				Save_Script.Save_Quest_Info ();
                showHeart(0);
            }
            else if (tree.ID == 2)
            {
				Stage3_Controller._Stage3_Quest[17] = true;
				Save_Script.Save_Quest_Info ();
                showHeart(1);
            }
            else if (tree.ID == 3)
            {
				Stage3_Controller._Stage3_Quest[18] = true;
				Save_Script.Save_Quest_Info ();
                showHeart(2);
            }
        }

        if (controller != null)
        {
            controller.activatePortal();
        } else
        {
            Debug.Log("not working");
        }
    }
    
    void showHeart(int i)
    {
        likeListUI = GameObject.Find("quest12_likelist(Clone)");
        likeListScript = likeListUI.GetComponent<Stage3_5_likeList>();
        if (tree.like && likeListUI != null)
        {
            likeListScript.heart[i].GetComponent<Image>().enabled = true;
        } else if(!tree.like && likeListUI != null)
        {
            likeListScript.heart[i].GetComponent<Image>().enabled = false;
        }
    }
}
