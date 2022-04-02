using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelMenager : MonoBehaviour{
    public void QuitGame(){
        Application.Quit();
    }
    public void PlayGame(){
        SceneManager.LoadScene("SampleLevelOne");
    }
    public void MainMenu(){
        SceneManager.LoadScene("Menu");
    }
}
