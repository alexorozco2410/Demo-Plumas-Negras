using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class PuaseGame : MonoBehaviour
{
    public bool isPaused = false;
    public GameObject objIntroPause;
    public GameObject objOutroPuase;
    private PlayableDirector cinematicIntroPause;
    private PlayableDirector cinematicOutroPause;
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
                cinematicIntroPause.Play();
                isPaused = true;
                // Time.timeScale = 0;
            } else {
                cinematicOutroPause.Play();
                isPaused = false;
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
}
