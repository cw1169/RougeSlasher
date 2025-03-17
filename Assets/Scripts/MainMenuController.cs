using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class MainMenuController : MonoBehaviour
{

    public SceneTransition transitionScript;
    public GameObject fadeTransition;

    void Start(){
        fadeTransition.SetActive(false);
    }

    public void StartGame(){
        fadeTransition.SetActive(true);
        transitionScript.beginFadeTransition("Main Game");
    }

//Function to close the application
    // public void QuitApplication(){
    //     Application.Quit();
    //     #if UNITY_EDITOR
    //     UnityEditor.EditorApplication.isPlaying = false;
    //     #endif
    // }
}
