using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial_Controller : MonoBehaviour {


    //호균
    public GameObject tutorialMessage;
    public GameObject[] tutorialMessagePrefab;
    private GameObject itemCanvas;
    public GameObject _ivon_textbox;
    private bool ivon_quest_end;
    public int tutorialMessageIndex;

    void Awake () {
		DontDestroyOnLoad (gameObject);

        itemCanvas = GameObject.FindWithTag("Item_Canvas");
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        tutorialMessageIndex = 0;
    }

    public void instantiateMessage(int index)
    {
        if (index == tutorialMessageIndex && tutorialMessagePrefab[index] != null)
        {
            tutorialMessage = Instantiate(tutorialMessagePrefab[index], Vector3.zero, Quaternion.identity) as GameObject;
            tutorialMessage.transform.SetParent(itemCanvas.transform, false);
            tutorialMessagePrefab[index] = null;
            //Debug.Log("instantiate");
        }
    }

    void OnDisable()
    {
        Destroy(tutorialMessage);
    }

}
