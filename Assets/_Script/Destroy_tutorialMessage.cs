using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Destroy_tutorialMessage : MonoBehaviour, IPointerDownHandler {
    public int index;
    private Tutorial_Controller tc;
    private GameObject tutorialMessage;

    void Start()
    {
        tc = GameObject.FindWithTag("Controller").GetComponent<Tutorial_Controller>();
        tutorialMessage = GameObject.FindWithTag("tutorialMessage");
    }

    void Update()
    {
        tutorialMessage = GameObject.FindWithTag("tutorialMessage");
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player") && tutorialMessage.name.Substring(17,1) == index.ToString())
        {
            Destroy(tutorialMessage);
            tc.tutorialMessageIndex++;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log("바위를 넘었다");
        if (other.gameObject.CompareTag("Player") && tutorialMessage.name.Substring(17, 1) == index.ToString())
        {
            Destroy(tutorialMessage);
            tc.tutorialMessageIndex++;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (tutorialMessage && tutorialMessage.name.Substring(17,1) == index.ToString())
        {
            Destroy(tutorialMessage);
            tc.tutorialMessageIndex++;
        }
    }
    
}
