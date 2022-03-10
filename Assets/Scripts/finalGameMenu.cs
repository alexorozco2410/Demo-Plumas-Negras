using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class finalGameMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void exitMainMenu () {
        SceneManager.LoadScene(0);
    }

    public void restart () {
        SceneManager.LoadScene(1);
    }
}
