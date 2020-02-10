using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePause : MonoBehaviour
{
    private GameObject pauseCanvas;

    // Start is called before the first frame update
    private void Awake()
    {
        pauseCanvas = GameObject.Find("Canvas").transform.Find("PauseControl").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        PauseMenu();
    }

    void PauseMenu()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseMenuControl();
        }
    }

    void PauseMenuControl()
    {
        if (pauseCanvas.activeSelf)
        {
            DoContinue();
        }
        else
        {
            DoPause();
        }
    }

    public void DoContinue()
    {
        Time.timeScale = 1;
        pauseCanvas.SetActive(false);
    }

    public void DoPause()
    {
        Time.timeScale = 0;
        pauseCanvas.SetActive(true);
    }
}
