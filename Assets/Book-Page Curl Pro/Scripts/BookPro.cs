﻿using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using System.Collections;
using System;
public enum FlipMode
{
    RightToLeft,
    LeftToRight
}
public class BookPro : MonoBehaviour
{
    Canvas canvas;
    [SerializeField]
    RectTransform BookPanel;
    public Image ClippingPlane;
    public Image Shadow;
    public Image LeftPageShadow;
    public Image RightPageShadow;
    public Image ShadowLTR;
    public RectTransform LeftPageTransform;
    public RectTransform RightPageTransform;
    public bool interactable = true;
    public bool enableShadowEffect = true;

    [HideInInspector]
    public int currentPaper = 0;
    [HideInInspector]
    public Paper[] papers;
    /// <summary>
    /// OnFlip invocation list, called when any page flipped
    /// </summary>
    public UnityEvent OnFlip;

    /// <summary>
    /// The Current Shown paper (the paper its front shown in right part)
    /// </summary>
    public int CurrentPaper
    {
        get { return currentPaper; }
        set
        {
            if (value != currentPaper)
            {
                if (value <StartFlippingPaper)
                    currentPaper = StartFlippingPaper;
                else if (value > EndFlippingPaper+1)
                    currentPaper = EndFlippingPaper+1;
                else
                    currentPaper = value;
                UpdatePages();
            }
        }
    }
    [HideInInspector]
    public int StartFlippingPaper = 0;
    [HideInInspector]
    public int EndFlippingPaper = 1;

    public Vector3 EndBottomLeft
    {
        get { return ebl; }
    }
    public Vector3 EndBottomRight
    {
        get { return ebr; }
    }
    public float Height
    {
        get
        {
            return BookPanel.rect.height;
        }
    }

    Image Left;
    Image Right;

    //current flip mode
    FlipMode mode;

    bool pageDragging = false;

    // Use this for initialization
    void Start()
    {
        Canvas[] c = GetComponentsInParent<Canvas>();
        if (c.Length > 0)
            canvas = c[c.Length - 1];
        else
            Debug.LogError("Book Must be a child to canvas diectly or indirectly");

        UpdatePages();

        CalcCurlCriticalPoints();


        float pageWidth = BookPanel.rect.width / 2.0f;
        float pageHeight = BookPanel.rect.height;
        

        ClippingPlane.rectTransform.sizeDelta = new Vector2(pageWidth * 2 + pageHeight, pageHeight + pageHeight * 2);

        //hypotenous (diagonal) page length
        float hyp = Mathf.Sqrt(pageWidth * pageWidth + pageHeight * pageHeight);
        float shadowPageHeight = pageWidth / 2 + hyp;

        Shadow.rectTransform.sizeDelta = new Vector2(pageWidth, shadowPageHeight);
        Shadow.rectTransform.pivot = new Vector2(1, (pageWidth / 2) / shadowPageHeight);

        ShadowLTR.rectTransform.sizeDelta = new Vector2(pageWidth, shadowPageHeight);
        ShadowLTR.rectTransform.pivot = new Vector2(0, (pageWidth / 2) / shadowPageHeight);

        RightPageShadow.rectTransform.sizeDelta = new Vector2(pageWidth, shadowPageHeight);
        RightPageShadow.rectTransform.pivot = new Vector2(0, (pageWidth / 2) / shadowPageHeight);

        LeftPageShadow.rectTransform.sizeDelta = new Vector2(pageWidth, shadowPageHeight);
        LeftPageShadow.rectTransform.pivot = new Vector2(1, (pageWidth / 2) / shadowPageHeight);
    }

    /// <summary>
    /// transform point from global (world-space) to local space
    /// </summary>
    /// <param name="global">poit iin world space</param>
    /// <returns></returns>
    public Vector3 transformPoint(Vector3 global)
    {
        Vector2 localPos = BookPanel.InverseTransformPoint(global);
        return localPos;
    }
    /// <summary>
    /// transform mouse position to local space
    /// </summary>
    /// <param name="mouseScreenPos"></param>
    /// <returns></returns>
    public Vector3 transformPointMousePosition(Vector3 mouseScreenPos)
    {
        if(canvas.renderMode== RenderMode.ScreenSpaceCamera)
        {
            Vector3 mouseWorldPos = canvas.worldCamera.ScreenToWorldPoint(new Vector3(mouseScreenPos.x, mouseScreenPos.y, canvas.planeDistance));
            Vector2 localPos = BookPanel.InverseTransformPoint(mouseWorldPos);

            return localPos;
        }
        else
        {
            //Screen Space Overlay
            Vector2 localPos = BookPanel.InverseTransformPoint(mouseScreenPos);
            return localPos;
        }
        
    }

