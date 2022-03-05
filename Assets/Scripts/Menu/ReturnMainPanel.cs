using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnMainPanel : MonoBehaviour
{
    public GameObject PrincipalPanel;
    public GameObject PanelControls;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ShowMainPanel(){
        PanelControls.SetActive(false);
        PrincipalPanel.SetActive(true);
    }
}
