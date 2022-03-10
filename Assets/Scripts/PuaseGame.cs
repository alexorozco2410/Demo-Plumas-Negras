using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PuaseGame : MonoBehaviour
{
    public bool isPaused = false;
    public GameObject objIntroPause;
    public GameObject objOutroPuase;
    private PlayableDirector cinematicIntroPause;
    private PlayableDirector cinematicOutroPause;

    public GameObject principalPanelPause;
    public GameObject itemsPanelPause;

    // Start is called before the first frame update
    void Start()
    {
        cinematicIntroPause = objIntroPause.GetComponent<PlayableDirector>();
        cinematicOutroPause = objOutroPuase.GetComponent<PlayableDirector>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (!isPaused) {
                Pause();
                // Time.timeScale = 0;
            } else {
                returnMainPanel();
                Resume();
                // Time.timeScale = 1;
            }
        }
    }

    public void PauseGame() {
        Time.timeScale = 0;
    }

    public void ResumeGame() {
        Time.timeScale = 1;
    }

    public void Pause() {
        cinematicIntroPause.Play();
        isPaused = true;
    }
    public void Resume() {
        cinematicOutroPause.Play();
        isPaused = false;
    }

    public void exitMainMenu () {
        SceneManager.LoadScene(0);
    }

    public void restart () {
        SceneManager.LoadScene(1);
    }

    public void openItemsMenu() {
        principalPanelPause.SetActive(false);
        itemsPanelPause.SetActive(true);
    }

    public void returnMainPanel() {
        itemsPanelPause.SetActive(false);
        principalPanelPause.SetActive(true);
    }
}
