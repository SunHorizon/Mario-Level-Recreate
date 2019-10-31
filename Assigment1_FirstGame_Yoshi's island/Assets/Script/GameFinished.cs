using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameFinished : MonoBehaviour {

    CanvasGroup cg;
    Button again;
    Button quit;


    // Use this for initialization
    void Start () {
        //Finished_Canvas.SetActive(false);
        again = GameObject.Find("Again_Button1").GetComponent<Button>();
        quit = GameObject.Find("Quit_Button1").GetComponent<Button>();
        //cg.blocksRaycasts = false;
        cg = GetComponent<CanvasGroup>();
        if (!cg)
        {
            cg = gameObject.AddComponent<CanvasGroup>();
            cg.alpha = 0.0f;
            cg.blocksRaycasts = false;
            again.interactable = false;
            quit.interactable = false;
        }
        again.onClick.AddListener(NewGame);
        quit.onClick.AddListener(QuitGame);
    } 
	
	// Update is called once per frame
	void Update () {
        if (Character.FinishedCheck)
        {
            cg.alpha = 1.0f;
            Time.timeScale = 0;
            cg.blocksRaycasts = true;
            again.interactable = true;
            quit.interactable = true;
            PauseManager.PausedCheck = false;
        }
        if (Character.DeadCheck)
        {
            cg.blocksRaycasts = false;
            again.interactable = false;
            quit.interactable = false;
        }
        if (PauseManager.PausedCheck)
        {
            cg.blocksRaycasts = false;
            again.interactable = false;
            quit.interactable = false;
        }

    }

    public void QuitGame()
    {
        //SceneManager.UnloadSceneAsync("Level1");
        cg.alpha = 0.0f;
        Time.timeScale = 1;
        cg.blocksRaycasts = false;
        again.interactable = false;
        quit.interactable = false;
        Character.FinishedCheck = false;
        SceneManager.LoadScene("Title");
    }

    public void NewGame()
    {
        cg.alpha = 0.0f;
        Time.timeScale = 1;
        cg.blocksRaycasts = false;
        again.interactable = false;
        quit.interactable = false;
        Character.FinishedCheck = false;
        SceneManager.LoadScene("Mario");   
    }
}

