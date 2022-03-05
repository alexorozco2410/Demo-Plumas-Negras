using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject PrincipalPanel;
    public GameObject PanelControls;
    public Animator controlsAnimator;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void openControlsMenu() {
        PanelControls.SetActive(true);
        PrincipalPanel.SetActive(false);
        controlsAnimator.SetBool("openCM", true);
    }

    public void ReturnPrincipalPanel() {
        /*PanelControls.SetActive(false);
        PrincipalPanel.SetActive(true);*/
        controlsAnimator.SetBool("openCM", false);
    }

    public void ChangeSceneGameplay () {
        SceneManager.LoadScene(1);
    }

    /*public void ShowMainPanel(){
        PanelControls.SetActive(false);
        PrincipalPanel.SetActive(true);
    }*/

}
