using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class arrowButton : MonoBehaviour {

    public GameObject page1, page2;
    public GameObject likeList;
    private bool instructQuest;
    private GameObject ui;
    
    void Awake()
    {
        page1.SetActive(true);
        page2.SetActive(false);
        ui = GameObject.FindWithTag("Item_Canvas");
    }
    
    void OnMouseDown()
    {
        page1.SetActive(!page1.activeSelf);
        page2.SetActive(!page1.activeSelf);
        transform.Rotate(new Vector3(0, transform.rotation.y + 180f, 0));

        if (!instructQuest)
        {
            likeList = Instantiate(likeList, new Vector3(0,467,0), Quaternion.identity) as GameObject;
            likeList.transform.SetParent(ui.transform, false);
            instructQuest = true;
        }
    }
}
