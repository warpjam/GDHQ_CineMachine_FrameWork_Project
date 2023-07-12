using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    public Canvas menuCanvas;
    private bool menuActive = false;

    private void Update() 
    {
        
        if(Input.GetKeyDown("q") && !menuActive)
        {
            menuActive = true;
            menuCanvas.gameObject.SetActive(true);
            Time.timeScale = 0;
        }
        else if(Input.GetKeyDown("q") && menuActive)
        {
            menuActive = false;
            menuCanvas.gameObject.SetActive(false);
            Time.timeScale = 1;
        }
    }

    public void LoadGame()
    {
        SceneManager.LoadScene("SampleScene");
        Time.timeScale = 1;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
