using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CycleMenu : MonoBehaviour
{
    public RectTransform[] pages;
    public Vector2[] targetPosition;
    int current = 0;
    Vector2 sampleSize ;
    bool locked = false;
    SceneControl sceneControl;

    // Start is called before the first frame update
    void Start()
    {
        sampleSize = GetComponent<RectTransform>().sizeDelta;
        sceneControl = GetComponent<SceneControl>();
    }

    // Update is called once per frame
    void Update()
    {
        NextOrPrevios();

        Refresh();

        EnterGame();
        
    }

    void NextOrPrevios()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            Next();
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Previous();
        }
    }

    public void Next()
    {
        if (locked)
            return;
        targetPosition[current].x = -sampleSize.x;
        current++;
        if(current >= pages.Length)
        {
            current = 0;
        }
        pages[current].anchoredPosition = new Vector2(sampleSize.x, 0);
        targetPosition[current].x = 0;
        StartCoroutine("Lock");
    }

    public void Previous()
    {
        if (locked)
            return;
        targetPosition[current].x = sampleSize.x;
        current--;
        if (current < 0)
        {
            current = pages.Length - 1;
        }
        pages[current].anchoredPosition = new Vector2(-sampleSize.x, 0);
        targetPosition[current].x = 0;
        StartCoroutine("Lock");
    }

    void Refresh()
    {
        for (int i = 0; i< pages.Length; i++)
        {
            Vector3 pos = pages[i].anchoredPosition3D;
            pos = Vector3.Lerp(pos, targetPosition[i], 10 * Time.deltaTime);
            pages[i].anchoredPosition3D = pos;
        }

    }

    void EnterGame()
    {
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            sceneControl.ChangeScene(pages[current].name);
        }
    }

    IEnumerator Lock()
    {
        locked = true;
        yield return new WaitForSeconds(0.2f);
        locked = false;
    }
}
