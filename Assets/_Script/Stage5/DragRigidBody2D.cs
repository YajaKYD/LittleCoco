using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragRigidBody2D : MonoBehaviour
{
    private Vector3 offset;
    private Vector2 LastPos;
    private Vector2 mouseSpeed;
    private Rigidbody2D rb2D;
    private GameObject trashHeap;
    private GameObject player;
    private SpriteRenderer spriteRenderer;

    public float throwSpeed;
    private bool mouseUp = false;

    private Vector2 start, end, force;

    void Start()
    { 
        rb2D = GetComponent<Rigidbody2D>();
        LastPos = Vector3.zero;
        player = GameObject.Find("Player");
        trashHeap = GameObject.Find("TrashHeap");
        spriteRenderer = GetComponent<SpriteRenderer>();

        Physics2D.IgnoreCollision(trashHeap.GetComponent<BoxCollider2D>(), GetComponent<BoxCollider2D>());
        Physics2D.IgnoreCollision(player.GetComponent<BoxCollider2D>(), GetComponent<BoxCollider2D>());
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            start = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            end = Input.mousePosition;

            force = start - end;
            
            GetComponent<Rigidbody2D>().AddForce(force*-1*throwSpeed);

            start = new Vector2(0f, 0f);
            end = new Vector2(0f, 0f);
        }
        
        
    }

    void OnMouseDown()
    {
        offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10.0f));
    }

    void OnMouseDrag()
    {
        Vector3 newPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10.0f);
        transform.position = Camera.main.ScreenToWorldPoint(newPosition) + offset;
    }

    void OnMouseUp()
    {
        mouseUp = true;
        StartCoroutine(Disappear());
    }

    IEnumerator Disappear()
    {
        for (float f = 1f; f > 0; f -= Time.deltaTime)
        {
            Color c = spriteRenderer.color;
            c.a = f;
            spriteRenderer.color = c;
            yield return null;
        }
        Destroy(this.gameObject);
        Stage5_Controller._Stage5_Quest[37] = true; // 별감 인형이 사라지고 대화 시작하기 전까지 완료
    }
}