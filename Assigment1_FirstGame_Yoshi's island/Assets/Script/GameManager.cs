using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {


    Button startBtn;
    Button quitBtn;
    public AudioClip TitleMusic;
    //SoundManager sm;
    public AudioSource TitleSource;

    // Use this for initialization
    void Start () {
        if (!TitleSource)
        {
            TitleSource = gameObject.AddComponent<AudioSource>();
            TitleSource.playOnAwake = false;
            TitleSource.loop = false;
            TitleSource.volume = 1.0f;
            Debug.Log("No AudioSource found on " + TitleSource);

        }
        if (TitleSource)
        {
            TitleSource.clip = TitleMusic;
            TitleSource.volume = 0.5f;
            TitleSource.Play();
        }

        //sm = SoundManager.instance;
        //if(sm == null)
        //{
        //    Debug.Log("Sm not found");
        //}

        //sm.PlayMusic(TitleMusic);
        startBtn = GameObject.Find("Button_Start").GetComponent<Button>();
        GameObject temp = GameObject.Find("Button_Quit");
        if (temp)
        {
            quitBtn = temp.GetComponent<Button>();
        }
        startBtn.onClick.AddListener(StartGame);
        quitBtn.onClick.AddListener(QuitGame);
        //SoundManager.instance.PlayMusic(TitleMusic);
    }
	
	// Update is called once per frame
	void Update () {

    }

    public void StartGame()
    {
        TitleSource.Stop();
        //SoundManager.instance.StopMusic();
        SceneManager.LoadScene("Mario");
        Time.timeScale = 1;
        //SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        TitleSource.Stop();
        Debug.Log("Quitting....");
        Application.Quit();
    }
}
