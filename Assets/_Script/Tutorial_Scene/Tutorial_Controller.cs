using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial_Controller : MonoBehaviour {


    //호균
    public GameObject tutorialMessage;
    public GameObject[] tutorialMessagePrefab;
	public GameObject[] buttonForTutorial;
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
		Debug.Log ("tutorialMessageIndex is " + tutorialMessageIndex);
    }

    public void instantiateMessage(int index)
    {
		Debug.Log ("index is " + index + ", " + tutorialMessageIndex);
        if (index == tutorialMessageIndex && tutorialMessagePrefab[index] != null)
        {
            tutorialMessage = Instantiate(tutorialMessagePrefab[index], Vector3.zero, Quaternion.identity) as GameObject;
            tutorialMessage.transform.SetParent(itemCanvas.transform, false);
			//tutorialMessage.transform.SetSiblingIndex (itemCanvas.transform.childCount - 2 - index);
			try{
				buttonForTutorial [index].transform.SetAsLastSibling ();
				if(index == 5){
					buttonForTutorial[0].transform.SetAsLastSibling();
				}
			} catch {
				Debug.Log ("no button");
			}
            tutorialMessagePrefab[index] = null;
        }
    }

    void OnDisable()
    {
        Destroy(tutorialMessage);
    }

}
