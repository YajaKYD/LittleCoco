using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pot_Socket_Controller : MonoBehaviour {

    private bool box_used = false;
    private bool umbrella_used = false;

    private Moving_by_RLbuttons mbr;

    private Item_Controller i_c;
    private GameObject player;
    private GameObject umbrella;
    private GameObject hardbox;

    void Awake()
    {
        player = GameObject.Find("Player");
        mbr = player.GetComponent<Moving_by_RLbuttons>();
        i_c = GameObject.FindWithTag("Item_Canvas").GetComponent<Item_Controller>();

        umbrella = GameObject.Find("Umbrella");
        hardbox = GameObject.Find("HardBox");
    }

    void Start()
    {
        this.GetComponent<SpriteRenderer>().enabled = false;
    }

    void Update()
    {
        if (Stage5_Controller._Stage5_Quest[44] && !Stage5_Controller._Stage5_Quest[45])
        {
            mbr.enabled = false;
            this.GetComponent<SpriteRenderer>().enabled = true;
            if (!box_used || !umbrella_used)
            {
                if (i_c._now_used_item == "Umbrella")
                {
                    umbrella_used = true;
                    umbrella.SetActive(true);
                    GetComponent<Outline>().used_or_not_for_retry = false;
                }
                else if (i_c._now_used_item == "HardBox")
                {
                    box_used = true;
                    hardbox.SetActive(true);
                    GetComponent<Outline>().used_or_not_for_retry = false;
                }
            }
            else
            {
                this.GetComponent<SpriteRenderer>().enabled = false;
                Stage5_Controller._Stage5_Quest[45] = true; // 상자와 우산 설치 완료
            }
        }
    }
}
