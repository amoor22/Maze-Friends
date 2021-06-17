using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIScript : MonoBehaviour
{
    
    #region Variables

    #endregion

    #region Unity methods

    void Start()
    {
        
    }

    
    void Update()
    {
        
    }
    public void StartGame()
    {
        SceneManager.LoadScene(PlayerPrefs.GetInt("level", 1));
    }
    public void Reset()
    {
        PlayerPrefs.SetInt("level", 1);
    }
    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void Quit()
    {
        Application.Quit();
    }
    #endregion
}
