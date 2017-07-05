using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SevenGarbage2 : MonoBehaviour {
    private Vector3 offset;
    private Vector2 LastPos;
    private Vector2 mouseSpeed;

    public float throwSpeed;
    private bool mouseUp = false;

    private Vector2 start, end, force;

    public GameObject garbage1;
    public GameObject garbage2;
    public GameObject garbage3;
    public GameObject garbage4;
    public GameObject garbage5;
    public GameObject garbage6;
    public GameObject garbage7;

    private Rigidbody2D rb2D_garbage1;
    private Rigidbody2D rb2D_garbage2;
    private Rigidbody2D rb2D_garbage3;
    private Rigidbody2D rb2D_garbage4;
    private Rigidbody2D rb2D_garbage5;
    private Rigidbody2D rb2D_garbage6;
    private Rigidbody2D rb2D_garbage7;

    private bool garbage_1 = false; private bool garbage_1_1 = false; private bool garbage_2_1;
    private bool garbage_2 = false; private bool garbage_1_2 = false; private bool garbage_2_2;
    private bool garbage_3 = false; private bool garbage_1_3 = false; private bool garbage_2_3;
    private bool garbage_4 = false; private bool garbage_1_4 = false; private bool garbage_2_4;
    private bool garbage_5 = false; private bool garbage_1_5 = false; private bool garbage_2_5;
    private bool garbage_6 = false; private bool garbage_1_6 = false; private bool garbage_2_6;
    private bool garbage_7 = false; private bool garbage_1_7 = false; private bool garbage_2_7;

    private GameObject player;
    private GameObject trashHeap;

    void Awake()
    {
        rb2D_garbage1 = garbage1.GetComponent<Rigidbody2D>(); rb2D_garbage2 = garbage2.GetComponent<Rigidbody2D>(); rb2D_garbage3 = garbage3.GetComponent<Rigidbody2D>();
        rb2D_garbage4 = garbage4.GetComponent<Rigidbody2D>(); rb2D_garbage5 = garbage5.GetComponent<Rigidbody2D>(); rb2D_garbage6 = garbage6.GetComponent<Rigidbody2D>();
        rb2D_garbage7 = garbage7.GetComponent<Rigidbody2D>();

        player = GameObject.Find("Player");

        garbage1.SetActive(false); garbage2.SetActive(false); garbage3.SetActive(false);
        garbage4.SetActive(false); garbage5.SetActive(false); garbage6.SetActive(false); garbage7.SetActive(false);
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

            if (!garbage_1) rb2D_garbage1.AddForce(force * -1 * throwSpeed);
            else if (garbage_1 && !garbage_2) rb2D_garbage2.AddForce(force * -1 * throwSpeed);
            else if (garbage_2 && !garbage_3) rb2D_garbage3.AddForce(force * -1 * throwSpeed);
            else if (garbage_3 && !garbage_4) rb2D_garbage4.AddForce(force * -1 * throwSpeed);
            else if (garbage_4 && !garbage_5) rb2D_garbage5.AddForce(force * -1 * throwSpeed);
            else if (garbage_5 && !garbage_6) rb2D_garbage6.AddForce(force * -1 * throwSpeed);
            else if (garbage_6 && !garbage_7) rb2D_garbage7.AddForce(force * -1 * throwSpeed);

            start = new Vector2(0f, 0f);
            end = new Vector2(0f, 0f);
        }
    }

    void OnMouseDown()
    {
        if (!garbage_1 && !garbage_1_1 && !Stage5_Controller._Stage5_Quest[41])
        {
            garbage1.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 1f));
            garbage1.SetActive(true);
            Physics2D.IgnoreCollision(GetComponent<PolygonCollider2D>(), garbage1.GetComponent<BoxCollider2D>());
            Physics2D.IgnoreCollision(player.GetComponent<PolygonCollider2D>(), garbage1.GetComponent<BoxCollider2D>());
            garbage_2_1 = true;
            //offset = garbage1.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10.0f));
        }
        else if (garbage_1 && !garbage_2 && !garbage_1_2 && !Stage5_Controller._Stage5_Quest[41])
        {
            garbage2.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 1f));
            garbage2.SetActive(true);
            Physics2D.IgnoreCollision(GetComponent<PolygonCollider2D>(), garbage2.GetComponent<BoxCollider2D>());
            Physics2D.IgnoreCollision(player.GetComponent<PolygonCollider2D>(), garbage2.GetComponent<BoxCollider2D>());
            garbage_2_2 = true;
        }
        else if (garbage_2 && !garbage_3 && !garbage_1_3 && !Stage5_Controller._Stage5_Quest[41])
        {
            garbage3.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 1f));
            garbage3.SetActive(true);
            Physics2D.IgnoreCollision(GetComponent<PolygonCollider2D>(), garbage3.GetComponent<BoxCollider2D>());
            Physics2D.IgnoreCollision(player.GetComponent<PolygonCollider2D>(), garbage3.GetComponent<BoxCollider2D>());
            garbage_2_3 = true;
        }
        else if (garbage_3 && !garbage_4 && !garbage_1_4 && !Stage5_Controller._Stage5_Quest[41])
        {
            garbage4.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 1f));
            garbage4.SetActive(true);
            Physics2D.IgnoreCollision(GetComponent<PolygonCollider2D>(), garbage4.GetComponent<BoxCollider2D>());
            Physics2D.IgnoreCollision(player.GetComponent<PolygonCollider2D>(), garbage4.GetComponent<BoxCollider2D>());
            garbage_2_4 = true;
        }
        else if (garbage_4 && !garbage_5 && !garbage_1_5 && !Stage5_Controller._Stage5_Quest[41])
        {
            garbage5.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 1f));
            garbage5.SetActive(true);
            Physics2D.IgnoreCollision(GetComponent<PolygonCollider2D>(), garbage5.GetComponent<BoxCollider2D>());
            Physics2D.IgnoreCollision(player.GetComponent<PolygonCollider2D>(), garbage5.GetComponent<BoxCollider2D>());
            garbage_2_5 = true;
        }
        else if (garbage_5 && !garbage_6 && !garbage_1_6 && !Stage5_Controller._Stage5_Quest[41])
        {
            garbage6.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 1f));
            garbage6.SetActive(true);
            Physics2D.IgnoreCollision(GetComponent<PolygonCollider2D>(), garbage6.GetComponent<BoxCollider2D>());
            Physics2D.IgnoreCollision(player.GetComponent<PolygonCollider2D>(), garbage6.GetComponent<BoxCollider2D>());
            garbage_2_6 = true;
        }
        else if (garbage_6 && !garbage_7 && !garbage_1_7 && !Stage5_Controller._Stage5_Quest[41])
        {
            garbage7.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 1f));
            garbage7.SetActive(true);
            Physics2D.IgnoreCollision(GetComponent<PolygonCollider2D>(), garbage7.GetComponent<BoxCollider2D>());
            Physics2D.IgnoreCollision(player.GetComponent<PolygonCollider2D>(), garbage7.GetComponent<BoxCollider2D>());
            garbage_2_7 = true;
        }
        // offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10.0f));
    }

    void OnMouseDrag()
    {
        if (!garbage_1 && !garbage_1_1 && garbage_2_1)
        {
            Vector3 newPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10.0f);
            garbage1.transform.position = Camera.main.ScreenToWorldPoint(newPosition) + offset;
        }
        else if (garbage_1 && !garbage_2 && !garbage_1_2 && garbage_2_2)
        {
            Vector3 newPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10.0f);
            garbage2.transform.position = Camera.main.ScreenToWorldPoint(newPosition) + offset;
        }
        else if (garbage_2 && !garbage_3 && !garbage_1_3 && garbage_2_3)
        {
            Vector3 newPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10.0f);
            garbage3.transform.position = Camera.main.ScreenToWorldPoint(newPosition) + offset;
        }
        else if (garbage_3 && !garbage_4 && !garbage_1_4 && garbage_2_4)
        {
            Vector3 newPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10.0f);
            garbage4.transform.position = Camera.main.ScreenToWorldPoint(newPosition) + offset;
        }
        else if (garbage_4 && !garbage_5 && !garbage_1_5 && garbage_2_5)
        {
            Vector3 newPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10.0f);
            garbage5.transform.position = Camera.main.ScreenToWorldPoint(newPosition) + offset;
        }
        else if (garbage_5 && !garbage_6 && !garbage_1_6 && garbage_2_6)
        {
            Vector3 newPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10.0f);
            garbage6.transform.position = Camera.main.ScreenToWorldPoint(newPosition) + offset;
        }
        else if (garbage_6 && !garbage_7 && !garbage_1_7 && garbage_2_7)
        {
            Vector3 newPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10.0f);
            garbage7.transform.position = Camera.main.ScreenToWorldPoint(newPosition) + offset;
        }
    }

    void OnMouseUp()
    {
        if (!garbage_1 && !garbage_1_1 && garbage_2_1 && !Stage5_Controller._Stage5_Quest[41])
        {
            StartCoroutine(Disappear1(garbage1));
            garbage_1_1 = true;
        }
        else if (garbage_1 && !garbage_2 && !garbage_1_2 && garbage_2_2 && !Stage5_Controller._Stage5_Quest[41])
        {
            StartCoroutine(Disappear2(garbage2));
            garbage_1_2 = true;
        }
        else if (garbage_2 && !garbage_3 && !garbage_1_3 && garbage_2_3 && !Stage5_Controller._Stage5_Quest[41])
        {
            StartCoroutine(Disappear3(garbage3));
            garbage_1_3 = true;
        }
        else if (garbage_3 && !garbage_4 && !garbage_1_4 && garbage_2_4 && !Stage5_Controller._Stage5_Quest[41])
        {
            StartCoroutine(Disappear4(garbage4));
            garbage_1_4 = true;
        }
        else if (garbage_4 && !garbage_5 && !garbage_1_5 && garbage_2_5 && !Stage5_Controller._Stage5_Quest[41])
        {
            StartCoroutine(Disappear5(garbage5));
            garbage_1_5 = true;
        }
        else if (garbage_5 && !garbage_6 && !garbage_1_6 && garbage_2_6 && !Stage5_Controller._Stage5_Quest[41])
        {
            StartCoroutine(Disappear6(garbage6));
            garbage_1_6 = true;
        }
        else if (garbage_6 && !garbage_7 && !garbage_1_7 && garbage_2_7 && !Stage5_Controller._Stage5_Quest[41])
        {
            StartCoroutine(Disappear7(garbage7));
            garbage_1_7 = true;
        }
    }

    IEnumerator Disappear1(GameObject garbage)
    {
        SpriteRenderer spriteRenderer = garbage.GetComponent<SpriteRenderer>();
        for (float f = 1f; f > 0; f -= Time.deltaTime)
        {
            Color c = spriteRenderer.color;
            c.a = f;
            spriteRenderer.color = c;
            yield return null;
        }
        Destroy(garbage.gameObject);
        garbage_1 = true;
    }

    IEnumerator Disappear2(GameObject garbage)
    {
        SpriteRenderer spriteRenderer = garbage.GetComponent<SpriteRenderer>();
        for (float f = 1f; f > 0; f -= Time.deltaTime)
        {
            Color c = spriteRenderer.color;
            c.a = f;
            spriteRenderer.color = c;
            yield return null;
        }
        Destroy(garbage.gameObject);
        garbage_2 = true;
    }

    IEnumerator Disappear3(GameObject garbage)
    {
        SpriteRenderer spriteRenderer = garbage.GetComponent<SpriteRenderer>();
        for (float f = 1f; f > 0; f -= Time.deltaTime)
        {
            Color c = spriteRenderer.color;
            c.a = f;
            spriteRenderer.color = c;
            yield return null;
        }
        Destroy(garbage.gameObject);
        garbage_3 = true;
    }

    IEnumerator Disappear4(GameObject garbage)
    {
        SpriteRenderer spriteRenderer = garbage.GetComponent<SpriteRenderer>();
        for (float f = 1f; f > 0; f -= Time.deltaTime)
        {
            Color c = spriteRenderer.color;
            c.a = f;
            spriteRenderer.color = c;
            yield return null;
        }
        Destroy(garbage.gameObject);
        garbage_4 = true;
    }

    IEnumerator Disappear5(GameObject garbage)
    {
        SpriteRenderer spriteRenderer = garbage.GetComponent<SpriteRenderer>();
        for (float f = 1f; f > 0; f -= Time.deltaTime)
        {
            Color c = spriteRenderer.color;
            c.a = f;
            spriteRenderer.color = c;
            yield return null;
        }
        Destroy(garbage.gameObject);
        garbage_5 = true;
    }

    IEnumerator Disappear6(GameObject garbage)
    {
        SpriteRenderer spriteRenderer = garbage.GetComponent<SpriteRenderer>();
        for (float f = 1f; f > 0; f -= Time.deltaTime)
        {
            Color c = spriteRenderer.color;
            c.a = f;
            spriteRenderer.color = c;
            yield return null;
        }
        Destroy(garbage.gameObject);
        garbage_6 = true;
    }

    IEnumerator Disappear7(GameObject garbage)
    {
        SpriteRenderer spriteRenderer = garbage.GetComponent<SpriteRenderer>();
        for (float f = 1f; f > 0; f -= Time.deltaTime)
        {
            Color c = spriteRenderer.color;
            c.a = f;
            spriteRenderer.color = c;
            yield return null;
        }
        Destroy(garbage.gameObject);
        Stage5_Controller._Stage5_Quest[41] = true;
        garbage_7 = true;
    }
}
