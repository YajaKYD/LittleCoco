using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stage5_7_1_GameController : MonoBehaviour {

    private Transform start_pos;
    private GameObject player;
    private Moving_by_RLbuttons mbr;
    private GameObject _star_textbox;
    private GameObject _coco_textbox;
    private GameObject _ivon_textbox;
    private Text_Importer ti;
    private Item_Controller ic;

    public GameObject starDoll;

    private GameObject trashHeap;
    public Text mission;

    private bool q1a1 = false;
    private bool q1a2 = false;
    private bool q1a3 = false;
    private bool q1a4 = false;
    private bool q1a5 = false;
    private bool q1a6 = false;
    private bool sevenTime = false;
    private bool q2a1 = false;
    private bool q2a2 = false;
    private bool q2a3 = false;
    private bool q2a4 = false;

    private float velocity = 0.0f;
    private float smoothTime = 1.5f; // For Camera move

    void Awake()
    {
        player = GameObject.Find("Player");
        mbr = player.GetComponent<Moving_by_RLbuttons>();
        start_pos = GameObject.Find("Start_Pos").transform;
        ic = GameObject.FindWithTag("Item_Canvas").GetComponent<Item_Controller>();

        player.transform.position = start_pos.position;

        trashHeap = GameObject.Find("TrashHeap");
        mission.enabled = false;
    }

    void Start()
    {
        if (GetComponent<Load_data>()._where_are_you_from == 36)
        {
            player.transform.position = start_pos.position;
        }

        ti = GameObject.FindWithTag("Dialogue").GetComponent<Text_Importer>();
        _star_textbox = ti._text_boxes[0];
        _ivon_textbox = ti._text_boxes[1];
        _coco_textbox = ti._text_boxes[2];

        if (!Stage5_Controller._Stage5_Quest[37])
        {
            starDoll.GetComponent<DragRigidBody2D>().enabled = false;
        }
    }

    void Update()
    {
        if (Stage5_Controller._Stage5_Quest[35] && !Stage5_Controller._Stage5_Quest[36])
        {
            Q1_Find_Trash_Ani();
        }
        else if (Stage5_Controller._Stage5_Quest[36] && !Stage5_Controller._Stage5_Quest[37])
        {
            Q2_Start_Ani();
        }
        else if (Stage5_Controller._Stage5_Quest[37] && !Stage5_Controller._Stage5_Quest[38])
        {
            Q3_Seven_Trash();
        }
    }

    void Q1_Find_Trash_Ani()
    {
        if (!q1a1)
        {
            ti.currLineArr[0] = 135; // 엇 저 인형은?
            ti.NPC_Say_yeah("별감");
            q1a1 = true;
        }
        else if (q1a1 && !q1a2 && !_star_textbox.activeSelf)
        {
            ti.currLineArr[2] = 54;
            ti.NPC_Say_yeah("코코");
            q1a2 = true;
        }
        else if (q1a2 && !q1a3 && !_coco_textbox.activeSelf)
        {
            ti.currLineArr[0] = 137; // 그래 한번 찾아보자
            ti.NPC_Say_yeah("별감");
            q1a3 = true;
        }
        else if (q1a3 && !q1a4 && !_star_textbox.activeSelf)
        {
            StartCoroutine(MoveToTrash());
            q1a5 = true;
        }
        else if (q1a5)
        {
            Stage5_Controller._Stage5_Quest[36] = true;
        }
    }

    void Q2_Start_Ani()
    {
        if (!q2a1)
        {
            StartCoroutine(Mission());
            q2a1 = true;
        }
        else if (q2a1 && !mission.enabled)
        {
            starDoll.GetComponent<DragRigidBody2D>().enabled = true;
        }
    }

    void Q3_Seven_Trash()
    {

    }
    
    IEnumerator MoveToTrash()
    {
        mbr.enabled = false;
        print("자동으로 쓰레기 더미 앞까지 가기");
        while (true)
        {
            float newPosition = Mathf.SmoothDamp(player.transform.position.x, trashHeap.transform.position.x, ref velocity, smoothTime);
            player.transform.position = new Vector3(newPosition, player.transform.position.y, player.transform.position.z);
            yield return new WaitForSeconds(1f);
            q1a4 = true;
            break;
        }
    }

    IEnumerator Mission()
    {
        while (true)
        {
            mission.enabled = true;
            yield return new WaitForSeconds(2f);
            mission.enabled = false;
            break;
        }
    }

    void Execute_Ani()
    {
        print("쓰레기 더미 헤집는 중");
    }
}