    /// <summary>
    /// Update page orders
    /// This function should be called whenever the current page changed, the dragging of the page started or the page has been flipped
    /// </summary>
    public void UpdatePages()
    {
        int previousPaper = pageDragging ? currentPaper - 2 : currentPaper - 1;

        //Hide all pages
        for (int i = 0; i < papers.Length; i++)
        {
            BookUtility.HidePage(papers[i].Front);
            papers[i].Front.transform.SetParent(BookPanel.transform);
            BookUtility.HidePage(papers[i].Back);
            papers[i].Back.transform.SetParent(BookPanel.transform);
        }

        //Show the back page of all previous papers
        for (int i = 0; i <= previousPaper; i++)
        {
            BookUtility.ShowPage(papers[i].Back);
            papers[i].Back.transform.SetParent(BookPanel.transform);
            papers[i].Back.transform.SetSiblingIndex(i);
            BookUtility.CopyTransform(LeftPageTransform.transform, papers[i].Back.transform);
        }

        //Show the front page of all next papers
        for (int i = papers.Length - 1; i >= currentPaper; i--)
        {
            BookUtility.ShowPage(papers[i].Front);
            papers[i].Front.transform.SetSiblingIndex(papers.Length - i + previousPaper);
            BookUtility.CopyTransform(RightPageTransform.transform, papers[i].Front.transform);
        }

        #region Shadow Effect
        if (enableShadowEffect)
        {
            //the shadow effect enabled
            if (previousPaper >= 0)
            {
                //has at least one previous page, then left shadow should be active
                LeftPageShadow.gameObject.SetActive(true);
                LeftPageShadow.transform.SetParent(papers[previousPaper].Back.transform, true);
                LeftPageShadow.rectTransform.anchoredPosition = new Vector3();
                LeftPageShadow.rectTransform.localRotation = Quaternion.identity;
            }
            else
            {
                //if no previous pages, the leftShaow should be disabled
                LeftPageShadow.gameObject.SetActive(false);
                LeftPageShadow.transform.SetParent(BookPanel, true);
            }

            if (currentPaper < papers.Length)
            {
                //has at least one next page, the right shadow should be active
                RightPageShadow.gameObject.SetActive(true);
                RightPageShadow.transform.SetParent(papers[currentPaper].Front.transform, true);
                RightPageShadow.rectTransform.anchoredPosition = new Vector3();
                RightPageShadow.rectTransform.localRotation = Quaternion.identity;
            }
            else
            {
                //no next page, the right shadow should be diabled
                RightPageShadow.gameObject.SetActive(false);
                RightPageShadow.transform.SetParent(BookPanel, true);
            }
        }
        else
        {
            //Enable Shadow Effect is Unchecked, all shadow effects should be disabled
            LeftPageShadow.gameObject.SetActive(false);
            LeftPageShadow.transform.SetParent(BookPanel, true);

            RightPageShadow.gameObject.SetActive(false);
            RightPageShadow.transform.SetParent(BookPanel, true);
            
        }
        #endregion
    }

  
    //mouse interaction events call back
    public void OnMouseDragRightPage()
    {
        if (interactable)
        {
          
            DragRightPageToPoint(transformPointMousePosition(Input.mousePosition));
        }

    }
    public void DragRightPageToPoint(Vector3 point)
    {
        if (currentPaper > EndFlippingPaper) return;
        pageDragging = true;
        mode = FlipMode.RightToLeft;
        f = point;

        ClippingPlane.rectTransform.pivot = new Vector2(1, 0.35f);
        currentPaper += 1;

        UpdatePages();

        Left = papers[currentPaper - 1].Front.GetComponent<Image>();
        BookUtility.ShowPage(Left.gameObject);
        Left.rectTransform.pivot = new Vector2(0, 0);
        Left.transform.position = RightPageTransform.transform.position;
        Left.transform.eulerAngles = new Vector3(0, 0, 0);

        Right = papers[currentPaper - 1].Back.GetComponent<Image>();
        BookUtility.ShowPage(Right.gameObject);
        Right.transform.position = RightPageTransform.transform.position;
        Right.transform.eulerAngles = new Vector3(0, 0, 0);
        
        if (enableShadowEffect) Shadow.gameObject.SetActive(true);
        UpdateBookRTLToPoint(f);
    }
    public void OnMouseDragLeftPage()
    {
        if (interactable)
        {
            DragLeftPageToPoint(transformPointMousePosition(Input.mousePosition));

        }

    }
    public void DragLeftPageToPoint(Vector3 point)
    {
        if (currentPaper <= StartFlippingPaper) return;
        pageDragging = true;
        mode = FlipMode.LeftToRight;
        f = point;

        UpdatePages();

        ClippingPlane.rectTransform.pivot = new Vector2(0, 0.35f);

        Right = papers[currentPaper - 1].Back.GetComponent<Image>();
        BookUtility.ShowPage(Right.gameObject);
        Right.transform.position = LeftPageTransform.transform.position;
        Right.transform.eulerAngles = new Vector3(0, 0, 0);
        Right.transform.SetAsFirstSibling();

        Left = papers[currentPaper - 1].Front.GetComponent<Image>();
        BookUtility.ShowPage(Left.gameObject);
        Left.gameObject.SetActive(true);
        Left.rectTransform.pivot = new Vector2(1, 0);
        Left.transform.position = LeftPageTransform.transform.position;
        Left.transform.eulerAngles = new Vector3(0, 0, 0);


        if (enableShadowEffect) ShadowLTR.gameObject.SetActive(true);
        UpdateBookLTRToPoint(f);
    }
    public void OnMouseRelease()
    {
        if (interactable)
            ReleasePage();
    }
    public void ReleasePage()
    {
        if (pageDragging)
        {
            pageDragging = false;
            float distanceToLeft = Vector2.Distance(c, ebl);
            float distanceToRight = Vector2.Distance(c, ebr);
            if (distanceToRight < distanceToLeft && mode == FlipMode.RightToLeft)
                TweenBack();
            else if (distanceToRight > distanceToLeft && mode == FlipMode.LeftToRight)
                TweenBack();
            else
                TweenForward();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (pageDragging && interactable)
        {
            UpdateBook();
        }
    }
    public void UpdateBook()
    {
        f = Vector3.Lerp(f, transformPointMousePosition(Input.mousePosition), Time.deltaTime * 10);
        if (mode == FlipMode.RightToLeft)
            UpdateBookRTLToPoint(f);
        else
            UpdateBookLTRToPoint(f);
    }
    
    /// <summary>
    /// This function called when the page dragging point reached its distenation after releasing the mouse
    /// This function will call the OnFlip invocation list
    /// if you need to call any fnction after the page flipped just add it to the OnFlip invocation list
    /// </summary>
    void Flip()
    {
        pageDragging = false;

        if (mode == FlipMode.LeftToRight)
            currentPaper -= 1;

        Left.transform.SetParent(BookPanel.transform, true);
        Left.rectTransform.pivot = new Vector2(0, 0);
        Right.transform.SetParent(BookPanel.transform, true);
        UpdatePages();
        Shadow.gameObject.SetActive(false);
        ShadowLTR.gameObject.SetActive(false);
        if (OnFlip != null)
            OnFlip.Invoke();
    }

    public void TweenForward()
    {
        if (mode == FlipMode.RightToLeft)
            StartCoroutine(TweenTo(ebl, 0.2f, () => { Flip(); }));
        else
            StartCoroutine(TweenTo(ebr, 0.2f, () => { Flip(); }));
    }
    public void TweenBack()
    {
        if (mode == FlipMode.RightToLeft)
        {
            StartCoroutine(TweenTo(ebr, 0.15f,
                () =>
                {
                    currentPaper -= 1;
                    Right.transform.SetParent(BookPanel.transform);
                    Left.transform.SetParent(BookPanel.transform);
                    pageDragging = false;
                    Shadow.gameObject.SetActive(false);
                    ShadowLTR.gameObject.SetActive(false);
                    UpdatePages();
                }
                ));
        }
        else
        {

            StartCoroutine(TweenTo(ebl, 0.15f,
                () =>
                {
                    Left.transform.SetParent(BookPanel.transform);
                    Right.transform.SetParent(BookPanel.transform);
                    pageDragging = false;
                    Shadow.gameObject.SetActive(false);
                    ShadowLTR.gameObject.SetActive(false);
                    UpdatePages();
                }
                ));
        }
    }
    public IEnumerator TweenTo(Vector3 to, float duration, System.Action onFinish)
    {
        int steps = (int)(duration / 0.025f);
        Vector3 displacement = (to - f) / steps;
        for (int i = 0; i < steps - 1; i++)
        {
            if (mode == FlipMode.RightToLeft)
                UpdateBookRTLToPoint(f + displacement);
            else
                UpdateBookLTRToPoint(f + displacement);

            yield return new WaitForSeconds(0.025f);
        }
        if (onFinish != null)
            onFinish();
    }

    #region Page Curl Internal Calculations
    //for more info about this part please check this link : http://rbarraza.com/html5-canvas-pageflip/

    float radius1, radius2;
    //Spine Bottom
    Vector3 sb;
    //Spine Top
    Vector3 st;
    //corner of the page
    Vector3 c;
    //Edge Bottom Right
    Vector3 ebr;
    //Edge Bottom Left
    Vector3 ebl;
    //follow point 
    Vector3 f;
    
    private void CalcCurlCriticalPoints()
    {

        float scaledPageWidth = (BookPanel.rect.width * BookPanel.transform.lossyScale.x) / 2;
        float scaledPageHeight = BookPanel.rect.height * BookPanel.transform.lossyScale.y;

        Vector3 globalsb = BookPanel.transform.position + new Vector3(0, -scaledPageHeight / 2);
        sb = transformPoint(globalsb);
        
        Vector3 globalebr = BookPanel.transform.position + new Vector3(scaledPageWidth, -scaledPageHeight / 2);
        ebr = transformPoint(globalebr);

        Vector3 globalebl = BookPanel.transform.position + new Vector3(-scaledPageWidth, -scaledPageHeight / 2);
        ebl = transformPoint(globalebl);

        Vector3 globalst = BookPanel.transform.position + new Vector3(0, scaledPageHeight / 2);
        st = transformPoint(globalst);

        radius1 = Vector2.Distance(sb, ebr);
        float pageWidth = BookPanel.rect.width / 2.0f;
        float pageHeight = BookPanel.rect.height;
        radius2 = Mathf.Sqrt(pageWidth * pageWidth + pageHeight * pageHeight);
    }
    public void UpdateBookRTLToPoint(Vector3 followLocation)
    {
        mode = FlipMode.RightToLeft;
        f = followLocation;
        if (enableShadowEffect)
        {
            Shadow.transform.SetParent(ClippingPlane.transform, true);
            Shadow.transform.localPosition = new Vector3(0, 0, 0);
            Shadow.transform.localEulerAngles = new Vector3(0, 0, 0);

            ShadowLTR.transform.SetParent(Left.transform);
            ShadowLTR.rectTransform.anchoredPosition = new Vector3();
            ShadowLTR.transform.eulerAngles = Vector3.zero;
            ShadowLTR.gameObject.SetActive(true);
        }
        Right.transform.SetParent(ClippingPlane.transform, true);

        Left.transform.SetParent(BookPanel.transform, true);
        c = Calc_C_Position(followLocation);
        Vector3 t1;
        float T0_T1_Angle = Calc_T0_T1_Angle(c, ebr, out t1);
        if (T0_T1_Angle >= -90) T0_T1_Angle -= 180;

        ClippingPlane.rectTransform.pivot = new Vector2(1, 0.35f);
        ClippingPlane.transform.eulerAngles = new Vector3(0, 0, T0_T1_Angle + 90);
        ClippingPlane.transform.position = BookPanel.TransformPoint(t1);


        RightPageShadow.transform.eulerAngles = new Vector3(0, 0, T0_T1_Angle + 90);
        RightPageShadow.transform.position = BookPanel.TransformPoint(t1);

        //page position and angle
        Right.transform.position = BookPanel.TransformPoint(c);
        float C_T1_dy = t1.y - c.y;
        float C_T1_dx = t1.x - c.x;
        float C_T1_Angle = Mathf.Atan2(C_T1_dy, C_T1_dx) * Mathf.Rad2Deg;
        Right.transform.eulerAngles = new Vector3(0, 0, C_T1_Angle);

        Left.transform.SetParent(ClippingPlane.transform, true);
        Left.transform.SetAsFirstSibling();

        Shadow.rectTransform.SetParent(Right.rectTransform, true);
    }
    public void UpdateBookLTRToPoint(Vector3 followLocation)
    {
        mode = FlipMode.LeftToRight;
        f = followLocation;
        if (enableShadowEffect)
        {
            ShadowLTR.transform.SetParent(ClippingPlane.transform, true);
            ShadowLTR.transform.localPosition = new Vector3(0, 0, 0);
            ShadowLTR.transform.localEulerAngles = new Vector3(0, 0, 0);

            Shadow.transform.SetParent(Right.transform);
            Shadow.rectTransform.anchoredPosition = new Vector3(0, 0, 0);
            Shadow.transform.eulerAngles = Vector3.zero;
            Shadow.gameObject.SetActive(true);
        }
        Left.transform.SetParent(ClippingPlane.transform, true);
        Right.transform.SetParent(BookPanel.transform, true);

        c = Calc_C_Position(followLocation);
        Vector3 t1;
        float T0_T1_Angle = Calc_T0_T1_Angle(c, ebl, out t1);
        if (T0_T1_Angle < 0) T0_T1_Angle += 180;

        ClippingPlane.transform.eulerAngles = new Vector3(0, 0, T0_T1_Angle - 90);
        ClippingPlane.transform.position = BookPanel.TransformPoint(t1);

        LeftPageShadow.transform.eulerAngles = new Vector3(0, 0, T0_T1_Angle - 90);
        LeftPageShadow.transform.position = BookPanel.TransformPoint(t1);

        //page position and angle
        Left.transform.position = BookPanel.TransformPoint(c);
        float C_T1_dy = t1.y - c.y;
        float C_T1_dx = t1.x - c.x;
        float C_T1_Angle = Mathf.Atan2(C_T1_dy, C_T1_dx) * Mathf.Rad2Deg;
        Left.transform.eulerAngles = new Vector3(0, 0, C_T1_Angle - 180);

        Right.transform.SetParent(ClippingPlane.transform, true);
        Right.transform.SetAsFirstSibling();

        ShadowLTR.rectTransform.SetParent(Left.rectTransform, true);
    }
    private float Calc_T0_T1_Angle(Vector3 c, Vector3 bookCorner, out Vector3 t1)
    {
        Vector3 t0 = (c + bookCorner) / 2;
        float T0_CORNER_dy = bookCorner.y - t0.y;
        float T0_CORNER_dx = bookCorner.x - t0.x;
        float T0_CORNER_Angle = Mathf.Atan2(T0_CORNER_dy, T0_CORNER_dx);
        float T0_T1_Angle = 90 - T0_CORNER_Angle;

        float T1_X = t0.x - T0_CORNER_dy * Mathf.Tan(T0_CORNER_Angle);
        T1_X = normalizeT1X(T1_X, bookCorner, sb);
        t1 = new Vector3(T1_X, sb.y, 0);
        ////////////////////////////////////////////////
        //clipping plane angle=T0_T1_Angle
        float T0_T1_dy = t1.y - t0.y;
        float T0_T1_dx = t1.x - t0.x;
        T0_T1_Angle = Mathf.Atan2(T0_T1_dy, T0_T1_dx) * Mathf.Rad2Deg;
        return T0_T1_Angle;
    }
    private float normalizeT1X(float t1, Vector3 corner, Vector3 sb)
    {
        if (t1 > sb.x && sb.x > corner.x)
            return sb.x;
        if (t1 < sb.x && sb.x < corner.x)
            return sb.x;
        return t1;
    }
    private Vector3 Calc_C_Position(Vector3 followLocation)
    {
        Vector3 c;
        f = followLocation;
        float F_SB_dy = f.y - sb.y;
        float F_SB_dx = f.x - sb.x;
        float F_SB_Angle = Mathf.Atan2(F_SB_dy, F_SB_dx);
        Vector3 r1 = new Vector3(radius1 * Mathf.Cos(F_SB_Angle), radius1 * Mathf.Sin(F_SB_Angle), 0) + sb;

        float F_SB_distance = Vector2.Distance(f, sb);
        if (F_SB_distance < radius1)
            c = f;
        else
            c = r1;
        float F_ST_dy = c.y - st.y;
        float F_ST_dx = c.x - st.x;
        float F_ST_Angle = Mathf.Atan2(F_ST_dy, F_ST_dx);
        Vector3 r2 = new Vector3(radius2 * Mathf.Cos(F_ST_Angle),
           radius2 * Mathf.Sin(F_ST_Angle), 0) + st;
        float C_ST_distance = Vector2.Distance(c, st);
        if (C_ST_distance > radius2)
            c = r2;
        return c;
    }
    #endregion

}
[Serializable]
public class Paper
{
    public GameObject Front;
    public GameObject Back;
}


public static class BookUtility
{
    /// <summary>
    /// Call this function to Show a Hidden Page
    /// </summary>
    /// <param name="page">the page to be shown</param>
    public static void ShowPage(GameObject page)
    {
        CanvasGroup cgf = page.GetComponent<CanvasGroup>();
        cgf.alpha = 1;
        cgf.blocksRaycasts = true;
    }

    /// <summary>
    /// Call this function to hide any page
    /// </summary>
    /// <param name="page">the page to be hidden</param>
    public static void HidePage(GameObject page)
    {
        CanvasGroup cgf = page.GetComponent<CanvasGroup>();
        cgf.alpha = 0;
        cgf.blocksRaycasts = false;
        page.transform.SetAsFirstSibling();
    }

    public static void CopyTransform(Transform from, Transform to)
    {
        to.position = from.position;
        to.rotation = from.rotation;
        to.localScale = from.localScale;

    }
}