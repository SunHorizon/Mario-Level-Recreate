using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour {


    CanvasGroup cg;

    Button resume;
    Button quit;
    public AudioClip PauseSound;
    public static bool PausedCheck;

    // Use this for initialization
    void Start () {
        resume = GameObject.Find("Button_Resume").GetComponent<Button>();
        quit = GameObject.Find("Button_Quit").GetComponent<Button>();

        cg = GetComponent<CanvasGroup>();
        if (!cg)
        {
            cg = gameObject.AddComponent<CanvasGroup>();
            cg.alpha = 0.0f;
            resume.interactable = false;
            quit.interactable = false;
            cg.blocksRaycasts = false;
        }
        resume.onClick.AddListener(GameResume);
        quit.onClick.AddListener(QuitGame);
    }
	
	// Update is called once per frame
	void Update () {
        if ((Character.FinishedCheck == false && Character.DeadCheck == false))
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                resume.interactable = true;
                quit.interactable = true;
                PauseGame();
                SoundManager.instance.PlaySound(PauseSound);
                cg.blocksRaycasts = true;
                PausedCheck = true;
            }
        }
        if (Character.FinishedCheck)
        {
            cg.blocksRaycasts = false;
            resume.interactable = false;
            quit.interactable = false;
            PausedCheck = false;
        }
        if (Character.DeadCheck)
        {
            cg.blocksRaycasts = false;
            resume.interactable = false;
            quit.interactable = false;
            PausedCheck = false;
        }
    }
    public void QuitGame()
    {
        //SceneManager.UnloadSceneAsync("Level1");
        SceneManager.LoadScene("Title");
        resume.interactable = false;
        quit.interactable = false;
        cg.blocksRaycasts = false;
        PausedCheck = false;
    }

    public void GameResume()
    {
        cg.alpha = 0.0f;
        Time.timeScale = 1;
        resume.interactable = false;
        quit.interactable = false;
        cg.blocksRaycasts = false;
        PausedCheck = false;

    }

    void PauseGame()
    {
        if (cg.alpha == 0.0f)
        {
            cg.alpha = 1.0f;
            Time.timeScale = 0;
            PausedCheck = true;
        }
        else
        {
            PausedCheck = false;
            resume.interactable = false;
            quit.interactable = false;
            cg.blocksRaycasts = false;
            cg.alpha = 0.0f;
            Time.timeScale = 1;
        }
    }
}
