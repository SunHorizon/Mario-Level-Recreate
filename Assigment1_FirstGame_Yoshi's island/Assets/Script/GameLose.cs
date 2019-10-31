using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameLose : MonoBehaviour {

    CanvasGroup cg;
    Button again;
    Button Quit;

    // Use this for initialization
    void Start()
    {
        //Finished_Canvas.SetActive(false);
        again = GameObject.Find("Again_Button2").GetComponent<Button>();
        Quit = GameObject.Find("Quit_Button2").GetComponent<Button>();
        //cg.blocksRaycasts = false;
        cg = GetComponent<CanvasGroup>();
        if (!cg)
        {
            cg = gameObject.AddComponent<CanvasGroup>();
            cg.alpha = 0.0f;
            cg.blocksRaycasts = false;
            again.interactable = false;
            Quit.interactable = false;
        }
        again.onClick.AddListener(NewGame);
        Quit.onClick.AddListener(QuitGame);
    }

    // Update is called once per frame
    void Update()
    {
        if (Character.DeadCheck)
        {
            cg.alpha = 1.0f;
            Time.timeScale = 0;
            cg.blocksRaycasts = true;
            again.interactable = true;
            Quit.interactable = true;
            PauseManager.PausedCheck = false;
        }
        if (Character.FinishedCheck)
        {
            cg.alpha = 0.0f;
            Time.timeScale = 0;
            cg.blocksRaycasts = false;
            again.interactable = false;
            Quit.interactable = false;
        }
        if (PauseManager.PausedCheck)
        {
            cg.blocksRaycasts = false;
            again.interactable = false;
            Quit.interactable = false;
        }

    }

    public void QuitGame()
    {
        //SceneManager.UnloadSceneAsync("Level1");
        cg.alpha = 0.0f;
        Time.timeScale = 1;
        cg.blocksRaycasts = false;
        again.interactable = false;
        Quit.interactable = false;
        Character.DeadCheck = false;
        SceneManager.LoadScene("Title");
    }

    public void NewGame()
    {
        cg.alpha = 0.0f;
        Time.timeScale = 1;
        cg.blocksRaycasts = false;
        again.interactable = false;
        Quit.interactable = false;
        Character.DeadCheck = false;
        SceneManager.LoadScene("Mario");
    }
}
